module App

open Elmish
open Fable.React
open Fable.React.Props
open App.Types
open App.State
open Fable.Core.JsInterop

importSideEffects "./game.styl"

let root model dispatch =
  div [Class "container"] [
    match model with
    | { CurrentPage = Router.Page.StartScreenPage ; StartScreen = Some extractedModel } ->
      Interface.StartScreen.View.root model (StartScreenDispatcherMsg >> dispatch)
    | { CurrentPage = Router.Page.GameScreenPage ; GameScreen = Some extractedModel ; CurrentGame = Some extractedGame  } ->
      Interface.GameScreen.View.root extractedGame extractedModel (GameScreenDispatcherMsg >> dispatch)
    | _ -> div [Class "textError"] [str "not found"]
  ]
  

#if DEBUG
open Elmish.Debug
#endif
open Elmish.Navigation
open Elmish.UrlParser
open Elmish.HMR

Program.mkProgram init update root
|> Program.toNavigable (parseHash Router.pageParser) urlUpdate
#if DEBUG
|> Program.withConsoleTrace
|> Program.withDebugger
#endif
|> Program.withReactBatched "trek-app"
|> Program.run

