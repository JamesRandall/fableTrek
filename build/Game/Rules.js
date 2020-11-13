import { Game, AiAction, AiInstruction, GameWorldPosition_get_Max, Position, GameWorldPosition, Position__DistanceTo_Z53A6D567, GameWorldPosition__get_AsString, Player, GameObject__get_IsEnemy, Player__get_SystemsAsList, GameObject, GameObjectAttributes, Enemy, RangeValue$1 } from "./Types.js";
import { rangeNumber, map, collect, delay, tryFind, sumBy } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";
import { rollDice, Position_findRandomAndVacantSectorPosition, Position_positionIsVacant, GameWorld_currentSectorObjects } from "./Utils.js";
import { ofSeq as ofSeq_1, empty, filter, singleton, append, length, tryFind as tryFind_1 } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";
import { compareSafe, min, comparePrimitives, max, equalsSafe } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Util.js";
import { FSharpResult$2 } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Choice.js";
import { printf, toText } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/String.js";
import { some } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Option.js";
import { union, ofSeq } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Set.js";

export function Enemy_energyImpact(gameObject, energy) {
    let Attributes, arg0, Shields, rt, newValue_2, rangeBoundValue, HitPoints, rt_1, newValue_3, rangeBoundValue_1;
    const matchValue = gameObject.Attributes;
    if (matchValue.tag === 0) {
        const enemy = matchValue.fields[0];
        let patternInput;
        const zero = 0;
        const newValue = enemy.Shields.Current - energy;
        patternInput = ((newValue < zero) ? [zero, Math.abs(newValue)] : [newValue, zero]);
        const shieldsOverflow = patternInput[1];
        const newShields = patternInput[0];
        let patternInput_1;
        const value_2 = (shieldsOverflow) * 1;
        const zero_1 = 0;
        const newValue_1 = enemy.HitPoints.Current - value_2;
        patternInput_1 = ((newValue_1 < zero_1) ? [zero_1, Math.abs(newValue_1)] : [newValue_1, zero_1]);
        const newHull = patternInput_1[0];
        if (newHull <= 0) {
            return void 0;
        }
        else {
            return Attributes = (arg0 = (Shields = (rt = enemy.Shields, (newValue_2 = newShields, (rangeBoundValue = ((newValue_2 < rt.Min) ? rt.Min : ((newValue_2 > rt.Max) ? rt.Max : newValue_2)), new RangeValue$1(rt.Max, rt.Min, rangeBoundValue)))), (HitPoints = (rt_1 = enemy.HitPoints, (newValue_3 = newHull, (rangeBoundValue_1 = ((newValue_3 < rt_1.Min) ? rt_1.Min : ((newValue_3 > rt_1.Max) ? rt_1.Max : newValue_3)), new RangeValue$1(rt_1.Max, rt_1.Min, rangeBoundValue_1)))), new Enemy(enemy.Energy, Shields, HitPoints, enemy.ShipClass, enemy.RechargeRate))), (new GameObjectAttributes(0, arg0))), new GameObject(gameObject.Position, Attributes);
        }
    }
    else {
        return gameObject;
    }
}

export function Damage_efficiencyFactor(systemHitPoints) {
    let percentage;
    const rt = systemHitPoints;
    percentage = (((rt.Current) - (rt.Min)) / ((rt.Max) - (rt.Min)));
    const efficiency = Math.log(percentage * 10) / Math.log(10);
    return efficiency;
}

export function Damage_inefficiencyFactor(systemHitPoints) {
    let percentage;
    const rt = systemHitPoints;
    percentage = (((rt.Current) - (rt.Min)) / ((rt.Max) - (rt.Min)));
    const inefficiency = 1 + (1 - (Math.log(percentage * 10) / Math.log(10)));
    return inefficiency;
}

export function Damage_systemRepairTime(player, systemHitPoints) {
    const hitPointsNeedingRepair = systemHitPoints.Max - systemHitPoints.Current;
    return hitPointsNeedingRepair / player.HitPointsRepairedPerDay;
}

export function Damage_totalRepairTime(player) {
    let source;
    return (source = Player__get_SystemsAsList(player), (sumBy((tupledArg) => {
        const hp = tupledArg[1];
        return hp.Max - hp.Current;
    }, source, {
        GetZero: () => 0,
        Add: (x, y) => (x + y),
    }))) / player.HitPointsRepairedPerDay;
}

export function Damage_canRepair(game) {
    let option, source;
    if (option = (source = (GameWorld_currentSectorObjects(game)), (tryFind(GameObject__get_IsEnemy, source))), (option == null)) {
        return (Damage_totalRepairTime(game.Player)) > 0;
    }
    else {
        return false;
    }
}

