module Game.State

open Elmish
open Factory
open Types

let updatePlayerState msg playerModel =
  match msg with
  | ToggleShields -> { playerModel with ShieldsRaised = not(playerModel.ShieldsRaised)}, Cmd.none
  | SetPhaserPower newPower -> { playerModel with PhaserPower = playerModel.PhaserPower.Update newPower }, Cmd.none

let update msg model =
  match msg with
  | NewGame difficulty ->
    difficulty |> createGame, Cmd.none
  | UpdatePlayerState subMsg -> 
    let playerModel, cmd = updatePlayerState subMsg model.Player
    { model with Player = playerModel }, cmd