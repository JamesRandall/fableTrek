module Interface.GameScreen.Types
open Game.Types

type MenuItem =
  | NoActionLabel of string
  | MenuItem of (string * UpdateGameStateMsg)

type ShortRangeScannerMenu =
  { Position: GameWorldPosition ; MenuItems: MenuItem array }

type Model =
  {
    IsUiDisabled: bool
    IsLongRangeScannerVisible: bool
    ShortRangeScannerMenuItems: ShortRangeScannerMenu option
  }
  static member Empty =
    { IsUiDisabled = false
      IsLongRangeScannerVisible = false
      ShortRangeScannerMenuItems = None
    }

type GameScreenMsg =
  | ShowLongRangeScanner
  | HideLongRangeScanner
  | ShowShortRangeScannerMenu of (GameWorldPosition*MenuItem array)
  | HideShortRangeScannerMenu
  | FirePhasers
  | ShowPhasers of GameWorldPosition
  | HidePhasers
  | DisableUi
  | EnableUi