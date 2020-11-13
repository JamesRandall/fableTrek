import { Union, Record } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Types.js";
import { union_type, record_type, unit_type } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Reflection.js";
import { GameDifficulty$reflection } from "../../Game/Types.js";

export class Model extends Record {
    constructor(Placeholder) {
        super();
        this.Placeholder = Placeholder;
    }
}

export function Model$reflection() {
    return record_type("Interface.StartScreen.Types.Model", [], Model, () => [["Placeholder", unit_type]]);
}

export function Model_get_Empty() {
    return new Model(void 0);
}

export class StartScreenMsg extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["StartNewGame"];
    }
}

export function StartScreenMsg$reflection() {
    return union_type("Interface.StartScreen.Types.StartScreenMsg", [], StartScreenMsg, () => [[["Item", GameDifficulty$reflection()]]]);
}

