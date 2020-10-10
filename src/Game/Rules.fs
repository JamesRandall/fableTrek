module Game.Rules

open Types
open Utils

module Enemy =
  let inline private overflowSubtract (current:^T) (value:^T) =
    let zero = LanguagePrimitives.GenericZero<'T>
    let newValue = current - value
    if newValue < zero then
      zero, abs newValue      
    else
      newValue, zero

  let energyImpact gameObject energy =    
    match gameObject.Attributes with
    | EnemyAttributes enemy ->
      let newShields, shieldsOverflow = overflowSubtract enemy.Shields.Current energy
      let newHull, _ = overflowSubtract enemy.HitPoints.Current ((shieldsOverflow |> float) * 1.<hitpoints>)
      if newHull <= 0.<hitpoints> then
        None
      else
        Some { gameObject with Attributes = { enemy with Shields = enemy.Shields.Update newShields
                                                         HitPoints = enemy.HitPoints.Update newHull
                                            } |> EnemyAttributes
             }      
    | _ -> Some gameObject

module Damage =
  let efficiencyFactor (systemHitPoints:HitPoints) =
    let percentage = systemHitPoints.Percentage
    let efficiency = System.Math.Log(percentage * 10., 10.)
    efficiency

  let inefficiencyFactor (systemHitPoints:HitPoints) =
    let percentage = systemHitPoints.Percentage
    let inefficiency = 1.+(1.-System.Math.Log(percentage * 10., 10.))
    inefficiency

module Weapons =
  let canAddTarget (player:Player) (gameObject:GameObject) =
    (player.Targets |> List.tryFind (fun t -> t = gameObject.Position)) |> Option.isNone &&
    player.Targets.Length < 3 &&
    gameObject.IsEnemy

  let addTarget (player:Player) (gameObject:GameObject) =
    if canAddTarget player gameObject  then
      Ok { player with Targets = [gameObject.Position] |> List.append player.Targets }
    else
      Error (sprintf "Cannot target object at %s" gameObject.Position.AsString)

  let removeTarget (player:Player) position =
    let newPlayer = { player with Targets = player.Targets |> List.filter (fun p -> p <> position)}
    if newPlayer.Targets.Length = player.Targets.Length then
      Error "No target to remove"
    else
      Ok newPlayer

  let canFirePhasers player =
    player.Targets.Length > 0 &&
    (player.PhaserPower.Current * (player.Targets.Length |> float) <= player.Energy.Current)

  let canFireTorpedoes player =
    player.Targets.Length > 0 && player.Targets.Length < (player.Torpedos.Current |> int)

  let calculatePhaserPower player =
    // TODO: factor distance to targer in here
    (player.Phasers |> Damage.efficiencyFactor) * player.PhaserPower.Current

  let calculatePhaserTemperatureIncrease player =
    if player.Phasers.Current <= 0.<hitpoints> then
      player.PhaserTemperature.Max
    else
      let damageModifier = 1. + (1. - System.Math.Log(player.Phasers.Percentage * 1000., 1000.))
      (player.PhaserPower.Current |> float) * player.PhaserTemperatureCostPerGigawatt * damageModifier        

  let calculatePhaserDamage player =
    0.<hitpoints>
  
  let inline firePhasers game targetPosition =
    let optionalTarget = Game.Utils.GameWorld.objectAtPosition game.GameObjects targetPosition
    match optionalTarget with
    | Some target ->
      let phaserEnergyHit = game.Player |> calculatePhaserPower
      let modifiedTarget = phaserEnergyHit |> Enemy.energyImpact target
      let modifiedPlayer = { game.Player with Energy = (game.Player.Energy.Update (game.Player.Energy.Current - game.Player.PhaserPower.Current))
                                              PhaserTemperature = game.Player.PhaserTemperature.Update(game.Player.PhaserTemperature.Current + (game.Player |> calculatePhaserTemperatureIncrease))
                                              Phasers = game.Player.Phasers.Update (game.Player.Phasers.Current + (game.Player |> calculatePhaserDamage))
                           }
      match modifiedTarget with
      | Some damagedTarget ->
        let logMessage = (sprintf "Hit of %.0f gigawatts on %s at %s" phaserEnergyHit target.Name target.Position.SectorPosition.AsString)
        { game with GameObjects = Game.Utils.GameWorld.replaceGameObject game.GameObjects damagedTarget
                    Player = { modifiedPlayer with CaptainsLog = [logMessage |> Information] |> List.append game.Player.CaptainsLog }
        }
      | None -> // destroyed target
        let logMessage = (sprintf "Destroyed %s at %s" target.Name target.Position.SectorPosition.AsString)
        { game with GameObjects = Game.Utils.GameWorld.removeGameObject game.GameObjects target
                    Player = { modifiedPlayer with CaptainsLog = [logMessage |> Information] |> List.append game.Player.CaptainsLog
                                                   Targets = modifiedPlayer.Targets |> List.filter (fun t -> t <> targetPosition)
                             }
        }
    | None ->
      { game with Player = { game.Player with CaptainsLog = ["Phasers missed!" |> Warning] |> List.append game.Player.CaptainsLog }}    

module Movement =
  open Damage

  let energyRequirementsForMove (player:Player) newPosition =
    if newPosition.GalacticPosition = player.Position.GalacticPosition then
      // we're moving within a sector
      let distance = player.Position.SectorPosition.DistanceTo newPosition.SectorPosition
      let energyCost = player.ImpulseMovementCost * (player.ImpulseDrive |> inefficiencyFactor) * distance
      energyCost
    else
      1000.<gigawatt>

  let canMove player newPosition =
    (energyRequirementsForMove player newPosition) <= player.Energy.Current

  let move player gameObjects newPosition =
    let energyRequirements = energyRequirementsForMove player newPosition
    match player.Energy.Current > energyRequirements, (Position.positionIsVacant gameObjects newPosition) with
    | true, true ->
      Ok { player with Energy = player.Energy.Update (player.Energy.Current - energyRequirements) ; Position = newPosition } 
    | false, _ ->
      Error "Insufficient energy to move to that location"
    | _, false ->
      Error "Object blocking move"