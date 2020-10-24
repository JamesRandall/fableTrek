module App.State

open App.Types
open Browser
open Elmish
open Router

let urlUpdate (result: Option<Router.Page>) model =
  match result with
  | None ->
    console.error("Error parsing url: " + window.location.href)
    model, Router.modifyUrl model.CurrentPage
  | Some page ->
    let model = { model with CurrentPage = page}
    match page with
    | StartScreenPage ->
      let (subModel, subCmd) = Interface.StartScreen.State.init () 
      { model with StartScreen = Some subModel }, subCmd
    | GameScreenPage ->
      let (subModel, subCmd) = Interface.GameScreen.State.init () 
      { model with GameScreen = Some subModel }, subCmd

let init result =
  //Model.Empty, Cmd.none 
  let (model, cmd) = urlUpdate result Model.Empty
  let game =
    match model.CurrentPage with
    | GameScreenPage ->
      let gameResult = Game.Factory.tryLoad()
      match gameResult with
      | Ok game -> Some game
      | Error _ ->
        Some (Game.Factory.createGame Game.Types.EasyDifficulty)
    | _ -> None
  { model with CurrentGame = game }, cmd

open Game.Types
open Interface.GameScreen.Types

let private animationSleep () = async {
  do! Async.Sleep (Interface.Animation.scannerAnimationDurationMs)
  return EnableUi |> GameScreenDispatcherMsg
}

let private sleepThenFireNextPhasers = async {
  do! Async.Sleep (Interface.Animation.phaserAnimationDurationMs)
  return FirePhasersAtNextTarget |> GameScreenDispatcherMsg
}

let private endWarpAnimation = async {
  do! Async.Sleep (Interface.Animation.warpAnimationDurationMs)
  return EndWarpTo |> GameScreenDispatcherMsg
}

// To bridge the two applications we have commands in the UI
// that map to commands in the game.
let mapUiCommandToGameCommand bridgeMsg uiModel game =
  match bridgeMsg with
  | GameBridgeMsg.ToggleShields -> 
    Cmd.map GameDispatcherMsg (Cmd.ofMsg (UpdateGameStateMsg.ToggleShields |> UpdateGameState))
  | GameBridgeMsg.FirePhasersAtTarget position ->
    Cmd.map GameDispatcherMsg (Cmd.ofMsg (position |> FirePhasersAtPosition |> UpdateGameState))
  | GameBridgeMsg.WarpTo position ->
    Cmd.map GameDispatcherMsg ({game.Player.Position with GalacticPosition = position } |> UpdateGameStateMsg.WarpTo |> UpdateGameState |> Cmd.ofMsg)

let mapGameCommandToUiCommand returnedCmd gameCmd =
  match gameCmd with
  | GameEvent bridgeMsg ->
    match bridgeMsg with
    | GameEventMsg.FiredPhasersAtTarget ->
      Cmd.OfAsync.result sleepThenFireNextPhasers
    | TargetDestroyed position -> Cmd.map GameScreenDispatcherMsg (Cmd.ofMsg (position |> Explosion.ExplodingEnemyScout |> ShowExplosion))
    | PlayerImpulsed _ -> Cmd.batch [
        Cmd.map GameScreenDispatcherMsg (Cmd.ofMsg HideShortRangeScannerMenu)
        Cmd.OfAsync.result (animationSleep ())
      ]
    | PlayerWarped _ ->
      Cmd.OfAsync.result (endWarpAnimation)
  // this last bit just makes the short range scanner overlay disappear for those commands that don't warrant eventing
  | UpdateGameState gameStateMsg ->
    match gameStateMsg with
    | AddTarget _ | RemoveTarget _ ->
        Cmd.map GameScreenDispatcherMsg (Cmd.ofMsg HideShortRangeScannerMenu)
    | _ -> Cmd.map GameDispatcherMsg returnedCmd
  | _ -> Cmd.map GameDispatcherMsg returnedCmd

let update msg model =
  match (msg, model) with
  | (StartScreenDispatcherMsg subMsg, { StartScreen = Some extractedModel }) ->
    match subMsg with
    | Interface.StartScreen.Types.StartNewGame difficulty ->      
      { model with CurrentGame = difficulty |> Game.Factory.createGame |> Some }, Cmd.ofMsg (GameScreenPage |> GotoPage)        
  | (GameScreenDispatcherMsg subMsg, { GameScreen = Some extractedModel ; CurrentGame = Some extractedGame }) ->
    match subMsg with
    | GameBridge gameBridgeMsg ->
      let cmd = mapUiCommandToGameCommand gameBridgeMsg extractedModel extractedGame
      model, cmd
    | _ ->
      let subModel, subCmd = Interface.GameScreen.State.update subMsg extractedModel extractedGame
      { model with GameScreen = Some subModel}, Cmd.map GameScreenDispatcherMsg  subCmd
  | (GameDispatcherMsg subMsg, { CurrentGame = Some extractedModel }) ->
    let (subModel, subCmd) = Game.State.update subMsg extractedModel
    { model with CurrentGame = Some subModel}, (subMsg |> mapGameCommandToUiCommand subCmd)
  | GotoPage page,_ ->
    page |> Router.modifyLocation
    model, Cmd.none
  | CommandSequence sequence,_ ->
    match sequence |> Seq.tryHead with
    | Some head ->
      model, Cmd.batch [head ; Cmd.ofMsg (sequence |> Seq.skip 1 |> Seq.toArray |> CommandSequence)]
    | None -> model, Cmd.none
  | _ ->
    console.error("Missing match in App.State")
    model, Cmd.none

