import { CSSProp, HTMLAttr, SVGAttr } from "../../.fable/Fable.React.5.1.0/Fable.React.Props.fs.js";
import { label, Css_rgb } from "../Common.js";
import * as react from "react";
import { keyValueList } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/MapUtil.js";
import { length, singleton, ofArray } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";
import { append, map, rangeNumber } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";
import { GameWorld_objectAtPosition } from "../../Game/Utils.js";
import { GameMsg, UpdateGameStateMsg, Position__get_AsString } from "../../Game/Types.js";
import { renderGameObject } from "./Units/Vector.js";
import { printf, toText } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/String.js";
import { FunctionComponent_Of_2F363EB5 } from "../../.fable/Fable.React.5.1.0/Fable.React.FunctionComponent.fs.js";
import { Browser_Types_Event__Event_get_Value } from "../../.fable/Fable.React.5.1.0/Fable.React.Extensions.fs.js";
import { parse } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Double.js";

export const noTargetAcquired = ofArray([(() => {
    let props_8, children_8;
    const props_10 = [["style", {
        width: "100%",
        height: "121px",
    }], new SVGAttr(39, "0 0 100 100"), new SVGAttr(23, "xMinYMid keep")];
    const children_10 = [(props_8 = [new SVGAttr(30, Css_rgb(0, 128, 0)), new SVGAttr(35, 2)], (children_8 = [react.createElement("path", {
        d: "M 40 1 L 60 1 M 50 1 L 50 15",
    }), react.createElement("path", {
        d: "M 1 40 L 1 60 M 1 50 L 15 50",
    }), react.createElement("path", {
        d: "M 40 99 L 60 99 M 50 99 L 50 85",
    }), react.createElement("path", {
        d: "M 99 40 L 99 60 M 99 50 L 85 50",
    })], react.createElement("g", keyValueList(props_8, 1), ...children_8)))];
    return react.createElement("svg", keyValueList(props_10, 1), ...children_10);
})(), react.createElement("div", {
    className: "noTargetAcquiredLabel",
}, "No Target")]);

export function torpedoesView(torpedoes) {
    let children_2;
    const source = rangeNumber(1, 1, (torpedoes.Max));
    children_2 = map((t) => {
        const props = [new HTMLAttr(25, "torpedo " + ((t <= (torpedoes.Current)) ? "torpedoActive" : "torpedoInactive"))];
        return react.createElement("div", keyValueList(props, 1));
    }, source);
    return react.createElement("div", {
        className: "torpedoes",
    }, ...children_2);
}

