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

let preGameUpdateMessage gameDispatcherMsg =
  match gameDispatcherMsg with
  | UpdateGameState subMsg ->
    match subMsg with
    | MoveTo _ ->
      Cmd.map GameScreenDispatcherMsg (Cmd.ofMsg DisableUi)
    | _ -> Cmd.none
  | _ -> Cmd.none

let postGameUpdateMessage gameDispatcherMsg =
  match gameDispatcherMsg with
  | UpdateGameState subMsg ->
    match subMsg with
    | MoveTo _ ->
      Cmd.batch [
        Cmd.map GameScreenDispatcherMsg (Cmd.ofMsg HideShortRangeScannerMenu)
        Cmd.OfAsync.result (animationSleep ())
      ]
    | AddTarget _ | RemoveTarget _ ->
      Cmd.map GameScreenDispatcherMsg (Cmd.ofMsg HideShortRangeScannerMenu)
    | FirePhasersAtPosition _ -> Cmd.OfAsync.result sleepThenFireNextPhasers      
    | TargetDestroyed position -> Cmd.map GameScreenDispatcherMsg (Cmd.ofMsg (position |> Explosion.ExplodingEnemyScout |> ShowExplosion))
    | _ -> Cmd.none      
  | _ -> Cmd.none

let update msg model =
  match (msg, model) with
  | (StartScreenDispatcherMsg subMsg, { StartScreen = Some extractedModel }) ->
    match subMsg with
    | Interface.StartScreen.Types.StartNewGame difficulty ->      
      console.log("Dispatching new game")
      { model with CurrentGame = difficulty |> Game.Factory.createGame |> Some }, Cmd.ofMsg (GameScreenPage |> GotoPage)        
  | (GameScreenDispatcherMsg subMsg, { GameScreen = Some extractedModel ; CurrentGame = Some extractedGame }) ->
    match subMsg with
    | FirePhasersAtTarget position ->
      let (subModel, subCmd) = Interface.GameScreen.State.update subMsg extractedModel extractedGame
      model, Cmd.map GameDispatcherMsg (Cmd.ofMsg (position |> FirePhasersAtPosition |> UpdateGameState))
    | _ ->
      let (subModel, subCmd) = Interface.GameScreen.State.update subMsg extractedModel extractedGame
      { model with GameScreen = Some subModel}, Cmd.map GameScreenDispatcherMsg subCmd
  | (GameDispatcherMsg subMsg, { CurrentGame = Some extractedModel }) ->
    let (subModel, subCmd) = Game.State.update subMsg extractedModel
    { model with CurrentGame = Some subModel}, Cmd.batch [
      preGameUpdateMessage subMsg
      Cmd.map GameDispatcherMsg subCmd
      postGameUpdateMessage subMsg
    ] 
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

