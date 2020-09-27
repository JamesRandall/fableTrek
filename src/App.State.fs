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

let update msg model =
  match (msg, model) with
  | (StartScreenDispatcherMsg subMsg, { StartScreen = Some extractedModel }) ->
    match subMsg with
    | Interface.StartScreen.Types.StartNewGame difficulty ->      
      console.log("Dispatching new game")
      { model with CurrentGame = difficulty |> Game.Factory.createGame |> Some }, Cmd.ofMsg (GameScreenPage |> GotoPage)        
    //| _ -> 
    //  let (subModel, subCmd) = Interface.StartScreen.State.update subMsg extractedModel
    //  { model with StartScreen = Some subModel}, Cmd.ofMsg (Game.Types.EasyDifficulty |> Game.Types.NewGame |> GameDispatcherMsg) // Cmd.map StartScreenDispatcherMsg subCmd
  | (GameScreenDispatcherMsg subMsg, { GameScreen = Some extractedModel ; CurrentGame = Some extractedGame }) ->
    let (subModel, subCmd) = Interface.GameScreen.State.update subMsg extractedModel extractedGame
    { model with GameScreen = Some subModel}, Cmd.map GameScreenDispatcherMsg subCmd
  | (GameDispatcherMsg subMsg, { CurrentGame = Some extractedModel }) ->
    let (subModel, subCmd) = Game.State.update subMsg extractedModel
    { model with CurrentGame = Some subModel}, Cmd.map GameDispatcherMsg subCmd
  | GotoPage page,_ ->
    page |> Router.modifyLocation
    model, Cmd.none
  | _ ->
    console.error("Missing match in App.State")
    model, Cmd.none

