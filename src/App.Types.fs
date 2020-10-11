module App.Types
open Router
open Game.Types
open Elmish

type Model =
  {
    CurrentPage: Router.Page
    CurrentGame: Game option
    StartScreen: Interface.StartScreen.Types.Model option
    GameScreen: Interface.GameScreen.Types.Model option
  }
  static member Empty =
    { CurrentPage = StartScreenPage
      CurrentGame = None
      StartScreen = Some Interface.StartScreen.Types.Model.Empty
      GameScreen = None
    }

type Msg =
  | NewGame of GameDifficulty
  | GotoPage of Page
  | GameDispatcherMsg of GameMsg
  | GameScreenDispatcherMsg of Interface.GameScreen.Types.GameScreenMsg
  | StartScreenDispatcherMsg of Interface.StartScreen.Types.StartScreenMsg
  | CommandSequence of Cmd<Msg> array