module Interface.GameScreen.State
open Types
open Elmish

let init () = Model.Empty, Cmd.none

let update msg model game =
  model, Cmd.none
