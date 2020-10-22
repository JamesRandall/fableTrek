module Interface.GameScreen.Types
open Game.Types

type MenuItem =
  | NoActionLabel of string
  | MenuItem of (string * UpdateGameStateMsg)

type ShortRangeScannerMenu =
  { Position: GameWorldPosition ; MenuItems: MenuItem array }

type Explosion =
  | ExplodingEnemyScout of GameWorldPosition  

type Model =
  {
    IsUiDisabled: bool
    IsLongRangeScannerVisible: bool
    ShortRangeScannerMenuItems: ShortRangeScannerMenu option
    FiringTargets: GameWorldPosition list
    CurrentTarget: GameWorldPosition option
    Explosions: Explosion list
    WarpDestination: Position option
    IsWarping: bool
    IsDamageControlVisible: bool
    IsCaptainsLogVisible: bool
    IsEnemyTurn: bool
  }
  static member Empty =
    { IsUiDisabled = false
      IsLongRangeScannerVisible = false
      ShortRangeScannerMenuItems = None
      FiringTargets = List.empty
      CurrentTarget = None
      Explosions = List.empty
      WarpDestination = None
      IsWarping = false
      IsDamageControlVisible = false
      IsCaptainsLogVisible = false
      IsEnemyTurn = false
    }

type GameBridgeMsg =
  | ToggleShields
  | WarpTo of Position
  | FirePhasersAtTarget of GameWorldPosition

type GameScreenMsg =
  | ShowLongRangeScanner
  | HideLongRangeScanner  
  | ShowShortRangeScannerMenu of (GameWorldPosition*MenuItem array)
  | HideShortRangeScannerMenu
  | FirePhasers
  | FirePhasersAtNextTarget
  | FirePhasersAtTarget of GameWorldPosition
  | ShowExplosion of Explosion
  | DisableUi
  | EnableUi
  | SetWarpDestination of Position
  | RemoveWarpDestination
  | BeginWarpTo of Position
  | EndWarpTo
  | ShowDamageControl
  | HideDamageControl
  | ToggleShields
  | GameBridge of GameBridgeMsg
  //| ShowCaptainsLog
  //| HideCaptainsLog
  (*| HackHackMessage
  | ShowDamageControl
  | HideDamageControl
  | ShowCaptainsLog
  | HideCaptainsLog*)