module Game.Types

[<Measure>]
type gigawatt

[<Measure>]
type hitpoints

[<Measure>]
type coordinatecomponent

[<Measure>]
type torpedo

type RangeValue<'T> when ^T : (static member (+) : ^T * ^T -> ^T )
                     and ^T : (static member (-) : ^T * ^T -> ^T )
                     and ^T : (static member op_Explicit : ^T -> float)
                     and ^T : comparison =
  { Max: 'T
    Current: 'T
  }
  member inline rt.Percentage = (rt.Current |> float)/(rt.Max |> float)
  member inline rt.PercentageAsString = sprintf "%.0f" (rt.Percentage * 100.) 
  static member inline Create (withMax:^T) = { Max = withMax ; Current = withMax }
  static member inline (+~) (e:RangeValue<'T>, value:^T)  = 
    let newValue = e.Current + value
    if newValue > e.Max then
      let overflow = newValue - e.Max
      { e with Current = e.Max }, overflow
    else
      { e with Current = newValue }, (newValue-newValue) // how do I get the default value / cast for a generic type, I need 0

type EnergyLevel = RangeValue<float<gigawatt>>

type HitPoints = RangeValue<float<hitpoints>>

type Torpedos = RangeValue<int<torpedo>>

type Position =
  {
    X: int<coordinatecomponent>
    Y: int<coordinatecomponent>
  }
  member p.DistanceTo position =
    ((position.X - p.X)*(position.X - p.X))+((position.Y - p.Y)*(position.Y - p.Y)) |> float |> sqrt

type GameWorldPosition =
  {
    GalacticPosition: Position
    SectorPosition: Position
  }
  static member Max =
    { GalacticPosition = { X = 7<coordinatecomponent> ; Y = 7<coordinatecomponent>}
      SectorPosition = { X = 7<coordinatecomponent> ; Y = 7<coordinatecomponent> }
    }

type EnemyType =
  | Scout
  | Cruiser
  | Dreadnought

type Enemy =
  { Energy: EnergyLevel
    Shields: EnergyLevel 
    HitPoints: HitPoints
    ShipClass: EnemyType
    RechargeRate: float<gigawatt> // the rate at which the enemy can regenerate energy
  }

type Starbase =
  { Energy: EnergyLevel
    Shields: EnergyLevel
    HitPoints: HitPoints
    RechargeRate: float<gigawatt>
  }

type Player =
  { Position: GameWorldPosition
    Energy: EnergyLevel
    ForeShields: EnergyLevel
    PortShields: EnergyLevel
    AftShields: EnergyLevel
    StarboardShields: EnergyLevel
    Torpedos: Torpedos
    // Systems
    Hull: HitPoints // what it says on the tin
    WarpDrive: HitPoints // enables warp travel
    ShieldGenerator: HitPoints // what it says on the tin
    EnergyConverter: HitPoints // generates energy while warping
    DeflectorDish: HitPoints // prevents the ship from sustaining damage while warping
    Phasers: HitPoints
    TorpedoLaunchers: HitPoints
  }
  static member Default =
    {
      Position = { GalacticPosition = { X = 0<coordinatecomponent> ; Y = 0<coordinatecomponent>} ; SectorPosition = { X = 0<coordinatecomponent> ; Y = 0<coordinatecomponent> }}
      Energy = EnergyLevel.Create 5000.<gigawatt>
      ForeShields = EnergyLevel.Create 1500.<gigawatt>
      PortShields = EnergyLevel.Create 1000.<gigawatt>
      AftShields = EnergyLevel.Create 1500.<gigawatt>
      StarboardShields = EnergyLevel.Create 1000.<gigawatt>
      Torpedos = Torpedos.Create 9<torpedo>
      // Systems
      Hull = HitPoints.Create 3000.<hitpoints>
      WarpDrive = HitPoints.Create 1500.<hitpoints>
      ShieldGenerator = HitPoints.Create 1500.<hitpoints>
      EnergyConverter = HitPoints.Create 750.<hitpoints>
      DeflectorDish = HitPoints.Create 1000.<hitpoints>
      Phasers = HitPoints.Create 750.<hitpoints>
      TorpedoLaunchers = HitPoints.Create 1000.<hitpoints>
    }

type GameObjectAttributes =
  | EnemyAttributes of Enemy
  | StarbaseAttributes of Starbase
  | StarAttributes

type GameObject =
  { Position: GameWorldPosition
    Attributes: GameObjectAttributes
  }
  member x.Name =
    match x.Attributes with
    | EnemyAttributes enemy ->
      match enemy.ShipClass with
      | Scout -> "Scout"
      | Cruiser -> "Crusier"
      | Dreadnought -> "Dreadnought"
    | StarbaseAttributes _ -> "Starbase"
    | StarAttributes -> "Star"

type GameDifficulty =
  | EasyDifficulty
  | MediumDifficulty
  | HardDifficulty

type Game =
  { Difficulty: GameDifficulty
    Score: int
    GameObjects: GameObject array
    Player: Player
  }
  static member Empty = {
    Difficulty = MediumDifficulty
    Score = 0
    GameObjects = Array.empty
    Player = Player.Default
  }

type GameMsg =
  | NewGame of GameDifficulty