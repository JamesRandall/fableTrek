module Game.State

open Elmish
open Factory
open Types
open Game.Rules.Movement
open Game.Rules.Weapons

let appendToCaptainsLog player logItem =
  { player with CaptainsLog = [logItem] |> List.append player.CaptainsLog }

let updatePlayerState msg game =
  let playerModel = game.Player
  let updateGameWithPlayer player = { game with Player = player }, Cmd.none
  match msg with
  | ToggleShields -> { playerModel with ShieldsRaised = not(playerModel.ShieldsRaised)} |> updateGameWithPlayer
  | SetPhaserPower newPower -> { playerModel with PhaserPower = playerModel.PhaserPower.Update newPower } |> updateGameWithPlayer
  | SetWarpSpeed newSpeed -> { playerModel with WarpSpeed = playerModel.WarpSpeed.Update newSpeed } |> updateGameWithPlayer
  | ImpulseTo newPosition ->  
    match Game.Rules.Movement.move playerModel game.GameObjects newPosition with
    | Ok newPlayer -> newPlayer |> updateGameWithPlayer
    | Error errorMessage -> (errorMessage |> Warning |> appendToCaptainsLog playerModel) |> updateGameWithPlayer
  | WarpTo newPosition ->  
    match Game.Rules.Movement.move playerModel game.GameObjects newPosition with
    | Ok newPlayer -> { game with Player = newPlayer ; DiscoveredSectors = game.DiscoveredSectors |> Game.Rules.Sensors.discover newPlayer }, Cmd.none
    | Error errorMessage -> (errorMessage |> Warning |> appendToCaptainsLog playerModel) |> updateGameWithPlayer
  | AddTarget position ->
    match Utils.GameWorld.objectAtPosition game.GameObjects position with
    | Some gameObject ->
      match addTarget playerModel gameObject with
      | Ok newPlayer -> newPlayer |> updateGameWithPlayer
      | Error errorMessage -> (errorMessage |> Warning |> appendToCaptainsLog playerModel) |> updateGameWithPlayer
    | None -> playerModel |> updateGameWithPlayer
  | RemoveTarget position ->
    match removeTarget playerModel position with
    | Ok newPlayer -> newPlayer |> updateGameWithPlayer
    | Error errorMessage -> (errorMessage |> Warning |> appendToCaptainsLog playerModel)|> updateGameWithPlayer
  | FirePhasersAtPosition position ->
    match firePhasers game position with
    | FiringResponse.TargetDestroyed updatedGame ->
      updatedGame, Cmd.ofMsg (position |> UpdateGameStateMsg.TargetDestroyed |> UpdateGameState)
    | FiringResponse.TargetDamaged updatedGame ->
      updatedGame, Cmd.none
    | FiringResponse.TargetMissed updatedGame ->
      updatedGame, Cmd.none
  | BeginAiTurn -> (game |> Game.Rules.Turn.generateAiActions), Cmd.none
  | _ -> playerModel |> updateGameWithPlayer

let update msg model =
  match msg with
  | NewGame difficulty ->
    difficulty |> createGame, Cmd.none
  | UpdateGameState subMsg -> 
    updatePlayerState subMsg model