module Game.State

open Elmish
open Factory
open Types

let update msg model =
  match msg with
  | NewGame difficulty ->
    difficulty |> createGame, Cmd.none