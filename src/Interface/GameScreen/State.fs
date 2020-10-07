module Interface.GameScreen.State
open Types
open Elmish

let init () = Model.Empty, Cmd.none

let update msg (model:Model) game =
  match msg with
  | ShowShortRangeScannerMenu (position, menuItems) ->
    { model with ShortRangeScannerMenuItems = Some { Position = position ; MenuItems = menuItems }}, Cmd.none
  | HideShortRangeScannerMenu ->
    { model with ShortRangeScannerMenuItems = None }, Cmd.none
  | DisableUi ->
    { model with IsUiDisabled = true }, Cmd.none
  | EnableUi ->
    { model with IsUiDisabled = false }, Cmd.none
  | _ ->
    model, Cmd.none
