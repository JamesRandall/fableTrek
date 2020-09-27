module Interface.GameScreen.Types

type Model =
  {
    IsLongRangeScannerVisible: bool
  }
  static member Empty =
    { IsLongRangeScannerVisible = false
    }

type GameScreenMsg =
  | ShowLongRangeScanner
  | HideLongRangeScanner