module Interface.StartScreen.State

open Types
open Elmish

let init () = Model.Empty, Cmd.none

let update msg model =
  model, Cmd.none