export function targets(targets_1, gameObjects, gameDispatch) {
    let acquiredTargets;
    acquiredTargets = map((targetPosition) => {
        let children_4, children_2, children, children_24, rangeValue, percentage, rt, foregroundClass, children_10, children_8, props_6, css, arg10, arg10_1, rt_2, clo1_1, clo1, rangeValue_2, percentage_1, rt_3, foregroundClass_2, children_16, children_14, props_12, css_1, arg10_2, arg10_3, rt_5, clo1_3, clo1_2, rangeValue_4, percentage_2, rt_6, foregroundClass_4, children_22, children_20, props_18, css_2, arg10_4, arg10_5, rt_8, clo1_5, clo1_4;
        let optionalTarget;
        optionalTarget = GameWorld_objectAtPosition(gameObjects, targetPosition);
        if (optionalTarget == null) {
            return react.createElement("div", {
                className: "target",
            }, ...noTargetAcquired);
        }
        else {
            const target = optionalTarget;
            const matchValue = target.Attributes;
            if (matchValue.tag === 0) {
                const enemy = matchValue.fields[0];
                const children_26 = [(children_4 = [label(Position__get_AsString(target.Position.SectorPosition)), (children_2 = [(children = [renderGameObject(target)], react.createElement("div", {
                    className: "graphic",
                }, ...children))], react.createElement("div", {
                    className: "graphicContainer",
                }, ...children_2))], react.createElement("div", {
                    className: "graphicAndCoords",
                }, ...children_4)), (children_24 = [label("E"), (rangeValue = enemy.Energy, (percentage = (rt = rangeValue, ((rt.Current) - (rt.Min)) / ((rt.Max) - (rt.Min))), (foregroundClass = ("levelIndicatorForeground" + ((percentage > 0.5) ? "Healthy" : ((percentage > 0.25) ? "Warning" : "Danger"))), (children_10 = [(children_8 = [(props_6 = [new HTMLAttr(25, foregroundClass), (css = singleton(new CSSProp(394, (arg10 = (arg10_1 = ((rt_2 = rangeValue, ((rt_2.Current) - (rt_2.Min)) / ((rt_2.Max) - (rt_2.Min))) * 100), (clo1_1 = toText(printf("%.0f")), clo1_1(arg10_1))), (clo1 = toText(printf("%s%%")), clo1(arg10))))), ["style", keyValueList(css, 1)])], react.createElement("div", keyValueList(props_6, 1)))], react.createElement("div", {
                    className: "levelIndicatorBackground",
                }, ...children_8))], react.createElement("div", {
                    className: "levelIndicator",
                }, ...children_10))))), label("S"), (rangeValue_2 = enemy.Shields, (percentage_1 = (rt_3 = rangeValue_2, ((rt_3.Current) - (rt_3.Min)) / ((rt_3.Max) - (rt_3.Min))), (foregroundClass_2 = ("levelIndicatorForeground" + ((percentage_1 > 0.5) ? "Healthy" : ((percentage_1 > 0.25) ? "Warning" : "Danger"))), (children_16 = [(children_14 = [(props_12 = [new HTMLAttr(25, foregroundClass_2), (css_1 = singleton(new CSSProp(394, (arg10_2 = (arg10_3 = ((rt_5 = rangeValue_2, ((rt_5.Current) - (rt_5.Min)) / ((rt_5.Max) - (rt_5.Min))) * 100), (clo1_3 = toText(printf("%.0f")), clo1_3(arg10_3))), (clo1_2 = toText(printf("%s%%")), clo1_2(arg10_2))))), ["style", keyValueList(css_1, 1)])], react.createElement("div", keyValueList(props_12, 1)))], react.createElement("div", {
                    className: "levelIndicatorBackground",
                }, ...children_14))], react.createElement("div", {
                    className: "levelIndicator",
                }, ...children_16))))), label("H"), (rangeValue_4 = enemy.HitPoints, (percentage_2 = (rt_6 = rangeValue_4, ((rt_6.Current) - (rt_6.Min)) / ((rt_6.Max) - (rt_6.Min))), (foregroundClass_4 = ("levelIndicatorForeground" + ((percentage_2 > 0.5) ? "Healthy" : ((percentage_2 > 0.25) ? "Warning" : "Danger"))), (children_22 = [(children_20 = [(props_18 = [new HTMLAttr(25, foregroundClass_4), (css_2 = singleton(new CSSProp(394, (arg10_4 = (arg10_5 = ((rt_8 = rangeValue_4, ((rt_8.Current) - (rt_8.Min)) / ((rt_8.Max) - (rt_8.Min))) * 100), (clo1_5 = toText(printf("%.0f")), clo1_5(arg10_5))), (clo1_4 = toText(printf("%s%%")), clo1_4(arg10_4))))), ["style", keyValueList(css_2, 1)])], react.createElement("div", keyValueList(props_18, 1)))], react.createElement("div", {
                    className: "levelIndicatorBackground",
                }, ...children_20))], react.createElement("div", {
                    className: "levelIndicator",
                }, ...children_22)))))], react.createElement("div", {
                    className: "summary",
                }, ...children_24))];
                return react.createElement("div", {
                    className: "target",
                    onClick: (_arg1) => {
                        let arg0_1;
                        gameDispatch((arg0_1 = (new UpdateGameStateMsg(6, target.Position)), (new GameMsg(1, arg0_1))));
                    },
                }, ...children_26);
            }
            else {
                return react.createElement("div", {
                    className: "target",
                }, ...noTargetAcquired);
            }
        }
    }, targets_1);
    let blankTargets;
    const source_1 = rangeNumber(length(targets_1), 1, 2);
    blankTargets = map((_arg2) => react.createElement("div", {
        className: "target",
    }, ...noTargetAcquired), source_1);
    let children_34;
    children_34 = append(acquiredTargets, blankTargets);
    return react.createElement("div", {
        className: "targets",
    }, ...children_34);
}