export function Weapons_canAddTarget(player, gameObject) {
    let option;
    if ((option = (tryFind_1((t) => equalsSafe(t, gameObject.Position), player.Targets)), (option == null)) ? (length(player.Targets) < 3) : false) {
        return GameObject__get_IsEnemy(gameObject);
    }
    else {
        return false;
    }
}

export function Weapons_addTarget(player, gameObject) {
    let Targets, arg10, clo1;
    if (Weapons_canAddTarget(player, gameObject)) {
        return new FSharpResult$2(0, (Targets = (append(player.Targets, singleton(gameObject.Position))), new Player(player.Position, player.ShieldsRaised, player.Energy, player.ForeShields, player.PortShields, player.AftShields, player.StarboardShields, player.Torpedos, player.PhaserPower, player.PhaserTemperature, Targets, player.DockedWith, player.CaptainsLog, player.WarpSpeed, player.Hull, player.WarpDrive, player.ImpulseDrive, player.ShieldGenerator, player.EnergyConverter, player.DeflectorDish, player.Phasers, player.TorpedoLaunchers, player.ImpulseMovementCost, player.PhaserTemperatureCostPerGigawatt, player.EnergyGeneratedPerUnitOfTravel, player.EnergyCostPerUnitOfTravel, player.HitPointsRepairedPerDay)));
    }
    else {
        return new FSharpResult$2(1, (arg10 = GameWorldPosition__get_AsString(gameObject.Position), (clo1 = toText(printf("Cannot target object at %s")), clo1(arg10))));
    }
}

export function Weapons_removeTarget(player, position) {
    let newPlayer;
    let Targets;
    Targets = filter((p) => (!equalsSafe(p, position)), player.Targets);
    newPlayer = (new Player(player.Position, player.ShieldsRaised, player.Energy, player.ForeShields, player.PortShields, player.AftShields, player.StarboardShields, player.Torpedos, player.PhaserPower, player.PhaserTemperature, Targets, player.DockedWith, player.CaptainsLog, player.WarpSpeed, player.Hull, player.WarpDrive, player.ImpulseDrive, player.ShieldGenerator, player.EnergyConverter, player.DeflectorDish, player.Phasers, player.TorpedoLaunchers, player.ImpulseMovementCost, player.PhaserTemperatureCostPerGigawatt, player.EnergyGeneratedPerUnitOfTravel, player.EnergyCostPerUnitOfTravel, player.HitPointsRepairedPerDay));
    if (length(newPlayer.Targets) === length(player.Targets)) {
        return new FSharpResult$2(1, "No target to remove");
    }
    else {
        return new FSharpResult$2(0, newPlayer);
    }
}

export function Weapons_canFirePhasers(player) {
    let value;
    if (length(player.Targets) > 0) {
        return (player.PhaserPower.Current * (value = (length(player.Targets) | 0), (value))) <= player.Energy.Current;
    }
    else {
        return false;
    }
}

export function Weapons_canFireTorpedoes(player) {
    if (length(player.Targets) > 0) {
        return length(player.Targets) < (player.Torpedos.Current);
    }
    else {
        return false;
    }
}

export function Weapons_calculatePhaserPower(player) {
    return (Damage_efficiencyFactor(player.Phasers)) * player.PhaserPower.Current;
}

export function Weapons_calculatePhaserTemperatureIncrease(player) {
    let rt;
    if (player.Phasers.Current <= 0) {
        return player.PhaserTemperature.Max;
    }
    else {
        const damageModifier = 1 + (1 - (Math.log((rt = player.Phasers, ((rt.Current) - (rt.Min)) / ((rt.Max) - (rt.Min))) * 1000) / Math.log(1000)));
        return ((player.PhaserPower.Current) * player.PhaserTemperatureCostPerGigawatt) * damageModifier;
    }
}

export function Weapons_calculatePhaserDamage(player) {
    return 0;
}

export function Movement_isImpulseMove(player, newPosition) {
    return equalsSafe(player.Position.GalacticPosition, newPosition.GalacticPosition);
}

