module Interface.GameScreen.State
open Types
open Elmish
open Game.Types

let private sleepThenCompleteExplosion explosion = async {
  do! Async.Sleep (Interface.Animation.explosionDurationMs)
  return CompleteExplosion explosion
}

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
    { model with IsLongRangeScannerVisible = false ; IsWarping = false ; WarpDestination = None }, Cmd.none
  | ShowDamageControl ->
    { model with IsDamageControlVisible = true }, Cmd.none
  | HideDamageControl ->
    { model with IsDamageControlVisible = false }, Cmd.none
  //| ShowCaptainsLog ->
  //  { model with IsCaptainsLogVisible = true }, Cmd.none
  //| HideCaptainsLog ->
  //  { model with IsCaptainsLogVisible = false }, Cmd.none
  | ShowShortRangeScannerMenu (position, menuItems) ->
    { model with ShortRangeScannerMenuItems = Some { Position = position ; MenuItems = menuItems }}, Cmd.none
  | HideShortRangeScannerMenu ->
    { model with ShortRangeScannerMenuItems = None }, Cmd.none
  | DisableUi ->
    { model with IsUiDisabled = true }, Cmd.none
  | EnableUi ->
    { model with IsUiDisabled = false }, Cmd.none
  | ShowExplosion explosion ->
    { model with Explosions = model.Explosions |> List.append [explosion]}, Cmd.OfAsync.result (sleepThenCompleteExplosion explosion)
  | CompleteExplosion explosion ->
    //model, Cmd.ofMsg FirePhasersAtNextTarget
    Fable.Core.JS.console.log (sprintf "%O" model.Explosions)
    { model with Explosions = model.Explosions |> List.filter(fun e -> e <> explosion)}, Cmd.ofMsg FirePhasersAtNextTarget
  | FirePhasers ->
    { model with FiringTargets = game.Player.Targets }, Cmd.ofMsg FirePhasersAtNextTarget
  | FirePhasersAtNextTarget ->
    match model.FiringTargets |> Seq.tryHead with
    | Some nextTarget ->
      { model with CurrentTarget = Some nextTarget ; FiringTargets = model.FiringTargets |> Seq.skip 1 |> Seq.toList }, Cmd.ofMsg (nextTarget |> FirePhasersAtTarget |> GameRequest)
    | None -> { model with CurrentTarget = None }, Cmd.none
  | BeginWarpTo position -> { model with IsWarping = true ; IsUiDisabled = true }, Cmd.ofMsg (position |> GameRequestMsg.WarpTo |> GameRequest)
  | EndWarpTo ->
    (if model.IsWarping then { model with IsWarping = false ; IsUiDisabled = false ; IsLongRangeScannerVisible = false ; WarpDestination = None } else model ), Cmd.none
  | GameRequest _ ->
    // handled by App.State - not wildcarded due to Fable Compiler issue and for clarity as its not necessarily obvious
    // that these are handled elsewhere
    model, Cmd.none
  //| _ ->
  //  model, Cmd.none
