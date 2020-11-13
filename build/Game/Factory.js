import { empty, append, fold, rangeNumber } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";
import { Position_findRandomAndVacantGalacticPosition } from "./Utils.js";
import { Game$reflection, Game, Game_get_Empty, Player, Player_get_Default, EnemyType, Starbase, Enemy, RangeValue$1, GameObjectAttributes, GameObject } from "./Types.js";
import { compareSafe, partialApply } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Util.js";
import { empty as empty_1 } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Set.js";
import { Sensors_discover } from "./Rules.js";
import { FSharpResult$2 } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Choice.js";
import { Auto_fromString_Z5CB6BD } from "../.fable/Thoth.Json.4.0.0/Decode.fs.js";

export function createGame(difficulty) {
    let alreadyFound;
    const createGameworldObjects = (numberOfObjects, createAttributes, previousObjects) => {
        const source = rangeNumber(0, 1, numberOfObjects);
        return fold((foldedPreviousObjects, _arg1) => {
            const newStar = new GameObject(Position_findRandomAndVacantGalacticPosition(foldedPreviousObjects), createAttributes());
            return append([newStar], foldedPreviousObjects);
        }, previousObjects, source);
    };
    const createStars = partialApply(1, createGameworldObjects, [200, () => (new GameObjectAttributes(2))]);
    const createEnemies = (enemyShipClass) => {
        const patternInput = (enemyShipClass.tag === 1) ? ((difficulty.tag === 1) ? [20, 2000, 2500, 1750, 200] : ((difficulty.tag === 2) ? [25, 2000, 3000, 2000, 250] : [15, 2000, 2000, 1500, 200])) : ((enemyShipClass.tag === 2) ? ((difficulty.tag === 1) ? [10, 2000, 2500, 1750, 200] : ((difficulty.tag === 2) ? [15, 2000, 3000, 2000, 250] : [5, 2000, 2000, 1500, 200])) : ((difficulty.tag === 1) ? [60, 1000, 1250, 1200, 120] : ((difficulty.tag === 2) ? [70, 1000, 1500, 1500, 140] : [50, 1000, 1000, 1000, 100])));
        const rechargeRate = patternInput[4];
        const numberOf = patternInput[0] | 0;
        const maxShields = patternInput[2];
        const maxHitPoints = patternInput[3];
        const maxEnergy = patternInput[1];
        const createEnemy = () => {
            let arg0;
            let Energy;
            const withMax = maxEnergy;
            Energy = (new RangeValue$1(withMax, 0, withMax));
            let Shields;
            const withMax_1 = maxShields;
            Shields = (new RangeValue$1(withMax_1, 0, withMax_1));
            let HitPoints;
            const withMax_2 = maxHitPoints;
            HitPoints = (new RangeValue$1(withMax_2, 0, withMax_2));
            arg0 = (new Enemy(Energy, Shields, HitPoints, enemyShipClass, rechargeRate));
            return new GameObjectAttributes(0, arg0);
        };
        return partialApply(1, createGameworldObjects, [numberOf, createEnemy]);
    };
    let createStarbases;
    const patternInput_1 = (difficulty.tag === 1) ? [4, 20000, 5000, 7500, 500] : ((difficulty.tag === 2) ? [3, 20000, 5000, 7500, 500] : [5, 20000, 5000, 7500, 500]);
    const rechargeRate_1 = patternInput_1[4];
    const numberOf_1 = patternInput_1[0] | 0;
    const maxShields_1 = patternInput_1[2];
    const maxHitPoints_1 = patternInput_1[3];
    const maxEnergy_1 = patternInput_1[1];
    const createStarbase = () => {
        let withMax_3, withMax_4, withMax_5;
        const arg0_1 = new Starbase((withMax_3 = maxEnergy_1, new RangeValue$1(withMax_3, 0, withMax_3)), (withMax_4 = maxShields_1, new RangeValue$1(withMax_4, 0, withMax_4)), (withMax_5 = maxHitPoints_1, new RangeValue$1(withMax_5, 0, withMax_5)), rechargeRate_1);
        return new GameObjectAttributes(1, arg0_1);
    };
    createStarbases = partialApply(1, createGameworldObjects, [numberOf_1, createStarbase]);
    let gameObjects;
    const source_1 = createEnemies(new EnemyType(2))(createEnemies(new EnemyType(1))(createEnemies(new EnemyType(0))(createStarbases(createStars(empty())))));
    gameObjects = Array.from(source_1);
    let gamePlayer;
    const inputRecord = Player_get_Default();
    const Position = Position_findRandomAndVacantGalacticPosition(gameObjects);
    let ForeShields;
    const inputRecord_1 = Player_get_Default().ForeShields;
    ForeShields = (new RangeValue$1(inputRecord_1.Max, inputRecord_1.Min, 900));
    gamePlayer = (new Player(Position, inputRecord.ShieldsRaised, inputRecord.Energy, ForeShields, inputRecord.PortShields, inputRecord.AftShields, inputRecord.StarboardShields, inputRecord.Torpedos, inputRecord.PhaserPower, inputRecord.PhaserTemperature, inputRecord.Targets, inputRecord.DockedWith, inputRecord.CaptainsLog, inputRecord.WarpSpeed, inputRecord.Hull, inputRecord.WarpDrive, inputRecord.ImpulseDrive, inputRecord.ShieldGenerator, inputRecord.EnergyConverter, inputRecord.DeflectorDish, inputRecord.Phasers, inputRecord.TorpedoLaunchers, inputRecord.ImpulseMovementCost, inputRecord.PhaserTemperatureCostPerGigawatt, inputRecord.EnergyGeneratedPerUnitOfTravel, inputRecord.EnergyCostPerUnitOfTravel, inputRecord.HitPointsRepairedPerDay));
    const inputRecord_2 = Game_get_Empty();
    return new Game(difficulty, gameObjects, gamePlayer, (alreadyFound = empty_1({
        Compare: compareSafe,
    }), (Sensors_discover(gamePlayer, alreadyFound))), inputRecord_2.Stardate, inputRecord_2.Score, inputRecord_2.AiActions);
}

export function canLoad() {
    let value;
    return !(value = localStorage.getItem("currentGame"), (value == null));
}

export function tryLoad() {
    const possibleSavedGame = localStorage.getItem("currentGame");
    let matchValue;
    matchValue = (possibleSavedGame == null);
    if (matchValue) {
        return new FSharpResult$2(1, "No saved game");
    }
    else {
        return Auto_fromString_Z5CB6BD(possibleSavedGame, void 0, void 0, {
            ResolveType: Game$reflection,
        });
    }
}