export function Movement_netEnergyRequirementsForWarp(player, newPosition) {
    const energyRequirementsForMove = (player_1, newPosition_1) => {
        let clo1, clo2;
        if (Movement_isImpulseMove(player_1, newPosition_1)) {
            const distance = Position__DistanceTo_Z53A6D567(player_1.Position.SectorPosition, newPosition_1.SectorPosition);
            const energyCost = (player_1.ImpulseMovementCost * (Damage_inefficiencyFactor(player_1.ImpulseDrive))) * distance;
            return energyCost;
        }
        else {
            const distance_1 = Position__DistanceTo_Z53A6D567(player_1.Position.GalacticPosition, newPosition_1.GalacticPosition);
            const energyCostAtSpeed = player_1.EnergyCostPerUnitOfTravel * (1 - (Math.log((11 - (player_1.WarpSpeed.Current)) / 2) / Math.log(5.5)));
            const energyCostAtSpeedAndDamage = energyCostAtSpeed * Damage_inefficiencyFactor(player_1.WarpDrive);
            const totalEnergyCost = distance_1 * energyCostAtSpeedAndDamage;
            console.log(some((clo1 = toText(printf("Total energy cost at warp %f: %f")), clo2 = clo1(player_1.WarpSpeed.Current), clo2(totalEnergyCost))));
            return totalEnergyCost;
        }
    };
    const energyGeneratedByEnergyConverter = (player_2, newPosition_2) => {
        let clo1_1, clo2_1;
        if (Movement_isImpulseMove(player_2, newPosition_2)) {
            return 0;
        }
        else {
            const distance_2 = Position__DistanceTo_Z53A6D567(player_2.Position.GalacticPosition, newPosition_2.GalacticPosition);
            const energyGeneratedAtSpeed = player_2.EnergyGeneratedPerUnitOfTravel * (Math.log(11 - (player_2.WarpSpeed.Current)) / Math.log(11));
            const energyGeneratedAtSpeedAndDamage = energyGeneratedAtSpeed * Damage_efficiencyFactor(player_2.EnergyConverter);
            const totalEnergyGenerated = distance_2 * energyGeneratedAtSpeedAndDamage;
            console.log(some((clo1_1 = toText(printf("Total energy generated at warp %f: %f")), clo2_1 = clo1_1(player_2.WarpSpeed.Current), clo2_1(totalEnergyGenerated))));
            return totalEnergyGenerated;
        }
    };
    return energyRequirementsForMove(player, newPosition) - energyGeneratedByEnergyConverter(player, newPosition);
}

export function Movement_canMove(player, newPosition) {
    const totalEnergyCost = Movement_netEnergyRequirementsForWarp(player, newPosition);
    return (player.Energy.Current - totalEnergyCost) >= 0;
}

export function Movement_move(player, gameObjects, newPosition) {
    let Energy, rt, newValue, rangeBoundValue, Energy_1, rt_1, newValue_1, rangeBoundValue_1;
    const energyRequirements = Movement_netEnergyRequirementsForWarp(player, newPosition);
    if (Movement_isImpulseMove(player, newPosition)) {
        const matchValue = [player.Energy.Current > energyRequirements, Position_positionIsVacant(gameObjects, newPosition)];
        if (matchValue[0]) {
            if (matchValue[1]) {
                return new FSharpResult$2(0, (Energy = (rt = player.Energy, (newValue = (player.Energy.Current - energyRequirements), (rangeBoundValue = ((newValue < rt.Min) ? rt.Min : ((newValue > rt.Max) ? rt.Max : newValue)), new RangeValue$1(rt.Max, rt.Min, rangeBoundValue)))), new Player(newPosition, player.ShieldsRaised, Energy, player.ForeShields, player.PortShields, player.AftShields, player.StarboardShields, player.Torpedos, player.PhaserPower, player.PhaserTemperature, player.Targets, player.DockedWith, player.CaptainsLog, player.WarpSpeed, player.Hull, player.WarpDrive, player.ImpulseDrive, player.ShieldGenerator, player.EnergyConverter, player.DeflectorDish, player.Phasers, player.TorpedoLaunchers, player.ImpulseMovementCost, player.PhaserTemperatureCostPerGigawatt, player.EnergyGeneratedPerUnitOfTravel, player.EnergyCostPerUnitOfTravel, player.HitPointsRepairedPerDay)));
            }
            else {
                return new FSharpResult$2(1, "Object blocking move");
            }
        }
        else {
            return new FSharpResult$2(1, "Insufficient energy to move to that location");
        }
    }
    else {
        const matchValue_1 = player.Energy.Current > energyRequirements;
        if (matchValue_1) {
            let confirmedVacantPosition;
            if (Position_positionIsVacant(gameObjects, newPosition)) {
                confirmedVacantPosition = newPosition;
            }
            else {
                const SectorPosition = Position_findRandomAndVacantSectorPosition(gameObjects, newPosition.GalacticPosition);
                confirmedVacantPosition = (new GameWorldPosition(newPosition.GalacticPosition, SectorPosition));
            }
            return new FSharpResult$2(0, (Energy_1 = (rt_1 = player.Energy, (newValue_1 = (player.Energy.Current - energyRequirements), (rangeBoundValue_1 = ((newValue_1 < rt_1.Min) ? rt_1.Min : ((newValue_1 > rt_1.Max) ? rt_1.Max : newValue_1)), new RangeValue$1(rt_1.Max, rt_1.Min, rangeBoundValue_1)))), new Player(confirmedVacantPosition, player.ShieldsRaised, Energy_1, player.ForeShields, player.PortShields, player.AftShields, player.StarboardShields, player.Torpedos, player.PhaserPower, player.PhaserTemperature, player.Targets, player.DockedWith, player.CaptainsLog, player.WarpSpeed, player.Hull, player.WarpDrive, player.ImpulseDrive, player.ShieldGenerator, player.EnergyConverter, player.DeflectorDish, player.Phasers, player.TorpedoLaunchers, player.ImpulseMovementCost, player.PhaserTemperatureCostPerGigawatt, player.EnergyGeneratedPerUnitOfTravel, player.EnergyCostPerUnitOfTravel, player.HitPointsRepairedPerDay)));
        }
        else {
            return new FSharpResult$2(1, "Insufficient energy to move to that location");
        }
    }
}

