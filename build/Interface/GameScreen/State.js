import { singleton } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/AsyncBuilder.js";
import { sleep } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Async.js";
import { explosionDurationMs } from "../Animation.js";
import { GameRequestMsg, ShortRangeScannerMenu, Model, Model_get_Empty, GameScreenMsg } from "./Types.js";
import { Cmd_OfFunc_result, Cmd_OfAsync_start, Cmd_OfAsyncWith_result, Cmd_none } from "../../.fable/Fable.Elmish.3.1.0/cmd.fs.js";
import { ofSeq, filter, singleton as singleton_1, append } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";
import { printf, toText } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/String.js";
import { some } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Option.js";
import { equalsSafe } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Util.js";
import { skip, tryHead } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";

function sleepThenCompleteExplosion(explosion) {
    const builder$0040 = singleton;
    return builder$0040.Delay(() => builder$0040.Bind(sleep(explosionDurationMs), () => builder$0040.Return(new GameScreenMsg(7, explosion))));
}

export function init() {
    return [Model_get_Empty(), Cmd_none()];
}

export function update(msg, model, game) {
    let ShortRangeScannerMenuItems, Explosions, task, clo1, Explosions_1, FiringTargets_1, source_2, msg_3, arg0_2, msg_4, arg0_4, WarpDestination;
    switch (msg.tag) {
        case 11: {
            return [new Model(model.IsUiDisabled, model.IsLongRangeScannerVisible, model.ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, model.Explosions, void 0, model.IsWarping, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn), Cmd_none()];
        }
        case 0: {
            return [new Model(model.IsUiDisabled, true, model.ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, model.Explosions, model.WarpDestination, model.IsWarping, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn), Cmd_none()];
        }
        case 1: {
            return [new Model(model.IsUiDisabled, false, model.ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, model.Explosions, void 0, false, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn), Cmd_none()];
        }
        case 14: {
            return [new Model(model.IsUiDisabled, model.IsLongRangeScannerVisible, model.ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, model.Explosions, model.WarpDestination, model.IsWarping, true, model.IsCaptainsLogVisible, model.IsEnemyTurn), Cmd_none()];
        }
        case 15: {
            return [new Model(model.IsUiDisabled, model.IsLongRangeScannerVisible, model.ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, model.Explosions, model.WarpDestination, model.IsWarping, false, model.IsCaptainsLogVisible, model.IsEnemyTurn), Cmd_none()];
        }
        case 2: {
            const position = msg.fields[0][0];
            const menuItems = msg.fields[0][1];
            return [(ShortRangeScannerMenuItems = (new ShortRangeScannerMenu(position, menuItems)), new Model(model.IsUiDisabled, model.IsLongRangeScannerVisible, ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, model.Explosions, model.WarpDestination, model.IsWarping, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn)), Cmd_none()];
        }
        case 3: {
            return [new Model(model.IsUiDisabled, model.IsLongRangeScannerVisible, void 0, model.FiringTargets, model.CurrentTarget, model.Explosions, model.WarpDestination, model.IsWarping, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn), Cmd_none()];
        }
        case 8: {
            return [new Model(true, model.IsLongRangeScannerVisible, model.ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, model.Explosions, model.WarpDestination, model.IsWarping, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn), Cmd_none()];
        }
        case 9: {
            return [new Model(false, model.IsLongRangeScannerVisible, model.ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, model.Explosions, model.WarpDestination, model.IsWarping, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn), Cmd_none()];
        }
        case 6: {
            const explosion = msg.fields[0];
            return [(Explosions = (append(singleton_1(explosion), model.Explosions)), new Model(model.IsUiDisabled, model.IsLongRangeScannerVisible, model.ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, Explosions, model.WarpDestination, model.IsWarping, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn)), (task = sleepThenCompleteExplosion(explosion), Cmd_OfAsyncWith_result((x) => {
                Cmd_OfAsync_start(x);
            }, task))];
        }
        case 7: {
            const explosion_1 = msg.fields[0];
            console.log(some((clo1 = toText(printf("%O")), clo1(model.Explosions))));
            return [(Explosions_1 = (filter((e) => (!equalsSafe(e, explosion_1)), model.Explosions)), new Model(model.IsUiDisabled, model.IsLongRangeScannerVisible, model.ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, Explosions_1, model.WarpDestination, model.IsWarping, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn)), Cmd_OfFunc_result(new GameScreenMsg(5))];
        }
        case 4: {
            return [new Model(model.IsUiDisabled, model.IsLongRangeScannerVisible, model.ShortRangeScannerMenuItems, game.Player.Targets, model.CurrentTarget, model.Explosions, model.WarpDestination, model.IsWarping, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn), Cmd_OfFunc_result(new GameScreenMsg(5))];
        }
        case 5: {
            let matchValue;
            matchValue = tryHead(model.FiringTargets);
            if (matchValue == null) {
                return [new Model(model.IsUiDisabled, model.IsLongRangeScannerVisible, model.ShortRangeScannerMenuItems, model.FiringTargets, void 0, model.Explosions, model.WarpDestination, model.IsWarping, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn), Cmd_none()];
            }
            else {
                const nextTarget = matchValue;
                return [(FiringTargets_1 = (source_2 = (skip(1, model.FiringTargets)), (ofSeq(source_2))), new Model(model.IsUiDisabled, model.IsLongRangeScannerVisible, model.ShortRangeScannerMenuItems, FiringTargets_1, nextTarget, model.Explosions, model.WarpDestination, model.IsWarping, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn)), (msg_3 = (arg0_2 = (new GameRequestMsg(2, nextTarget)), (new GameScreenMsg(16, arg0_2))), Cmd_OfFunc_result(msg_3))];
            }
        }
        case 12: {
            const position_1 = msg.fields[0];
            return [new Model(true, model.IsLongRangeScannerVisible, model.ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, model.Explosions, model.WarpDestination, true, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn), (msg_4 = (arg0_4 = (new GameRequestMsg(1, position_1)), (new GameScreenMsg(16, arg0_4))), Cmd_OfFunc_result(msg_4))];
        }
        case 13: {
            return [model.IsWarping ? (new Model(false, false, model.ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, model.Explosions, void 0, false, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn)) : model, Cmd_none()];
        }
        case 16: {
            return [model, Cmd_none()];
        }
        default: {
            const p = msg.fields[0];
            return [(WarpDestination = (p), new Model(model.IsUiDisabled, model.IsLongRangeScannerVisible, model.ShortRangeScannerMenuItems, model.FiringTargets, model.CurrentTarget, model.Explosions, WarpDestination, model.IsWarping, model.IsDamageControlVisible, model.IsCaptainsLogVisible, model.IsEnemyTurn)), Cmd_none()];
        }
    }
}

