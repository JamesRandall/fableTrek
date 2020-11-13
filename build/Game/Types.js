import { Union, Record } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Types.js";
import { class_type, array_type, option_type, list_type, bool_type, string_type, float64_type, union_type, int32_type, record_type } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Reflection.js";
import { printf, toText } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/String.js";
import { ofArray, empty } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";
import { empty as empty_1 } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Set.js";
import { compareSafe } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Util.js";

export class RangeValue$1 extends Record {
    constructor(Max, Min, Current) {
        super();
        this.Max = Max;
        this.Min = Min;
        this.Current = Current;
    }
}

export function RangeValue$1$reflection(gen0) {
    return record_type("Game.Types.RangeValue`1", [gen0], RangeValue$1, () => [["Max", gen0], ["Min", gen0], ["Current", gen0]]);
}

export class Position extends Record {
    constructor(X, Y) {
        super();
        this.X = (X | 0);
        this.Y = (Y | 0);
    }
}

export function Position$reflection() {
    return record_type("Game.Types.Position", [], Position, () => [["X", int32_type], ["Y", int32_type]]);
}

export function Position__get_AsString(p) {
    const clo1 = toText(printf("%d,%d"));
    const clo2 = clo1(p.X);
    return clo2(p.Y);
}

export function Position__DistanceTo_Z53A6D567(p, position) {
    let value_1;
    const value = (((position.X - p.X) * (position.X - p.X)) + ((position.Y - p.Y) * (position.Y - p.Y))) | 0;
    value_1 = value;
    return Math.sqrt(value_1);
}

export class GameWorldPosition extends Record {
    constructor(GalacticPosition, SectorPosition) {
        super();
        this.GalacticPosition = GalacticPosition;
        this.SectorPosition = SectorPosition;
    }
}

export function GameWorldPosition$reflection() {
    return record_type("Game.Types.GameWorldPosition", [], GameWorldPosition, () => [["GalacticPosition", Position$reflection()], ["SectorPosition", Position$reflection()]]);
}

export function GameWorldPosition__get_AsString(p) {
    const arg10 = Position__get_AsString(p.GalacticPosition);
    const arg20 = Position__get_AsString(p.SectorPosition);
    const clo1 = toText(printf("%s,%s"));
    const clo2 = clo1(arg10);
    return clo2(arg20);
}

export function GameWorldPosition_get_Max() {
    return new GameWorldPosition(new Position(7, 7), new Position(7, 7));
}

export class EnemyType extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Scout", "Cruiser", "Dreadnought"];
    }
}

export function EnemyType$reflection() {
    return union_type("Game.Types.EnemyType", [], EnemyType, () => [[], [], []]);
}

export class Enemy extends Record {
    constructor(Energy, Shields, HitPoints, ShipClass, RechargeRate) {
        super();
        this.Energy = Energy;
        this.Shields = Shields;
        this.HitPoints = HitPoints;
        this.ShipClass = ShipClass;
        this.RechargeRate = RechargeRate;
    }
}

export function Enemy$reflection() {
    return record_type("Game.Types.Enemy", [], Enemy, () => [["Energy", RangeValue$1$reflection(float64_type)], ["Shields", RangeValue$1$reflection(float64_type)], ["HitPoints", RangeValue$1$reflection(float64_type)], ["ShipClass", EnemyType$reflection()], ["RechargeRate", float64_type]]);
}

export class Starbase extends Record {
    constructor(Energy, Shields, HitPoints, RechargeRate) {
        super();
        this.Energy = Energy;
        this.Shields = Shields;
        this.HitPoints = HitPoints;
        this.RechargeRate = RechargeRate;
    }
}

export function Starbase$reflection() {
    return record_type("Game.Types.Starbase", [], Starbase, () => [["Energy", RangeValue$1$reflection(float64_type)], ["Shields", RangeValue$1$reflection(float64_type)], ["HitPoints", RangeValue$1$reflection(float64_type)], ["RechargeRate", float64_type]]);
}