export function Sensors_discover(player, alreadyFound) {
    const pos = player.Position.GalacticPosition;
    let newPositions;
    const elements = delay(() => {
        let value_3, value_4, value_5;
        return collect((y) => {
            let value, value_1, value_2;
            return map((x) => (new Position(x * 1, y * 1)), rangeNumber(max(comparePrimitives, (value = ((pos.X - 1) | 0), (value)), 0), 1, min(comparePrimitives, (value_1 = ((pos.X + 1) | 0), (value_1)), (value_2 = (GameWorldPosition_get_Max().GalacticPosition.X | 0), (value_2)))));
        }, rangeNumber(max(comparePrimitives, (value_3 = ((pos.Y - 1) | 0), (value_3)), 0), 1, min(comparePrimitives, (value_4 = ((pos.Y + 1) | 0), (value_4)), (value_5 = (GameWorldPosition_get_Max().GalacticPosition.Y | 0), (value_5)))));
    });
    newPositions = ofSeq(elements, {
        Compare: compareSafe,
    });
    return union(newPositions, alreadyFound);
}

export function Turn_generateAiActions(game) {
    let aiActions;
    let source_1;
    source_1 = collect((go) => {
        let rt, Instruction_1, Instruction_2, arg0_1, Instruction_3;
        const diceRoll = rollDice() | 0;
        const pgp = game.Player.Position.GalacticPosition;
        const matchValue = [go.Attributes, equalsSafe(go.Position.GalacticPosition, pgp)];
        if (matchValue[0].tag === 0) {
            if (matchValue[1]) {
                const enemy = matchValue[0].fields[0];
                if ((diceRoll < 10) ? ((rt = enemy.Shields, ((rt.Current) - (rt.Min)) / ((rt.Max) - (rt.Min))) < 0.6) : false) {
                    return singleton(new AiAction(go, new AiInstruction(3)));
                }
                else if (diceRoll < 75) {
                    return singleton((Instruction_1 = (new AiInstruction(0, 100)), new AiAction(go, Instruction_1)));
                }
                else if (diceRoll < 95) {
                    const newPosition = Position_findRandomAndVacantSectorPosition(game.GameObjects, go.Position.GalacticPosition);
                    return singleton((Instruction_2 = (arg0_1 = (new GameWorldPosition(game.Player.Position.GalacticPosition, newPosition)), (new AiInstruction(1, arg0_1))), new AiAction(go, Instruction_2)));
                }
                else {
                    return empty();
                }
            }
            else {
                const enemy_1 = matchValue[0].fields[0];
                if (diceRoll < 2) {
                    const newPosition_1 = new GameWorldPosition(pgp, Position_findRandomAndVacantSectorPosition(game.GameObjects, pgp));
                    return singleton((Instruction_3 = (new AiInstruction(2, newPosition_1)), new AiAction(go, Instruction_3)));
                }
                else {
                    return empty();
                }
            }
        }
        else {
            return empty();
        }
    }, game.GameObjects);
    aiActions = ofSeq_1(source_1);
    return new Game(game.Difficulty, game.GameObjects, game.Player, game.DiscoveredSectors, game.Stardate, game.Score, aiActions);
}

