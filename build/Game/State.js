import { filter, singleton, append } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";
import { Position__get_AsString, GameObject__get_Name, FiringResponse, CaptainsLogItem, GameMsg, GameEventMsg, RangeValue$1, Game, Player as Player_3 } from "./Types.js";
import { Cmd_OfFunc_result, Cmd_none } from "../.fable/Fable.Elmish.3.1.0/cmd.fs.js";
import { Turn_generateAiActions, Weapons_calculatePhaserDamage, Weapons_calculatePhaserTemperatureIncrease, Enemy_energyImpact, Weapons_calculatePhaserPower, Weapons_removeTarget, Weapons_addTarget, Sensors_discover, Movement_move } from "./Rules.js";
import { GameWorld_replaceGameObject, GameWorld_removeGameObject, GameWorld_objectAtPosition } from "./Utils.js";
import { printf, toText } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/String.js";
import { equalsSafe } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Util.js";
import { createGame } from "./Factory.js";

export function appendToCaptainsLog(player, logItem) {
    let CaptainsLog;
    CaptainsLog = append(player.CaptainsLog, singleton(logItem));
    return new Player_3(player.Position, player.ShieldsRaised, player.Energy, player.ForeShields, player.PortShields, player.AftShields, player.StarboardShields, player.Torpedos, player.PhaserPower, player.PhaserTemperature, player.Targets, player.DockedWith, CaptainsLog, player.WarpSpeed, player.Hull, player.WarpDrive, player.ImpulseDrive, player.ShieldGenerator, player.EnergyConverter, player.DeflectorDish, player.Phasers, player.TorpedoLaunchers, player.ImpulseMovementCost, player.PhaserTemperatureCostPerGigawatt, player.EnergyGeneratedPerUnitOfTravel, player.EnergyCostPerUnitOfTravel, player.HitPointsRepairedPerDay);
}

