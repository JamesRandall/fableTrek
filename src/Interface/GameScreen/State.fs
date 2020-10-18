module Interface.GameScreen.State
open Types
open Elmish
open Game.Types


let init () = Model.Empty, Cmd.none

let update msg (model:Model) game =
  match msg with
  | SetWarpDestination p ->
    { model with WarpDestination = p |> Some }, Cmd.none
  | RemoveWarpDestination ->
    { model with WarpDestination = None }, Cmd.none
  | ShowLongRangeScanner ->
    { model with IsLongRangeScannerVisible = true }, Cmd.none
  | HideLongRangeScanner ->
    { model with IsLongRangeScannerVisible = false}, Cmd.none
  | ShowShortRangeScannerMenu (position, menuItems) ->
    { model with ShortRangeScannerMenuItems = Some { Position = position ; MenuItems = menuItems }}, Cmd.none
  | HideShortRangeScannerMenu ->
    { model with ShortRangeScannerMenuItems = None }, Cmd.none
  | DisableUi ->
    { model with IsUiDisabled = true }, Cmd.none
  | EnableUi ->
    { model with IsUiDisabled = false }, Cmd.none
  | ShowExplosion explosion ->
    { model with Explosions = model.Explosions |> List.append [explosion]}, Cmd.none
  | FirePhasers ->
    { model with FiringTargets = game.Player.Targets }, Cmd.ofMsg FirePhasersAtNextTarget
  | FirePhasersAtNextTarget ->
    match model.FiringTargets |> Seq.tryHead with
    | Some nextTarget ->
      { model with CurrentTarget = Some nextTarget ; FiringTargets = model.FiringTargets |> Seq.skip 1 |> Seq.toList }, Cmd.ofMsg (nextTarget |> FirePhasersAtTarget)
    | None -> { model with CurrentTarget = None }, Cmd.none
  | _ ->
    model, Cmd.none