export class CaptainsLogItem extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["Information", "Warning", "Danger"];
    }
}

export function CaptainsLogItem$reflection() {
    return union_type("Game.Types.CaptainsLogItem", [], CaptainsLogItem, () => [[["Item", string_type]], [["Item", string_type]], [["Item", string_type]]]);
}

export class Player extends Record {
    constructor(Position, ShieldsRaised, Energy, ForeShields, PortShields, AftShields, StarboardShields, Torpedos, PhaserPower, PhaserTemperature, Targets, DockedWith, CaptainsLog, WarpSpeed, Hull, WarpDrive, ImpulseDrive, ShieldGenerator, EnergyConverter, DeflectorDish, Phasers, TorpedoLaunchers, ImpulseMovementCost, PhaserTemperatureCostPerGigawatt, EnergyGeneratedPerUnitOfTravel, EnergyCostPerUnitOfTravel, HitPointsRepairedPerDay) {
        super();
        this.Position = Position;
        this.ShieldsRaised = ShieldsRaised;
        this.Energy = Energy;
        this.ForeShields = ForeShields;
        this.PortShields = PortShields;
        this.AftShields = AftShields;
        this.StarboardShields = StarboardShields;
        this.Torpedos = Torpedos;
        this.PhaserPower = PhaserPower;
        this.PhaserTemperature = PhaserTemperature;
        this.Targets = Targets;
        this.DockedWith = DockedWith;
        this.CaptainsLog = CaptainsLog;
        this.WarpSpeed = WarpSpeed;
        this.Hull = Hull;
        this.WarpDrive = WarpDrive;
        this.ImpulseDrive = ImpulseDrive;
        this.ShieldGenerator = ShieldGenerator;
        this.EnergyConverter = EnergyConverter;
        this.DeflectorDish = DeflectorDish;
        this.Phasers = Phasers;
        this.TorpedoLaunchers = TorpedoLaunchers;
        this.ImpulseMovementCost = ImpulseMovementCost;
        this.PhaserTemperatureCostPerGigawatt = PhaserTemperatureCostPerGigawatt;
        this.EnergyGeneratedPerUnitOfTravel = EnergyGeneratedPerUnitOfTravel;
        this.EnergyCostPerUnitOfTravel = EnergyCostPerUnitOfTravel;
        this.HitPointsRepairedPerDay = HitPointsRepairedPerDay;
    }
}

export function Player$reflection() {
    return record_type("Game.Types.Player", [], Player, () => [["Position", GameWorldPosition$reflection()], ["ShieldsRaised", bool_type], ["Energy", RangeValue$1$reflection(float64_type)], ["ForeShields", RangeValue$1$reflection(float64_type)], ["PortShields", RangeValue$1$reflection(float64_type)], ["AftShields", RangeValue$1$reflection(float64_type)], ["StarboardShields", RangeValue$1$reflection(float64_type)], ["Torpedos", RangeValue$1$reflection(int32_type)], ["PhaserPower", RangeValue$1$reflection(float64_type)], ["PhaserTemperature", RangeValue$1$reflection(float64_type)], ["Targets", list_type(GameWorldPosition$reflection())], ["DockedWith", option_type(GameWorldPosition$reflection())], ["CaptainsLog", list_type(CaptainsLogItem$reflection())], ["WarpSpeed", RangeValue$1$reflection(float64_type)], ["Hull", RangeValue$1$reflection(float64_type)], ["WarpDrive", RangeValue$1$reflection(float64_type)], ["ImpulseDrive", RangeValue$1$reflection(float64_type)], ["ShieldGenerator", RangeValue$1$reflection(float64_type)], ["EnergyConverter", RangeValue$1$reflection(float64_type)], ["DeflectorDish", RangeValue$1$reflection(float64_type)], ["Phasers", RangeValue$1$reflection(float64_type)], ["TorpedoLaunchers", RangeValue$1$reflection(float64_type)], ["ImpulseMovementCost", float64_type], ["PhaserTemperatureCostPerGigawatt", float64_type], ["EnergyGeneratedPerUnitOfTravel", float64_type], ["EnergyCostPerUnitOfTravel", float64_type], ["HitPointsRepairedPerDay", float64_type]]);
}