export const view = FunctionComponent_Of_2F363EB5((props) => {
    let children_18, range, rangeValue, percentage, rt, foregroundClass, children_4, children_2, props_3, css, arg10, arg10_1, rt_2, clo1_1, clo1, rangeValue_2, percentage_1, rt_3, foregroundClass_2, children_10, children_8, props_9, css_1, arg10_2, arg10_3, rt_5, clo1_3, clo1_2, rangeValue_4, percentage_2, rt_6, foregroundClass_4, children_16, children_14, props_15, css_2, arg10_4, arg10_5, rt_8, clo1_5, clo1_4;
    const children_20 = [(children_18 = [label("Phaser Power"), (range = props.player.PhaserPower, react.createElement("input", {
        type: "range",
        className: "rangeRed",
        min: range.Min,
        max: range.Max,
        value: range.Current,
        onChange: (ev) => {
            let arg0_1, arg0;
            const newValue = Browser_Types_Event__Event_get_Value(ev);
            props.gameDispatch((arg0_1 = (arg0 = (parse(newValue) * 1), (new UpdateGameStateMsg(0, arg0))), (new GameMsg(1, arg0_1))));
        },
    })), label("Phaser Temp"), (rangeValue = props.player.PhaserTemperature, (percentage = (rt = rangeValue, ((rt.Current) - (rt.Min)) / ((rt.Max) - (rt.Min))), (foregroundClass = ("levelIndicatorForeground" + ((percentage > 0.75) ? "Danger" : ((percentage > 0.5) ? "Warning" : "Healthy"))), (children_4 = [(children_2 = [(props_3 = [new HTMLAttr(25, foregroundClass), (css = singleton(new CSSProp(394, (arg10 = (arg10_1 = ((rt_2 = rangeValue, ((rt_2.Current) - (rt_2.Min)) / ((rt_2.Max) - (rt_2.Min))) * 100), (clo1_1 = toText(printf("%.0f")), clo1_1(arg10_1))), (clo1 = toText(printf("%s%%")), clo1(arg10))))), ["style", keyValueList(css, 1)])], react.createElement("div", keyValueList(props_3, 1)))], react.createElement("div", {
        className: "levelIndicatorBackground",
    }, ...children_2))], react.createElement("div", {
        className: "levelIndicator",
    }, ...children_4))))), label("Phasers"), (rangeValue_2 = props.player.Phasers, (percentage_1 = (rt_3 = rangeValue_2, ((rt_3.Current) - (rt_3.Min)) / ((rt_3.Max) - (rt_3.Min))), (foregroundClass_2 = ("levelIndicatorForeground" + ((percentage_1 > 0.5) ? "Healthy" : ((percentage_1 > 0.25) ? "Warning" : "Danger"))), (children_10 = [(children_8 = [(props_9 = [new HTMLAttr(25, foregroundClass_2), (css_1 = singleton(new CSSProp(394, (arg10_2 = (arg10_3 = ((rt_5 = rangeValue_2, ((rt_5.Current) - (rt_5.Min)) / ((rt_5.Max) - (rt_5.Min))) * 100), (clo1_3 = toText(printf("%.0f")), clo1_3(arg10_3))), (clo1_2 = toText(printf("%s%%")), clo1_2(arg10_2))))), ["style", keyValueList(css_1, 1)])], react.createElement("div", keyValueList(props_9, 1)))], react.createElement("div", {
        className: "levelIndicatorBackground",
    }, ...children_8))], react.createElement("div", {
        className: "levelIndicator",
    }, ...children_10))))), label("Torpedoes"), torpedoesView(props.player.Torpedos), label("Launcher"), (rangeValue_4 = props.player.TorpedoLaunchers, (percentage_2 = (rt_6 = rangeValue_4, ((rt_6.Current) - (rt_6.Min)) / ((rt_6.Max) - (rt_6.Min))), (foregroundClass_4 = ("levelIndicatorForeground" + ((percentage_2 > 0.5) ? "Healthy" : ((percentage_2 > 0.25) ? "Warning" : "Danger"))), (children_16 = [(children_14 = [(props_15 = [new HTMLAttr(25, foregroundClass_4), (css_2 = singleton(new CSSProp(394, (arg10_4 = (arg10_5 = ((rt_8 = rangeValue_4, ((rt_8.Current) - (rt_8.Min)) / ((rt_8.Max) - (rt_8.Min))) * 100), (clo1_5 = toText(printf("%.0f")), clo1_5(arg10_5))), (clo1_4 = toText(printf("%s%%")), clo1_4(arg10_4))))), ["style", keyValueList(css_2, 1)])], react.createElement("div", keyValueList(props_15, 1)))], react.createElement("div", {
        className: "levelIndicatorBackground",
    }, ...children_14))], react.createElement("div", {
        className: "levelIndicator",
    }, ...children_16)))))], react.createElement("div", {
        className: "inner",
    }, ...children_18)), targets(props.player.Targets, props.gameObjects, props.gameDispatch)];
    return react.createElement("div", {
        className: "weapons",
    }, ...children_20);
});

