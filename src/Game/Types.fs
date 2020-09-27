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
                     and ^T : comparison =
  { Max: 'T
    Current: 'T
  }
  static member inline Create (withMax:^T) = { Max = withMax ; Current = withMax }
  static member inline (+~) (e:RangeValue<'T>, value:'T)  = 
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
  { Energy: EnergyLevel
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

type GameObjectAttributes =
  | EnemyAttributes of Enemy
  | StarbaseAttributes of Starbase
  | PlayerAttributes of Player
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
    | PlayerAttributes _ -> "USS Discovery"
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
  }
  static member Empty = {
    Difficulty = MediumDifficulty
    Score = 0
    GameObjects = Array.empty
  }
  member g.Player = g.GameObjects |> Seq.find (fun go -> match go.Attributes with | PlayerAttributes _ -> true | _ -> false)

type GameMsg =
  | NewGame of GameDifficulty