export function Player_get_Default() {
    let withMax, withMax_1, withMax_2, withMax_3, withMax_4, withMax_5, withMax_9, withMax_10, withMax_11, withMax_12, withMax_13, withMax_14, withMax_15, withMax_16;
    return new Player(new GameWorldPosition(new Position(0, 0), new Position(0, 0)), true, (withMax = 5000, new RangeValue$1(withMax, 0, withMax)), (withMax_1 = 1500, new RangeValue$1(withMax_1, 0, withMax_1)), (withMax_2 = 1000, new RangeValue$1(withMax_2, 0, withMax_2)), (withMax_3 = 1500, new RangeValue$1(withMax_3, 0, withMax_3)), (withMax_4 = 1000, new RangeValue$1(withMax_4, 0, withMax_4)), (withMax_5 = 9, new RangeValue$1(withMax_5, 0, withMax_5)), (new RangeValue$1(750, 0, 400)), (new RangeValue$1(10000, 0, 0)), empty(), void 0, empty(), (new RangeValue$1(10, 1, 5)), (withMax_9 = 3000, new RangeValue$1(withMax_9, 0, withMax_9)), (withMax_10 = 1500, new RangeValue$1(withMax_10, 0, withMax_10)), (withMax_11 = 1500, new RangeValue$1(withMax_11, 0, withMax_11)), (withMax_12 = 1500, new RangeValue$1(withMax_12, 0, withMax_12)), (withMax_13 = 750, new RangeValue$1(withMax_13, 0, withMax_13)), (withMax_14 = 1000, new RangeValue$1(withMax_14, 0, withMax_14)), (withMax_15 = 750, new RangeValue$1(withMax_15, 0, withMax_15)), (withMax_16 = 1000, new RangeValue$1(withMax_16, 0, withMax_16)), 50, 1.5, 400, 650, 100);
}

export function Player__get_SystemsAsList(p) {
    return ofArray([["Hull", p.Hull], ["Warp Engines", p.WarpDrive], ["Impulse Drive", p.ImpulseDrive], ["Shield Generator", p.ShieldGenerator], ["Deflectors", p.DeflectorDish], ["Energy Convertor", p.EnergyConverter], ["Phasers", p.Phasers], ["Torpedo Launcher", p.TorpedoLaunchers]]);
}

export class GameObjectAttributes extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["EnemyAttributes", "StarbaseAttributes", "StarAttributes"];
    }
}

export function GameObjectAttributes$reflection() {
    return union_type("Game.Types.GameObjectAttributes", [], GameObjectAttributes, () => [[["Item", Enemy$reflection()]], [["Item", Starbase$reflection()]], []]);
}

export class GameObject extends Record {
    constructor(Position, Attributes) {
        super();
        this.Position = Position;
        this.Attributes = Attributes;
    }
}

export function GameObject$reflection() {
    return record_type("Game.Types.GameObject", [], GameObject, () => [["Position", GameWorldPosition$reflection()], ["Attributes", GameObjectAttributes$reflection()]]);
}

export function GameObject__get_Name(x) {
    const matchValue = x.Attributes;
    switch (matchValue.tag) {
        case 1: {
            return "Starbase";
        }
        case 2: {
            return "Star";
        }
        default: {
            const enemy = matchValue.fields[0];
            const matchValue_1 = enemy.ShipClass;
            switch (matchValue_1.tag) {
                case 1: {
                    return "Crusier";
                }
                case 2: {
                    return "Dreadnought";
                }
                default: {
                    return "Scout";
                }
            }
        }
    }
}

export function GameObject__get_IsEnemy(x) {
    if (x.Attributes.tag === 0) {
        return true;
    }
    else {
        return false;
    }
}

export class GameDifficulty extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["EasyDifficulty", "MediumDifficulty", "HardDifficulty"];
    }
}

