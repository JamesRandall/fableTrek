import { DOMAttr } from "../../.fable/Fable.React.5.1.0/Fable.React.Props.fs.js";
import { StartScreenMsg } from "./Types.js";
import * as react from "react";
import { GameDifficulty } from "../../Game/Types.js";
import { keyValueList } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/MapUtil.js";

export function root(model, dispatch) {
    let children_12, children_10, props_4, props_6, props_8;
    const startOnClick = (difficulty) => (new DOMAttr(40, (_arg1) => {
        dispatch((new StartScreenMsg(0, difficulty)));
    }));
    const children_14 = [react.createElement("div", {
        className: "title",
    }, "FableTrek"), (children_12 = [(children_10 = [react.createElement("button", {
        disabled: true,
    }, "Resume Game"), (props_4 = [startOnClick(new GameDifficulty(0))], react.createElement("button", keyValueList(props_4, 1), "Easy Game")), (props_6 = [startOnClick(new GameDifficulty(1))], react.createElement("button", keyValueList(props_6, 1), "Medium Game")), (props_8 = [startOnClick(new GameDifficulty(2))], react.createElement("button", keyValueList(props_8, 1), "Hard Game"))], react.createElement("div", {
        className: "buttons",
    }, ...children_10))], react.createElement("div", {
        className: "buttonContainer",
    }, ...children_12))];
    return react.createElement("div", {
        className: "startScreen",
    }, ...children_14);
}