export function updatePlayerState(msg, game) {
    let ShieldsRaised, PhaserPower, rt, newValue, rangeBoundValue, WarpSpeed, rt_1, newValue_1, rangeBoundValue_1, msg_2, arg0_4, logItem, msg_1, arg0_1, msg_4, arg0_9, logItem_1, DiscoveredSectors, msg_3, arg0_6, logItem_2, logItem_3, msg_6, msg_7, msg_5, arg0_19;
    const playerModel = game.Player;
    const updateGameWithPlayer = (cmd, player) => [new Game(game.Difficulty, game.GameObjects, player, game.DiscoveredSectors, game.Stardate, game.Score, game.AiActions), cmd];
    switch (msg.tag) {
        case 2: {
            return updateGameWithPlayer(Cmd_none(), (ShieldsRaised = (!playerModel.ShieldsRaised), new Player_3(playerModel.Position, ShieldsRaised, playerModel.Energy, playerModel.ForeShields, playerModel.PortShields, playerModel.AftShields, playerModel.StarboardShields, playerModel.Torpedos, playerModel.PhaserPower, playerModel.PhaserTemperature, playerModel.Targets, playerModel.DockedWith, playerModel.CaptainsLog, playerModel.WarpSpeed, playerModel.Hull, playerModel.WarpDrive, playerModel.ImpulseDrive, playerModel.ShieldGenerator, playerModel.EnergyConverter, playerModel.DeflectorDish, playerModel.Phasers, playerModel.TorpedoLaunchers, playerModel.ImpulseMovementCost, playerModel.PhaserTemperatureCostPerGigawatt, playerModel.EnergyGeneratedPerUnitOfTravel, playerModel.EnergyCostPerUnitOfTravel, playerModel.HitPointsRepairedPerDay)));
        }
        case 0: {
            const newPower = msg.fields[0];
            return updateGameWithPlayer(Cmd_none(), (PhaserPower = (rt = playerModel.PhaserPower, (newValue = newPower, (rangeBoundValue = ((newValue < rt.Min) ? rt.Min : ((newValue > rt.Max) ? rt.Max : newValue)), new RangeValue$1(rt.Max, rt.Min, rangeBoundValue)))), new Player_3(playerModel.Position, playerModel.ShieldsRaised, playerModel.Energy, playerModel.ForeShields, playerModel.PortShields, playerModel.AftShields, playerModel.StarboardShields, playerModel.Torpedos, PhaserPower, playerModel.PhaserTemperature, playerModel.Targets, playerModel.DockedWith, playerModel.CaptainsLog, playerModel.WarpSpeed, playerModel.Hull, playerModel.WarpDrive, playerModel.ImpulseDrive, playerModel.ShieldGenerator, playerModel.EnergyConverter, playerModel.DeflectorDish, playerModel.Phasers, playerModel.TorpedoLaunchers, playerModel.ImpulseMovementCost, playerModel.PhaserTemperatureCostPerGigawatt, playerModel.EnergyGeneratedPerUnitOfTravel, playerModel.EnergyCostPerUnitOfTravel, playerModel.HitPointsRepairedPerDay)));
        }
        case 1: {
            const newSpeed = msg.fields[0];
            return updateGameWithPlayer(Cmd_none(), (WarpSpeed = (rt_1 = playerModel.WarpSpeed, (newValue_1 = newSpeed, (rangeBoundValue_1 = ((newValue_1 < rt_1.Min) ? rt_1.Min : ((newValue_1 > rt_1.Max) ? rt_1.Max : newValue_1)), new RangeValue$1(rt_1.Max, rt_1.Min, rangeBoundValue_1)))), new Player_3(playerModel.Position, playerModel.ShieldsRaised, playerModel.Energy, playerModel.ForeShields, playerModel.PortShields, playerModel.AftShields, playerModel.StarboardShields, playerModel.Torpedos, playerModel.PhaserPower, playerModel.PhaserTemperature, playerModel.Targets, playerModel.DockedWith, playerModel.CaptainsLog, WarpSpeed, playerModel.Hull, playerModel.WarpDrive, playerModel.ImpulseDrive, playerModel.ShieldGenerator, playerModel.EnergyConverter, playerModel.DeflectorDish, playerModel.Phasers, playerModel.TorpedoLaunchers, playerModel.ImpulseMovementCost, playerModel.PhaserTemperatureCostPerGigawatt, playerModel.EnergyGeneratedPerUnitOfTravel, playerModel.EnergyCostPerUnitOfTravel, playerModel.HitPointsRepairedPerDay)));
        }
        case 3: {
            const newPosition = msg.fields[0];
            const matchValue = Movement_move(playerModel, game.GameObjects, newPosition);
            if (matchValue.tag === 1) {
                const errorMessage = matchValue.fields[0];
                return updateGameWithPlayer((msg_2 = (arg0_4 = (new GameEventMsg(3, false)), (new GameMsg(2, arg0_4))), Cmd_OfFunc_result(msg_2)), (logItem = (new CaptainsLogItem(1, errorMessage)), (appendToCaptainsLog(playerModel, logItem))));
            }
            else {
                const newPlayer = matchValue.fields[0];
                return updateGameWithPlayer((msg_1 = (arg0_1 = (new GameEventMsg(3, true)), (new GameMsg(2, arg0_1))), Cmd_OfFunc_result(msg_1)), newPlayer);
            }
        }
        case 4: {
            const newPosition_1 = msg.fields[0];
            const matchValue_1 = Movement_move(playerModel, game.GameObjects, newPosition_1);
            if (matchValue_1.tag === 1) {
                const errorMessage_1 = matchValue_1.fields[0];
                return updateGameWithPlayer((msg_4 = (arg0_9 = (new GameEventMsg(2, false)), (new GameMsg(2, arg0_9))), Cmd_OfFunc_result(msg_4)), (logItem_1 = (new CaptainsLogItem(1, errorMessage_1)), (appendToCaptainsLog(playerModel, logItem_1))));
            }
            else {
                const newPlayer_1 = matchValue_1.fields[0];
                return [(DiscoveredSectors = (Sensors_discover(newPlayer_1, game.DiscoveredSectors)), new Game(game.Difficulty, game.GameObjects, newPlayer_1, DiscoveredSectors, game.Stardate, game.Score, game.AiActions)), (msg_3 = (arg0_6 = (new GameEventMsg(2, true)), (new GameMsg(2, arg0_6))), Cmd_OfFunc_result(msg_3))];
            }
        }
        case 5: {
            const position = msg.fields[0];
            const matchValue_2 = GameWorld_objectAtPosition(game.GameObjects, position);
            if (matchValue_2 == null) {
                return updateGameWithPlayer(Cmd_none(), playerModel);
            }
            else {
                const gameObject = matchValue_2;
                const matchValue_3 = Weapons_addTarget(playerModel, gameObject);
                if (matchValue_3.tag === 1) {
                    const errorMessage_2 = matchValue_3.fields[0];
                    return updateGameWithPlayer(Cmd_none(), (logItem_2 = (new CaptainsLogItem(1, errorMessage_2)), (appendToCaptainsLog(playerModel, logItem_2))));
                }
                else {
                    const newPlayer_2 = matchValue_3.fields[0];
                    return updateGameWithPlayer(Cmd_none(), newPlayer_2);
                }
            }
        }
        case 6: {
            const position_1 = msg.fields[0];
            const matchValue_4 = Weapons_removeTarget(playerModel, position_1);
            if (matchValue_4.tag === 1) {
                const errorMessage_3 = matchValue_4.fields[0];
                return updateGameWithPlayer(Cmd_none(), (logItem_3 = (new CaptainsLogItem(1, errorMessage_3)), (appendToCaptainsLog(playerModel, logItem_3))));
            }
            else {
                const newPlayer_3 = matchValue_4.fields[0];
                return updateGameWithPlayer(Cmd_none(), newPlayer_3);
            }
        }
        case 9: {
            const position_2 = msg.fields[0];
            let matchValue_5;
            const game_1 = game;
            const targetPosition = position_2;
            const optionalTarget = GameWorld_objectAtPosition(game_1.GameObjects, targetPosition);
            if (optionalTarget == null) {
                let arg0_17;
                let Player_2;
                const inputRecord_1 = game_1.Player;
                let CaptainsLog_2;
                const list2_2 = singleton((new CaptainsLogItem(1, "Phasers missed!")));
                CaptainsLog_2 = append(game_1.Player.CaptainsLog, list2_2);
                Player_2 = (new Player_3(inputRecord_1.Position, inputRecord_1.ShieldsRaised, inputRecord_1.Energy, inputRecord_1.ForeShields, inputRecord_1.PortShields, inputRecord_1.AftShields, inputRecord_1.StarboardShields, inputRecord_1.Torpedos, inputRecord_1.PhaserPower, inputRecord_1.PhaserTemperature, inputRecord_1.Targets, inputRecord_1.DockedWith, CaptainsLog_2, inputRecord_1.WarpSpeed, inputRecord_1.Hull, inputRecord_1.WarpDrive, inputRecord_1.ImpulseDrive, inputRecord_1.ShieldGenerator, inputRecord_1.EnergyConverter, inputRecord_1.DeflectorDish, inputRecord_1.Phasers, inputRecord_1.TorpedoLaunchers, inputRecord_1.ImpulseMovementCost, inputRecord_1.PhaserTemperatureCostPerGigawatt, inputRecord_1.EnergyGeneratedPerUnitOfTravel, inputRecord_1.EnergyCostPerUnitOfTravel, inputRecord_1.HitPointsRepairedPerDay));
                arg0_17 = (new Game(game_1.Difficulty, game_1.GameObjects, Player_2, game_1.DiscoveredSectors, game_1.Stardate, game_1.Score, game_1.AiActions));
                matchValue_5 = (new FiringResponse(2, arg0_17));
            }
            else {
                const target = optionalTarget;
                let phaserEnergyHit;
                phaserEnergyHit = Weapons_calculatePhaserPower(game_1.Player);
                let modifiedTarget;
                modifiedTarget = Enemy_energyImpact(target, phaserEnergyHit);
                let modifiedPlayer;
                const inputRecord = game_1.Player;
                let Energy;
                const rt_2 = game_1.Player.Energy;
                const newValue_2 = game_1.Player.Energy.Current - game_1.Player.PhaserPower.Current;
                const rangeBoundValue_2 = (newValue_2 < rt_2.Min) ? rt_2.Min : ((newValue_2 > rt_2.Max) ? rt_2.Max : newValue_2);
                Energy = (new RangeValue$1(rt_2.Max, rt_2.Min, rangeBoundValue_2));
                let PhaserTemperature;
                const rt_3 = game_1.Player.PhaserTemperature;
                const newValue_3 = game_1.Player.PhaserTemperature.Current + (Weapons_calculatePhaserTemperatureIncrease(game_1.Player));
                const rangeBoundValue_3 = (newValue_3 < rt_3.Min) ? rt_3.Min : ((newValue_3 > rt_3.Max) ? rt_3.Max : newValue_3);
                PhaserTemperature = (new RangeValue$1(rt_3.Max, rt_3.Min, rangeBoundValue_3));
                let Phasers;
                const rt_4 = game_1.Player.Phasers;
                const newValue_4 = game_1.Player.Phasers.Current + (Weapons_calculatePhaserDamage(game_1.Player));
                const rangeBoundValue_4 = (newValue_4 < rt_4.Min) ? rt_4.Min : ((newValue_4 > rt_4.Max) ? rt_4.Max : newValue_4);
                Phasers = (new RangeValue$1(rt_4.Max, rt_4.Min, rangeBoundValue_4));
                modifiedPlayer = (new Player_3(inputRecord.Position, inputRecord.ShieldsRaised, Energy, inputRecord.ForeShields, inputRecord.PortShields, inputRecord.AftShields, inputRecord.StarboardShields, inputRecord.Torpedos, inputRecord.PhaserPower, PhaserTemperature, inputRecord.Targets, inputRecord.DockedWith, inputRecord.CaptainsLog, inputRecord.WarpSpeed, inputRecord.Hull, inputRecord.WarpDrive, inputRecord.ImpulseDrive, inputRecord.ShieldGenerator, inputRecord.EnergyConverter, inputRecord.DeflectorDish, Phasers, inputRecord.TorpedoLaunchers, inputRecord.ImpulseMovementCost, inputRecord.PhaserTemperatureCostPerGigawatt, inputRecord.EnergyGeneratedPerUnitOfTravel, inputRecord.EnergyCostPerUnitOfTravel, inputRecord.HitPointsRepairedPerDay));
                if (modifiedTarget == null) {
                    let logMessage_1;
                    const arg10_1 = GameObject__get_Name(target);
                    const arg20_1 = Position__get_AsString(target.Position.SectorPosition);
                    const clo1_1 = toText(printf("Destroyed %s at %s"));
                    const clo2_1 = clo1_1(arg10_1);
                    logMessage_1 = clo2_1(arg20_1);
                    let arg0_15;
                    const GameObjects_1 = GameWorld_removeGameObject(game_1.GameObjects, target);
                    let Player_1;
                    let CaptainsLog_1;
                    const list2_1 = singleton((new CaptainsLogItem(0, logMessage_1)));
                    CaptainsLog_1 = append(game_1.Player.CaptainsLog, list2_1);
                    let Targets;
                    Targets = filter((t) => (!equalsSafe(t, targetPosition)), modifiedPlayer.Targets);
                    Player_1 = (new Player_3(modifiedPlayer.Position, modifiedPlayer.ShieldsRaised, modifiedPlayer.Energy, modifiedPlayer.ForeShields, modifiedPlayer.PortShields, modifiedPlayer.AftShields, modifiedPlayer.StarboardShields, modifiedPlayer.Torpedos, modifiedPlayer.PhaserPower, modifiedPlayer.PhaserTemperature, Targets, modifiedPlayer.DockedWith, CaptainsLog_1, modifiedPlayer.WarpSpeed, modifiedPlayer.Hull, modifiedPlayer.WarpDrive, modifiedPlayer.ImpulseDrive, modifiedPlayer.ShieldGenerator, modifiedPlayer.EnergyConverter, modifiedPlayer.DeflectorDish, modifiedPlayer.Phasers, modifiedPlayer.TorpedoLaunchers, modifiedPlayer.ImpulseMovementCost, modifiedPlayer.PhaserTemperatureCostPerGigawatt, modifiedPlayer.EnergyGeneratedPerUnitOfTravel, modifiedPlayer.EnergyCostPerUnitOfTravel, modifiedPlayer.HitPointsRepairedPerDay));
                    arg0_15 = (new Game(game_1.Difficulty, GameObjects_1, Player_1, game_1.DiscoveredSectors, game_1.Stardate, game_1.Score, game_1.AiActions));
                    matchValue_5 = (new FiringResponse(1, arg0_15));
                }
                else {
                    const damagedTarget = modifiedTarget;
                    let logMessage;
                    const arg20 = GameObject__get_Name(target);
                    const arg30 = Position__get_AsString(target.Position.SectorPosition);
                    const clo1 = toText(printf("Hit of %.0f gigawatts on %s at %s"));
                    const clo2 = clo1(phaserEnergyHit);
                    const clo3 = clo2(arg20);
                    logMessage = clo3(arg30);
                    let arg0_13;
                    const GameObjects = GameWorld_replaceGameObject(game_1.GameObjects, damagedTarget);
                    let Player;
                    let CaptainsLog;
                    const list2 = singleton((new CaptainsLogItem(0, logMessage)));
                    CaptainsLog = append(game_1.Player.CaptainsLog, list2);
                    Player = (new Player_3(modifiedPlayer.Position, modifiedPlayer.ShieldsRaised, modifiedPlayer.Energy, modifiedPlayer.ForeShields, modifiedPlayer.PortShields, modifiedPlayer.AftShields, modifiedPlayer.StarboardShields, modifiedPlayer.Torpedos, modifiedPlayer.PhaserPower, modifiedPlayer.PhaserTemperature, modifiedPlayer.Targets, modifiedPlayer.DockedWith, CaptainsLog, modifiedPlayer.WarpSpeed, modifiedPlayer.Hull, modifiedPlayer.WarpDrive, modifiedPlayer.ImpulseDrive, modifiedPlayer.ShieldGenerator, modifiedPlayer.EnergyConverter, modifiedPlayer.DeflectorDish, modifiedPlayer.Phasers, modifiedPlayer.TorpedoLaunchers, modifiedPlayer.ImpulseMovementCost, modifiedPlayer.PhaserTemperatureCostPerGigawatt, modifiedPlayer.EnergyGeneratedPerUnitOfTravel, modifiedPlayer.EnergyCostPerUnitOfTravel, modifiedPlayer.HitPointsRepairedPerDay));
                    arg0_13 = (new Game(game_1.Difficulty, GameObjects, Player, game_1.DiscoveredSectors, game_1.Stardate, game_1.Score, game_1.AiActions));
                    matchValue_5 = (new FiringResponse(0, arg0_13));
                }
            }
            switch (matchValue_5.tag) {
                case 0: {
                    const updatedGame_1 = matchValue_5.fields[0];
                    return [updatedGame_1, (msg_6 = (new GameMsg(2, new GameEventMsg(0))), Cmd_OfFunc_result(msg_6))];
                }
                case 2: {
                    const updatedGame_2 = matchValue_5.fields[0];
                    return [updatedGame_2, (msg_7 = (new GameMsg(2, new GameEventMsg(0))), Cmd_OfFunc_result(msg_7))];
                }
                default: {
                    const updatedGame = matchValue_5.fields[0];
                    return [updatedGame, (msg_5 = (arg0_19 = (new GameEventMsg(1, position_2)), (new GameMsg(2, arg0_19))), Cmd_OfFunc_result(msg_5))];
                }
            }
        }
        case 10: {
            return [(Turn_generateAiActions(game)), Cmd_none()];
        }
        default: {
            return updateGameWithPlayer(Cmd_none(), playerModel);
        }
    }
}

export function update(msg, model) {
    switch (msg.tag) {
        case 1: {
            const subMsg = msg.fields[0];
            return updatePlayerState(subMsg, model);
        }
        case 2: {
            return [model, Cmd_none()];
        }
        default: {
            const difficulty = msg.fields[0];
            return [(createGame(difficulty)), Cmd_none()];
        }
    }
}