export function GameDifficulty$reflection() {
    return union_type("Game.Types.GameDifficulty", [], GameDifficulty, () => [[], [], []]);
}

export class AiInstruction extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["FirePhasersAtPlayer", "ImpulseMoveTo", "WarpMoveTo", "TransferToShields", "Recharge"];
    }
}

export function AiInstruction$reflection() {
    return union_type("Game.Types.AiInstruction", [], AiInstruction, () => [[["Item", float64_type]], [["Item", GameWorldPosition$reflection()]], [["Item", GameWorldPosition$reflection()]], [], []]);
}

export class AiAction extends Record {
    constructor(GameObject, Instruction) {
        super();
        this.GameObject = GameObject;
        this.Instruction = Instruction;
    }
}

export function AiAction$reflection() {
    return record_type("Game.Types.AiAction", [], AiAction, () => [["GameObject", GameObject$reflection()], ["Instruction", AiInstruction$reflection()]]);
}

export class Game extends Record {
    constructor(Difficulty, GameObjects, Player, DiscoveredSectors, Stardate, Score, AiActions) {
        super();
        this.Difficulty = Difficulty;
        this.GameObjects = GameObjects;
        this.Player = Player;
        this.DiscoveredSectors = DiscoveredSectors;
        this.Stardate = Stardate;
        this.Score = (Score | 0);
        this.AiActions = AiActions;
    }
}

export function Game$reflection() {
    return record_type("Game.Types.Game", [], Game, () => [["Difficulty", GameDifficulty$reflection()], ["GameObjects", array_type(GameObject$reflection())], ["Player", Player$reflection()], ["DiscoveredSectors", class_type("Microsoft.FSharp.Collections.FSharpSet`1", [Position$reflection()])], ["Stardate", float64_type], ["Score", int32_type], ["AiActions", list_type(AiAction$reflection())]]);
}

export function Game_get_Empty() {
    return new Game(new GameDifficulty(1), new Array(0), Player_get_Default(), empty_1({
        Compare: compareSafe,
    }), 2872, 0, empty());
}

export class FiringResponse extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["TargetDamaged", "TargetDestroyed", "TargetMissed"];
    }
}

export function FiringResponse$reflection() {
    return union_type("Game.Types.FiringResponse", [], FiringResponse, () => [[["Item", Game$reflection()]], [["Item", Game$reflection()]], [["Item", Game$reflection()]]]);
}

export class GameEventMsg extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["FiredPhasersAtTarget", "TargetDestroyed", "PlayerWarped", "PlayerImpulsed"];
    }
}

export function GameEventMsg$reflection() {
    return union_type("Game.Types.GameEventMsg", [], GameEventMsg, () => [[], [["Item", GameWorldPosition$reflection()]], [["Item", bool_type]], [["Item", bool_type]]]);
}

export class UpdateGameStateMsg extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["SetPhaserPower", "SetWarpSpeed", "ToggleShields", "ImpulseTo", "WarpTo", "AddTarget", "RemoveTarget", "Dock", "Undock", "FirePhasersAtPosition", "BeginAiTurn"];
    }
}

export function UpdateGameStateMsg$reflection() {
    return union_type("Game.Types.UpdateGameStateMsg", [], UpdateGameStateMsg, () => [[["Item", float64_type]], [["Item", float64_type]], [], [["Item", GameWorldPosition$reflection()]], [["Item", GameWorldPosition$reflection()]], [["Item", GameWorldPosition$reflection()]], [["Item", GameWorldPosition$reflection()]], [["Item", GameWorldPosition$reflection()]], [], [["Item", GameWorldPosition$reflection()]], []]);
}

export class GameMsg extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["NewGame", "UpdateGameState", "GameEvent"];
    }
}

export function GameMsg$reflection() {
    return union_type("Game.Types.GameMsg", [], GameMsg, () => [[["Item", GameDifficulty$reflection()]], [["Item", UpdateGameStateMsg$reflection()]], [["Item", GameEventMsg$reflection()]]]);
}

