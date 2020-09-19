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
      { model with StartScreen = Some subModel }, Cmd.map StartScreenDispatcherMsg subCmd
    | _ -> model, Cmd.none

let init _ =
  Model.Empty, Cmd.none 

let update msg model =
  match (msg, model) with
  // begin dispatcher update - do not remove
  | (StartScreenDispatcherMsg msg, { StartScreen = Some extractedModel }) ->
    let (subModel, subCmd) = Interface.StartScreen.State.update msg extractedModel
    { model with StartScreen = Some subModel}, Cmd.map StartScreenDispatcherMsg subCmd
  | _ -> model, Cmd.none

