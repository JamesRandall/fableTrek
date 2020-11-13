import { Msg, Model_get_Empty, Model } from "./App.Types.js";
import { update as update_1, init as init_1 } from "./Interface/GameScreen/State.js";
import { init as init_2 } from "./Interface/StartScreen/State.js";
import { some } from "./.fable/fable-library.3.0.0-nagareyama-rc-005/Option.js";
import { modifyLocation, Page, modifyUrl } from "./Router.js";
import { createGame, tryLoad } from "./Game/Factory.js";
import { GameWorldPosition, GameMsg, UpdateGameStateMsg, GameDifficulty } from "./Game/Types.js";
import { singleton } from "./.fable/fable-library.3.0.0-nagareyama-rc-005/AsyncBuilder.js";
import { sleep } from "./.fable/fable-library.3.0.0-nagareyama-rc-005/Async.js";
import { warpAnimationDurationMs, phaserAnimationDurationMs, scannerAnimationDurationMs } from "./Interface/Animation.js";
import { Explosion, GameScreenMsg } from "./Interface/GameScreen/Types.js";
import { Cmd_none, Cmd_OfAsync_start, Cmd_OfAsyncWith_result, Cmd_batch, Cmd_OfFunc_result, Cmd_map } from "./.fable/Fable.Elmish.3.1.0/cmd.fs.js";
import { ofArray } from "./.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";
import { update as update_2 } from "./Game/State.js";
import { skip, tryHead } from "./.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";

export function urlUpdate(result, model) {
    if (result != null) {
        const page = result;
        const model_1 = new Model(page, model.CurrentGame, model.StartScreen, model.GameScreen);
        if (page.tag === 1) {
            const patternInput_1 = init_1();
            const subModel_1 = patternInput_1[0];
            const subCmd_1 = patternInput_1[1];
            return [new Model(model_1.CurrentPage, model_1.CurrentGame, model_1.StartScreen, subModel_1), subCmd_1];
        }
        else {
            const patternInput = init_2();
            const subModel = patternInput[0];
            const subCmd = patternInput[1];
            return [new Model(model_1.CurrentPage, model_1.CurrentGame, subModel, model_1.GameScreen), subCmd];
        }
    }
    else {
        console.error(some("Error parsing url: " + window.location.href));
        return [model, modifyUrl(model.CurrentPage)];
    }
}

export function init(result) {
    const patternInput = urlUpdate(result, Model_get_Empty());
    const model = patternInput[0];
    const cmd = patternInput[1];
    let game_1;
    if (model.CurrentPage.tag === 1) {
        const gameResult = tryLoad();
        if (gameResult.tag === 1) {
            game_1 = createGame(new GameDifficulty(0));
        }
        else {
            const game = gameResult.fields[0];
            game_1 = game;
        }
    }
    else {
        game_1 = (void 0);
    }
    return [new Model(model.CurrentPage, game_1, model.StartScreen, model.GameScreen), cmd];
}

function animationSleep() {
    const builder$0040 = singleton;
    return builder$0040.Delay(() => builder$0040.Bind(sleep(scannerAnimationDurationMs), () => builder$0040.Return((new Msg(3, new GameScreenMsg(9))))));
}

const sleepThenFireNextPhasers = (() => {
    const builder$0040 = singleton;
    return builder$0040.Delay(() => builder$0040.Bind(sleep(phaserAnimationDurationMs), () => builder$0040.Return((new Msg(3, new GameScreenMsg(5))))));
})();

const endWarpAnimation = (() => {
    const builder$0040 = singleton;
    return builder$0040.Delay(() => builder$0040.Bind(sleep(warpAnimationDurationMs), () => builder$0040.Return((new Msg(3, new GameScreenMsg(13))))));
})();

export function mapUiRequestToGameCommand(requestMsg, uiModel, game) {
    let msg_1, arg0_4, msg_2, arg0_7, arg0_6, msg;
    switch (requestMsg.tag) {
        case 2: {
            const position = requestMsg.fields[0];
            return Cmd_map((arg0_2) => (new Msg(2, arg0_2)), (msg_1 = (arg0_4 = (new UpdateGameStateMsg(9, position)), (new GameMsg(1, arg0_4))), Cmd_OfFunc_result(msg_1)));
        }
        case 1: {
            const position_1 = requestMsg.fields[0];
            return Cmd_map((arg0_5) => (new Msg(2, arg0_5)), (msg_2 = (arg0_7 = (arg0_6 = (new GameWorldPosition(position_1, game.Player.Position.SectorPosition)), (new UpdateGameStateMsg(4, arg0_6))), (new GameMsg(1, arg0_7))), (Cmd_OfFunc_result(msg_2))));
        }
        default: {
            return Cmd_map((arg0) => (new Msg(2, arg0)), (msg = (new GameMsg(1, new UpdateGameStateMsg(2))), Cmd_OfFunc_result(msg)));
        }
    }
}

