module Interface.StartScreen.Types

type Model =
  {
    Placeholder : unit  
  }
  static member Empty =
    { Placeholder = ()
    }

type StartScreenMsg =
  | StartNewGame of Game.Types.GameDifficulty
