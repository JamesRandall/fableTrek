import { Player__get_SystemsAsList } from "../../Game/Types.js";
import { Damage_totalRepairTime, Damage_canRepair } from "../../Game/Rules.js";
import { ofArray, singleton as singleton_1, ofSeq } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";
import { collect, singleton, append, delay } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";
import { label } from "../Common.js";
import { printf, toText } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/String.js";
import * as react from "react";
import { CSSProp, HTMLAttr } from "../../.fable/Fable.React.5.1.0/Fable.React.Props.fs.js";
import { keyValueList } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/MapUtil.js";

export function view(game) {
    let children_10, source2, children_2, source1, children_14;
    const systems = Player__get_SystemsAsList(game.Player);
    let canRepair;
    canRepair = Damage_canRepair(game);
    const children_16 = [(children_10 = (source2 = singleton_1((children_2 = ofSeq(delay(() => {
        let arg10, clo1;
        return (!canRepair) ? append(singleton(label((arg10 = (Damage_totalRepairTime(game.Player)), (clo1 = toText(printf("Total repair time: %.1f days")), clo1(arg10))))), delay(() => singleton(react.createElement("button", {
            className: "plain",
        }, "Repair")))) : singleton(label("All systems fully operational"));
    })), react.createElement("div", {
        className: "damageControlFooter",
    }, ...children_2))), (source1 = (collect((tupledArg) => {
        let rangeValue, percentage, rt, foregroundClass, children_8, children_6, props_4, css, arg10_1, arg10_2, rt_2, clo1_2, clo1_1;
        const text = tupledArg[0];
        const hp = tupledArg[1];
        return ofArray([label(text), (rangeValue = hp, (percentage = (rt = rangeValue, ((rt.Current) - (rt.Min)) / ((rt.Max) - (rt.Min))), (foregroundClass = ("levelIndicatorForeground" + ((percentage > 0.5) ? "Healthy" : ((percentage > 0.25) ? "Warning" : "Danger"))), (children_8 = [(children_6 = [(props_4 = [new HTMLAttr(25, foregroundClass), (css = singleton_1(new CSSProp(394, (arg10_1 = (arg10_2 = ((rt_2 = rangeValue, ((rt_2.Current) - (rt_2.Min)) / ((rt_2.Max) - (rt_2.Min))) * 100), (clo1_2 = toText(printf("%.0f")), clo1_2(arg10_2))), (clo1_1 = toText(printf("%s%%")), clo1_1(arg10_1))))), ["style", keyValueList(css, 1)])], react.createElement("div", keyValueList(props_4, 1)))], react.createElement("div", {
            className: "levelIndicatorBackground",
        }, ...children_6))], react.createElement("div", {
            className: "levelIndicator",
        }, ...children_8)))))]);
    }, systems)), append(source1, source2))), react.createElement("div", {
        className: "damageControlContainer",
    }, ...children_10)), (children_14 = [react.createElement("div", {
        className: "arrowDown",
    })], react.createElement("div", {
        className: "arrowDownContainer",
    }, ...children_14))];
    return react.createElement("div", {
        className: "damageControl",
    }, ...children_16);
}

