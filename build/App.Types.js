import { Union, Record } from "./.fable/fable-library.3.0.0-nagareyama-rc-005/Types.js";
import { Page, Page$reflection } from "./Router.js";
import { GameMsg$reflection, GameDifficulty$reflection, Game$reflection } from "./Game/Types.js";
import { union_type, array_type, list_type, lambda_type, unit_type, record_type, option_type } from "./.fable/fable-library.3.0.0-nagareyama-rc-005/Reflection.js";
import { StartScreenMsg$reflection, Model_get_Empty as Model_get_Empty_1, Model$reflection as Model$reflection_1 } from "./Interface/StartScreen/Types.js";
import { GameScreenMsg$reflection, Model$reflection as Model$reflection_2 } from "./Interface/GameScreen/Types.js";

export class Model extends Record {
    constructor(CurrentPage, CurrentGame, StartScreen, GameScreen) {
        super();
        this.CurrentPage = CurrentPage;
        this.CurrentGame = CurrentGame;
        this.StartScreen = StartScreen;
        this.GameScreen = GameScreen;
    }
}

export function Model$reflection() {
    return record_type("App.Types.Model", [], Model, () => [["CurrentPage", Page$reflection()], ["CurrentGame", option_type(Game$reflection())], ["StartScreen", option_type(Model$reflection_1())], ["GameScreen", option_type(Model$reflection_2())]]);
}

export function Model_get_Empty() {
    return new Model(new Page(0), void 0, Model_get_Empty_1(), void 0);
}

export class Msg extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["NewGame", "GotoPage", "GameDispatcherMsg", "GameScreenDispatcherMsg", "StartScreenDispatcherMsg", "CommandSequence"];
    }
}

export function Msg$reflection() {
    return union_type("App.Types.Msg", [], Msg, () => [[["Item", GameDifficulty$reflection()]], [["Item", Page$reflection()]], [["Item", GameMsg$reflection()]], [["Item", GameScreenMsg$reflection()]], [["Item", StartScreenMsg$reflection()]], [["Item", array_type(list_type(lambda_type(lambda_type(Msg$reflection(), unit_type), unit_type)))]]]);
}

