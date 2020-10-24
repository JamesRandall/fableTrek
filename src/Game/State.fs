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
  let updateGameWithPlayer cmd player = ({ game with Player = player }, cmd)
  match msg with
  | ToggleShields -> { playerModel with ShieldsRaised = not(playerModel.ShieldsRaised)} |> updateGameWithPlayer Cmd.none
  | SetPhaserPower newPower -> { playerModel with PhaserPower = playerModel.PhaserPower.Update newPower } |> updateGameWithPlayer Cmd.none
  | SetWarpSpeed newSpeed -> { playerModel with WarpSpeed = playerModel.WarpSpeed.Update newSpeed } |> updateGameWithPlayer Cmd.none
  | ImpulseTo newPosition ->  
    match Game.Rules.Movement.move playerModel game.GameObjects newPosition with
    | Ok newPlayer -> newPlayer |> updateGameWithPlayer (Cmd.ofMsg (true |> GameEventMsg.PlayerImpulsed |> GameEvent))
    | Error errorMessage -> (errorMessage |> Warning |> appendToCaptainsLog playerModel) |> updateGameWithPlayer (Cmd.ofMsg (false |> GameEventMsg.PlayerImpulsed |> GameEvent))
  | WarpTo newPosition ->  
    match Game.Rules.Movement.move playerModel game.GameObjects newPosition with
    | Ok newPlayer -> { game with Player = newPlayer ; DiscoveredSectors = game.DiscoveredSectors |> Game.Rules.Sensors.discover newPlayer }, Cmd.ofMsg (true |> GameEventMsg.PlayerWarped |> GameEvent)
    | Error errorMessage -> (errorMessage |> Warning |> appendToCaptainsLog playerModel) |> updateGameWithPlayer (Cmd.ofMsg (false |> GameEventMsg.PlayerWarped |> GameEvent))
  | AddTarget position ->
    match Utils.GameWorld.objectAtPosition game.GameObjects position with
    | Some gameObject ->
      match addTarget playerModel gameObject with
      | Ok newPlayer -> newPlayer |> updateGameWithPlayer Cmd.none
      | Error errorMessage -> (errorMessage |> Warning |> appendToCaptainsLog playerModel) |> updateGameWithPlayer Cmd.none
    | None -> playerModel |> updateGameWithPlayer Cmd.none
  | RemoveTarget position ->
    match removeTarget playerModel position with
    | Ok newPlayer -> newPlayer |> updateGameWithPlayer Cmd.none
    | Error errorMessage -> (errorMessage |> Warning |> appendToCaptainsLog playerModel)|> updateGameWithPlayer Cmd.none
  | FirePhasersAtPosition position ->
    match firePhasers game position with
    | FiringResponse.TargetDestroyed updatedGame ->
      updatedGame, Cmd.ofMsg (position |> GameEventMsg.TargetDestroyed |> GameEvent)
    | FiringResponse.TargetDamaged updatedGame ->
      updatedGame, Cmd.ofMsg (GameEventMsg.FiredPhasersAtTarget |> GameEvent)
    | FiringResponse.TargetMissed updatedGame ->
      updatedGame, Cmd.ofMsg (GameEventMsg.FiredPhasersAtTarget |> GameEvent)
  | BeginAiTurn -> (game |> Game.Rules.Turn.generateAiActions), Cmd.none
  | _ -> playerModel |> updateGameWithPlayer Cmd.none

let update msg model =
  match msg with
  | NewGame difficulty ->
    difficulty |> createGame, Cmd.none
  | UpdateGameState subMsg -> 
    updatePlayerState subMsg model
  | GameEvent _ -> model, Cmd.none