module Interface.StartScreen.State

open Types
open Elmish

let init () = Model.Empty, Cmd.none

let update msg model =
  match msg with
  | NewGame -> model, Cmd.none