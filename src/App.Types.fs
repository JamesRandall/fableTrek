module App.Types
open Router

type Model =
  {
    CurrentPage: Router.Page
    StartScreen: Interface.StartScreen.Types.Model option
  }
  static member Empty =
    { CurrentPage = StartScreenPage
      StartScreen = Some Interface.StartScreen.Types.Model.Empty
    }

type Msg =
  | GameScreenDispatcherMsg
  | StartScreenDispatcherMsg of Interface.StartScreen.Types.StartScreenMsg