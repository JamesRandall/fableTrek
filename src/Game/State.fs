module Game.State

open Elmish
open Factory
open Types

let appendToCaptainsLog player logItem =
  { player with CaptainsLog = [logItem] |> List.append player.CaptainsLog }

let updatePlayerState msg game =
  let playerModel = game.Player
  match msg with
  | ToggleShields -> { playerModel with ShieldsRaised = not(playerModel.ShieldsRaised)}, Cmd.none
  | SetPhaserPower newPower -> { playerModel with PhaserPower = playerModel.PhaserPower.Update newPower }, Cmd.none
  | MoveTo newPosition ->  
    match Game.Utils.Player.move playerModel game.GameObjects newPosition with
    | Ok newPlayer -> newPlayer, Cmd.none
    | Error errorMessage -> (errorMessage |> Warning |> appendToCaptainsLog playerModel), Cmd.none
  | _ -> playerModel, Cmd.none

let update msg model =
  match msg with
  | NewGame difficulty ->
    difficulty |> createGame, Cmd.none
  | UpdatePlayerState subMsg -> 
    let playerModel, cmd = updatePlayerState subMsg model
    { model with Player = playerModel }, cmd