export function mapGameEventToUiCommand(returnedCmd, gameCmd) {
    let msg, arg0_2, task_1;
    switch (gameCmd.tag) {
        case 2: {
            const bridgeMsg = gameCmd.fields[0];
            switch (bridgeMsg.tag) {
                case 1: {
                    const position = bridgeMsg.fields[0];
                    return Cmd_map((arg0) => (new Msg(3, arg0)), (msg = (arg0_2 = (new Explosion(0, position)), (new GameScreenMsg(6, arg0_2))), Cmd_OfFunc_result(msg)));
                }
                case 3: {
                    return Cmd_batch(ofArray([Cmd_map((arg0_3) => (new Msg(3, arg0_3)), Cmd_OfFunc_result(new GameScreenMsg(3))), (task_1 = animationSleep(), Cmd_OfAsyncWith_result((x_1) => {
                        Cmd_OfAsync_start(x_1);
                    }, task_1))]));
                }
                case 2: {
                    return Cmd_OfAsyncWith_result((x_2) => {
                        Cmd_OfAsync_start(x_2);
                    }, endWarpAnimation);
                }
                default: {
                    return Cmd_OfAsyncWith_result((x) => {
                        Cmd_OfAsync_start(x);
                    }, sleepThenFireNextPhasers);
                }
            }
        }
        case 1: {
            const gameStateMsg = gameCmd.fields[0];
            switch (gameStateMsg.tag) {
                case 5:
                case 6: {
                    return Cmd_map((arg0_4) => (new Msg(3, arg0_4)), Cmd_OfFunc_result(new GameScreenMsg(3)));
                }
                default: {
                    return Cmd_map((arg0_5) => (new Msg(2, arg0_5)), returnedCmd);
                }
            }
        }
        default: {
            return Cmd_map((arg0_6) => (new Msg(2, arg0_6)), returnedCmd);
        }
    }
}

export function update(msg, model) {
    let CurrentGame, arg0, msg_1, msg_2, arg0_3, source_2;
    const matchValue = [msg, model];
    let pattern_matching_result, extractedModel, subMsg, extractedGame, extractedModel_1, subMsg_1, extractedModel_2, subMsg_2, page, sequence;
    if (matchValue[0].tag === 4) {
        if (matchValue[1].StartScreen != null) {
            pattern_matching_result = 0;
            extractedModel = matchValue[1].StartScreen;
            subMsg = matchValue[0].fields[0];
        }
        else {
            pattern_matching_result = 5;
        }
    }
    else if (matchValue[0].tag === 3) {
        if (matchValue[1].CurrentGame != null) {
            if (matchValue[1].GameScreen != null) {
                pattern_matching_result = 1;
                extractedGame = matchValue[1].CurrentGame;
                extractedModel_1 = matchValue[1].GameScreen;
                subMsg_1 = matchValue[0].fields[0];
            }
            else {
                pattern_matching_result = 5;
            }
        }
        else {
            pattern_matching_result = 5;
        }
    }
    else if (matchValue[0].tag === 2) {
        if (matchValue[1].CurrentGame != null) {
            pattern_matching_result = 2;
            extractedModel_2 = matchValue[1].CurrentGame;
            subMsg_2 = matchValue[0].fields[0];
        }
        else {
            pattern_matching_result = 5;
        }
    }
    else if (matchValue[0].tag === 1) {
        pattern_matching_result = 3;
        page = matchValue[0].fields[0];
    }
    else if (matchValue[0].tag === 5) {
        pattern_matching_result = 4;
        sequence = matchValue[0].fields[0];
    }
    else {
        pattern_matching_result = 5;
    }
    switch (pattern_matching_result) {
        case 0: {
            const difficulty = subMsg.fields[0];
            return [(CurrentGame = (arg0 = (createGame(difficulty)), (arg0)), new Model(model.CurrentPage, CurrentGame, model.StartScreen, model.GameScreen)), (msg_1 = (new Msg(1, new Page(1))), Cmd_OfFunc_result(msg_1))];
        }
        case 1: {
            if (subMsg_1.tag === 16) {
                const requestMsg = subMsg_1.fields[0];
                const cmd = mapUiRequestToGameCommand(requestMsg, extractedModel_1, extractedGame);
                return [model, cmd];
            }
            else {
                const patternInput = update_1(subMsg_1, extractedModel_1, extractedGame);
                const subModel = patternInput[0];
                const subCmd = patternInput[1];
                return [new Model(model.CurrentPage, model.CurrentGame, model.StartScreen, subModel), Cmd_map((arg0_2) => (new Msg(3, arg0_2)), subCmd)];
            }
        }
        case 2: {
            const patternInput_1 = update_2(subMsg_2, extractedModel_2);
            const subModel_1 = patternInput_1[0];
            const subCmd_1 = patternInput_1[1];
            return [new Model(model.CurrentPage, subModel_1, model.StartScreen, model.GameScreen), (mapGameEventToUiCommand(subCmd_1, subMsg_2))];
        }
        case 3: {
            modifyLocation(page);
            return [model, Cmd_none()];
        }
        case 4: {
            let matchValue_1;
            matchValue_1 = tryHead(sequence);
            if (matchValue_1 == null) {
                return [model, Cmd_none()];
            }
            else {
                const head = matchValue_1;
                return [model, Cmd_batch(ofArray([head, (msg_2 = (arg0_3 = (source_2 = (skip(1, sequence)), (Array.from(source_2))), (new Msg(5, arg0_3))), Cmd_OfFunc_result(msg_2))]))];
            }
        }
        case 5: {
            console.error(some("Missing match in App.State"));
            return [model, Cmd_none()];
        }
    }
}

