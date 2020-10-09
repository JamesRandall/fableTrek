module Game.Rules

open Types
open Utils

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
    player.Targets.Length > 0

  let canFireTorpedoes player =
    player.Targets.Length > 0 && player.Targets.Length < (player.Torpedos.Current |> int)

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
      Ok { player with Energy = player.Energy - energyRequirements ; Position = newPosition } 
    | false, _ ->
      Error "Insufficient energy to move to that location"
    | _, false ->
      Error "Object blocking move"