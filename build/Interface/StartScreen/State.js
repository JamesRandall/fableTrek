import { Model_get_Empty } from "./Types.js";
import { Cmd_none } from "../../.fable/Fable.Elmish.3.1.0/cmd.fs.js";

export function init() {
    return [Model_get_Empty(), Cmd_none()];
}

export function update(msg, model) {
    return [model, Cmd_none()];
}

