module Game.Factory

open Types
open Browser
open Utils.Position

// there is lots of scope to optimise in here but we're not dealing with much data
// and I'd rather keep the code simple for the sake of explanation
// maybe later optimise and explain
let createGame difficulty = 
  let createGameworldObjects numberOfObjects createAttributes previousObjects =
    {0..numberOfObjects}
    |> Seq.fold (fun foldedPreviousObjects _ ->
      let newStar =
        { Position = findRandomAndVacantGalacticPosition foldedPreviousObjects
          Attributes = createAttributes ()
        }
      foldedPreviousObjects |> Seq.append [newStar]
    ) previousObjects

  let createStars = createGameworldObjects 200 (fun () -> StarAttributes)
  
  let createEnemies enemyShipClass = 
    let numberOf, maxEnergy, maxShields, maxHitPoints, rechargeRate =
      match enemyShipClass with
      | Scout ->
        match difficulty with
        | EasyDifficulty -> 50, 1000.<gigawatt>, 1000.<gigawatt>, 1000.<hitpoints>, 100.<gigawatt>
        | MediumDifficulty -> 60, 1000.<gigawatt>, 1250.<gigawatt>, 1200.<hitpoints>, 120.<gigawatt>
        | HardDifficulty -> 70, 1000.<gigawatt>, 1500.<gigawatt>, 1500.<hitpoints>, 140.<gigawatt>
      | Cruiser ->
        match difficulty with
        | EasyDifficulty -> 15, 2000.<gigawatt>, 2000.<gigawatt>, 1500.<hitpoints>, 200.<gigawatt>
        | MediumDifficulty -> 20, 2000.<gigawatt>, 2500.<gigawatt>, 1750.<hitpoints>, 200.<gigawatt>
        | HardDifficulty -> 25, 2000.<gigawatt>, 3000.<gigawatt>, 2000.<hitpoints>, 250.<gigawatt>
      | Dreadnought ->
        match difficulty with
        | EasyDifficulty -> 5, 2000.<gigawatt>, 2000.<gigawatt>, 1500.<hitpoints>, 200.<gigawatt>
        | MediumDifficulty -> 10, 2000.<gigawatt>, 2500.<gigawatt>, 1750.<hitpoints>, 200.<gigawatt>
        | HardDifficulty -> 15, 2000.<gigawatt>, 3000.<gigawatt>, 2000.<hitpoints>, 250.<gigawatt>

    let createEnemy () =
      { Energy = EnergyLevel.Create maxEnergy
        Shields = EnergyLevel.Create maxShields
        HitPoints = HitPoints.Create maxHitPoints
        RechargeRate = rechargeRate
        ShipClass = enemyShipClass
      } |> EnemyAttributes

    createGameworldObjects numberOf createEnemy

  let createStarbases =
    let numberOf, maxEnergy, maxShields, maxHitPoints, rechargeRate =
      match difficulty with
      | EasyDifficulty -> 5, 20000.<gigawatt>, 5000.<gigawatt>, 7500.<hitpoints>, 500.<gigawatt>
      | MediumDifficulty -> 4, 20000.<gigawatt>, 5000.<gigawatt>, 7500.<hitpoints>, 500.<gigawatt>
      | HardDifficulty -> 3, 20000.<gigawatt>, 5000.<gigawatt>, 7500.<hitpoints>, 500.<gigawatt>
    let createStarbase () =
      { Energy = EnergyLevel.Create maxEnergy
        Shields = EnergyLevel.Create maxShields
        HitPoints = HitPoints.Create maxHitPoints
        RechargeRate = rechargeRate
      } |> StarbaseAttributes
    createGameworldObjects numberOf createStarbase  
  
  let gameObjects =
    Seq.empty
    |> createStars
    |> createStarbases
    |> createEnemies Scout
    |> createEnemies Cruiser
    |> createEnemies Dreadnought
    |> Seq.toArray
  
  { Difficulty = difficulty
    Score = 0
    GameObjects = gameObjects
    Player = { Player.Default with Position = findRandomAndVacantGalacticPosition gameObjects ; ForeShields = { Player.Default.ForeShields with Current = 900.<gigawatt>} }
  }

let canLoad () = not (localStorage.getItem("currentGame") |> isNull) 

let tryLoad () =
  let possibleSavedGame = localStorage.getItem("currentGame")
  match possibleSavedGame |> isNull with
  | true -> Error "No saved game"
  | false ->
    Thoth.Json.Decode.Auto.fromString<Game> possibleSavedGame
