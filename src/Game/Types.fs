module Game.Types

[<Measure>]
type gigawatt

[<Measure>]
type hitpoints

[<Measure>]
type coordinatecomponent

[<Measure>]
type torpedo

[<Measure>]
type celcius

[<Measure>]
type stardate

[<Measure>]
type score

(*
type RangeValue<'T> when 'T : (static member (-) : 'T * 'T -> 'T )
                     and 'T : (static member Zero: 'T)
                     and 'T : comparison =
  { Max: 'T
    Current: 'T
  }
  static member inline (-) (e:RangeValue<'T>, value: 'T) =
    let inline sub (a:^a, b:^b) = ((^a or ^b) : (static member (-) : 'T * 'T -> 'T) (a,b))
    let zero:'T = LanguagePrimitives.GenericZero<'T>
    let newValue:'T = sub (e.Current,value) // (e.Current |> float) - (value |> float)
    { e with Current = if newValue < zero then zero else newValue }
  static member inline (-) (e:RangeValue<'T>, value: 'T) =
    let zero:'T = LanguagePrimitives.GenericZero<'T>
    let newValue:'T = e.Current - value
    { e with Current = if newValue < zero then zero else newValue }
*)

type RangeValue<'T> when 'T : (static member (+) : 'T * 'T -> 'T )
                     and 'T : (static member (-) : 'T * 'T -> 'T )
                     and 'T : (static member op_Explicit : 'T -> float)
                     and 'T : (static member Zero: 'T)
                     and 'T : comparison =
  { Max: ^T
    Current: ^T
  }
  member inline rt.Update (newValue:^T) =
    let zero:'T = LanguagePrimitives.GenericZero<'T>
    let rangeBoundValue =      
      if newValue < zero then zero elif newValue > rt.Max then rt.Max else newValue
    { rt with Current = rangeBoundValue }  
  member inline rt.Percentage = (rt.Current |> float)/(rt.Max |> float)
  member inline rt.PercentageAsString = sprintf "%.0f" (rt.Percentage * 100.) 
  static member inline Create withMax = { Max = withMax ; Current = withMax }
  static member inline CreateAt withValue withMax = { Max = withMax ; Current = withValue } 

type EnergyLevel = RangeValue<float<gigawatt>>

type HitPoints = RangeValue<float<hitpoints>>

type Torpedos = RangeValue<int<torpedo>>

type TemperatureGauge = RangeValue<float<celcius>>

type Position =
  {
    X: int<coordinatecomponent>
    Y: int<coordinatecomponent>
  }
  member p.AsString = sprintf "%d,%d" p.X p.Y
  member p.DistanceTo position =
    ((position.X - p.X)*(position.X - p.X))+((position.Y - p.Y)*(position.Y - p.Y)) |> float |> sqrt

type GameWorldPosition =
  {
    GalacticPosition: Position
    SectorPosition: Position
  }
  member p.AsString = sprintf "%s,%s" p.GalacticPosition.AsString p.SectorPosition.AsString
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

type CaptainsLogItem =
  | Information of string
  | Warning of string
  | Danger of string

type Player =
  { Position: GameWorldPosition
    ShieldsRaised: bool
    Energy: EnergyLevel
    ForeShields: EnergyLevel
    PortShields: EnergyLevel
    AftShields: EnergyLevel
    StarboardShields: EnergyLevel
    Torpedos: Torpedos
    PhaserPower: EnergyLevel
    PhaserTemperature: TemperatureGauge
    Targets: GameWorldPosition list
    DockedWith: GameWorldPosition option
    CaptainsLog: CaptainsLogItem list
    // Systems
    Hull: HitPoints // what it says on the tin
    WarpDrive: HitPoints // enables warp travel
    ImpulseDrive: HitPoints // enables impulse movement
    ShieldGenerator: HitPoints // what it says on the tin
    EnergyConverter: HitPoints // generates energy while warping
    DeflectorDish: HitPoints // prevents the ship from sustaining damage while warping
    Phasers: HitPoints
    TorpedoLaunchers: HitPoints
    // Energy costs
    ImpulseMovementCost: float<gigawatt>
    PhaserTemperatureCostPerGigawatt: float<celcius>
  }
  static member Default =
    {
      Position = { GalacticPosition = { X = 0<coordinatecomponent> ; Y = 0<coordinatecomponent>} ; SectorPosition = { X = 0<coordinatecomponent> ; Y = 0<coordinatecomponent> }}
      ShieldsRaised = true
      Energy = EnergyLevel.Create 5000.<gigawatt>
      ForeShields = EnergyLevel.Create 1500.<gigawatt>
      PortShields = EnergyLevel.Create 1000.<gigawatt>
      AftShields = EnergyLevel.Create 1500.<gigawatt>
      StarboardShields = EnergyLevel.Create 1000.<gigawatt>
      Torpedos = Torpedos.Create 9<torpedo>
      PhaserPower = EnergyLevel.CreateAt 400.<gigawatt> 750.<gigawatt>
      PhaserTemperature = EnergyLevel.CreateAt 0.<celcius> 10000.<celcius>
      Targets = List.empty
      DockedWith = None
      CaptainsLog = []
      // Systems
      Hull = HitPoints.Create 3000.<hitpoints>
      WarpDrive = HitPoints.Create 1500.<hitpoints>
      ImpulseDrive = HitPoints.Create 1500.<hitpoints>
      ShieldGenerator = HitPoints.Create 1500.<hitpoints>
      EnergyConverter = HitPoints.Create 750.<hitpoints>
      DeflectorDish = HitPoints.Create 1000.<hitpoints>
      Phasers = HitPoints.Create 750.<hitpoints>
      TorpedoLaunchers = HitPoints.Create 1000.<hitpoints>
      // Energy costs
      ImpulseMovementCost = 50.<gigawatt>
      PhaserTemperatureCostPerGigawatt = 1.5<celcius>
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
  member x.IsEnemy = match x.Attributes with | EnemyAttributes _ -> true | _ -> false

type GameDifficulty =
  | EasyDifficulty
  | MediumDifficulty
  | HardDifficulty

type Game =
  { Difficulty: GameDifficulty
    GameObjects: GameObject array
    Player: Player
    Stardate: float<stardate>
    Score: int<score>
  }
  static member Empty = {
    Difficulty = MediumDifficulty
    GameObjects = Array.empty
    Player = Player.Default
    Stardate = 2872.<stardate>
    Score = 0<score>
  }

type FiringResponse =
  | TargetDamaged of Game
  | TargetDestroyed of Game
  | TargetMissed of Game

type UpdateGameStateMsg =
  | SetPhaserPower of float<gigawatt>
  | ToggleShields
  | MoveTo of GameWorldPosition
  | AddTarget of GameWorldPosition
  | RemoveTarget of GameWorldPosition
  | Dock of GameWorldPosition
  | Undock
  | FirePhasersAtPosition of GameWorldPosition
  | TargetDestroyed of GameWorldPosition
  
type GameMsg =
  | NewGame of GameDifficulty
  | UpdateGameState of UpdateGameStateMsg
