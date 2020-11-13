import { singleton, ofArray } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";
import { label, arc, GameColors_healthy, GameColors_warning, GameColors_danger, Css_rgba } from "../Common.js";
import { DOMAttr, Prop, CSSProp, HTMLAttr, SVGAttr } from "../../.fable/Fable.React.5.1.0/Fable.React.Props.fs.js";
import { append, mapIndexed } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";
import * as react from "react";
import { keyValueList } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/MapUtil.js";
import { FunctionComponent_Of_2F363EB5 } from "../../.fable/Fable.React.5.1.0/Fable.React.FunctionComponent.fs.js";
import { debouncedSize } from "../Browser.Helpers.js";
import { printf, toText } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/String.js";
import { GameRequestMsg, GameScreenMsg } from "./Types.js";
import { Renderers_opaquePlayer } from "./Units/Vector.js";

export function shields(player) {
    const shieldArcWidth = 6;
    const foreStart = 317;
    const foreEnd = 43;
    const starEnd = 313;
    const starStart = 227;
    const portStart = 47;
    const portEnd = 133;
    const aftStart = 137;
    const aftEnd = 223;
    const radii = ofArray([125 - shieldArcWidth, 125 - (shieldArcWidth * 3), 125 - (shieldArcWidth * 5), 125 - (shieldArcWidth * 7)]);
    const shieldColor_1 = (arcNumber, shieldLevel) => {
        const arcNumber_1 = arcNumber | 0;
        const opacity = player.ShieldsRaised ? 1 : 0.4;
        let p;
        const rt = shieldLevel;
        p = (((rt.Current) - (rt.Min)) / ((rt.Max) - (rt.Min)));
        const shieldArc = ((p <= 0.25) ? 0 : ((p <= 0.5) ? 1 : ((p <= 0.75) ? 2 : 3))) | 0;
        if (arcNumber_1 > shieldArc) {
            return Css_rgba(255, 0, 0, 0.2);
        }
        else {
            const basedP = (p - ((arcNumber_1) * 0.25)) / (1 / 3);
            return ((basedP <= 0.33) ? GameColors_danger : ((basedP <= 0.66) ? GameColors_warning : GameColors_healthy))(opacity);
        }
    };
    const props = [["style", {
        width: "100%",
        height: "100%",
    }], new SVGAttr(39, "0 0 250 250"), new SVGAttr(23, "xMinYMid keep")];
    let children;
    let source2_2;
    let source2_1;
    let source2;
    source2 = mapIndexed((i, r) => arc(125, 125, r, foreStart, foreEnd, [new SVGAttr(30, shieldColor_1(3 - i, player.ForeShields)), new SVGAttr(35, shieldArcWidth), new SVGAttr(6, "none")]), radii);
    let source1;
    source1 = mapIndexed((i_1, r_1) => arc(125, 125, r_1, starStart, starEnd, [new SVGAttr(30, shieldColor_1(3 - i_1, player.StarboardShields)), new SVGAttr(35, shieldArcWidth), new SVGAttr(6, "none")]), radii);
    source2_1 = append(source1, source2);
    let source1_1;
    source1_1 = mapIndexed((i_2, r_2) => arc(125, 125, r_2, portStart, portEnd, [new SVGAttr(30, shieldColor_1(3 - i_2, player.PortShields)), new SVGAttr(35, shieldArcWidth), new SVGAttr(6, "none")]), radii);
    source2_2 = append(source1_1, source2_1);
    let source1_2;
    source1_2 = mapIndexed((i_3, r_3) => arc(125, 125, r_3, aftStart, aftEnd, [new SVGAttr(30, shieldColor_1(3 - i_3, player.AftShields)), new SVGAttr(35, shieldArcWidth), new SVGAttr(6, "none")]), radii);
    children = append(source1_2, source2_2);
    return react.createElement("svg", keyValueList(props, 1), ...children);
}

export const view = FunctionComponent_Of_2F363EB5((props) => {
    let children_36, children_6, rangeValue, percentage, rt, foregroundClass, children_4, children_2, props_1, css, arg10, arg10_1, rt_2, clo1_1, clo1, children_26, children_16, children_8, arg10_2, rt_4, clo1_2, children_10, arg10_3, rt_6, clo1_3, children_12, arg10_4, rt_8, clo1_4, children_14, arg10_5, rt_10, clo1_5, props_25, css_1, children_24, children_20, children_18, props_23, css_2, tuple, tuple_1, children_22, children_34, rangeValue_2, percentage_1, rt_11, foregroundClass_2, children_32, children_30, props_29, css_3, arg10_6, arg10_7, rt_13, clo1_7, clo1_6;
    const shieldContainerSize = react.useState([0, 0]);
    const shieldContainerRef = react.useRef(void 0);
    debouncedSize(shieldContainerRef, (arg00) => {
        shieldContainerSize[1](arg00);
    });
    const children_38 = [(children_36 = [(children_6 = [label("Main"), (rangeValue = props.player.Energy, (percentage = (rt = rangeValue, ((rt.Current) - (rt.Min)) / ((rt.Max) - (rt.Min))), (foregroundClass = ("levelIndicatorForeground" + ((percentage > 0.5) ? "Healthy" : ((percentage > 0.25) ? "Warning" : "Danger"))), (children_4 = [(children_2 = [(props_1 = [new HTMLAttr(25, foregroundClass), (css = singleton(new CSSProp(394, (arg10 = (arg10_1 = ((rt_2 = rangeValue, ((rt_2.Current) - (rt_2.Min)) / ((rt_2.Max) - (rt_2.Min))) * 100), (clo1_1 = toText(printf("%.0f")), clo1_1(arg10_1))), (clo1 = toText(printf("%s%%")), clo1(arg10))))), ["style", keyValueList(css, 1)])], react.createElement("div", keyValueList(props_1, 1)))], react.createElement("div", {
        className: "levelIndicatorBackground",
    }, ...children_2))], react.createElement("div", {
        className: "levelIndicator",
    }, ...children_4)))))], react.createElement("div", {
        className: "innerTop",
    }, ...children_6)), (children_26 = [(children_16 = [(children_8 = [label("Fore"), label((arg10_2 = ((rt_4 = props.player.ForeShields, ((rt_4.Current) - (rt_4.Min)) / ((rt_4.Max) - (rt_4.Min))) * 100), (clo1_2 = toText(printf("%.0f")), clo1_2(arg10_2))))], react.createElement("div", {
        className: "labelValuePair",
    }, ...children_8)), (children_10 = [label("Star"), label((arg10_3 = ((rt_6 = props.player.StarboardShields, ((rt_6.Current) - (rt_6.Min)) / ((rt_6.Max) - (rt_6.Min))) * 100), (clo1_3 = toText(printf("%.0f")), clo1_3(arg10_3))))], react.createElement("div", {
        className: "labelValuePair",
    }, ...children_10)), (children_12 = [label("Aft"), label((arg10_4 = ((rt_8 = props.player.AftShields, ((rt_8.Current) - (rt_8.Min)) / ((rt_8.Max) - (rt_8.Min))) * 100), (clo1_4 = toText(printf("%.0f")), clo1_4(arg10_4))))], react.createElement("div", {
        className: "labelValuePair",
    }, ...children_12)), (children_14 = [label("Port"), label((arg10_5 = ((rt_10 = props.player.PortShields, ((rt_10.Current) - (rt_10.Min)) / ((rt_10.Max) - (rt_10.Min))) * 100), (clo1_5 = toText(printf("%.0f")), clo1_5(arg10_5))))], react.createElement("div", {
        className: "labelValuePair",
    }, ...children_14))], react.createElement("div", {
        className: "shieldsProperties",
    }, ...children_16)), (props_25 = [new HTMLAttr(25, "shieldsContainer"), new Prop(2, shieldContainerRef), (css_1 = singleton(new CSSProp(260, props.player.ShieldsRaised ? 1 : 0.6)), ["style", keyValueList(css_1, 1)]), new DOMAttr(40, (_arg1) => {
        props.dispatch((new GameScreenMsg(16, new GameRequestMsg(0))));
    })], (children_24 = [(children_20 = [(children_18 = [Renderers_opaquePlayer(true, 1)], react.createElement("div", {
        className: "shieldsPlayer",
    }, ...children_18))], react.createElement("div", {
        className: "shieldPlayerContainer",
    }, ...children_20)), (props_23 = [new HTMLAttr(25, "shieldsGraphics"), (css_2 = ofArray([new CSSProp(394, (tuple = (shieldContainerSize[0]), (tuple[0]))), new CSSProp(189, (tuple_1 = (shieldContainerSize[0]), (tuple_1[0])))]), ["style", keyValueList(css_2, 1)])], (children_22 = [shields(props.player)], react.createElement("div", keyValueList(props_23, 1), ...children_22)))], react.createElement("div", keyValueList(props_25, 1), ...children_24)))], react.createElement("div", {
        className: "innerMiddle",
    }, ...children_26)), (children_34 = [label("Generators"), (rangeValue_2 = props.player.ShieldGenerator, (percentage_1 = (rt_11 = rangeValue_2, ((rt_11.Current) - (rt_11.Min)) / ((rt_11.Max) - (rt_11.Min))), (foregroundClass_2 = ("levelIndicatorForeground" + ((percentage_1 > 0.5) ? "Healthy" : ((percentage_1 > 0.25) ? "Warning" : "Danger"))), (children_32 = [(children_30 = [(props_29 = [new HTMLAttr(25, foregroundClass_2), (css_3 = singleton(new CSSProp(394, (arg10_6 = (arg10_7 = ((rt_13 = rangeValue_2, ((rt_13.Current) - (rt_13.Min)) / ((rt_13.Max) - (rt_13.Min))) * 100), (clo1_7 = toText(printf("%.0f")), clo1_7(arg10_7))), (clo1_6 = toText(printf("%s%%")), clo1_6(arg10_6))))), ["style", keyValueList(css_3, 1)])], react.createElement("div", keyValueList(props_29, 1)))], react.createElement("div", {
        className: "levelIndicatorBackground",
    }, ...children_30))], react.createElement("div", {
        className: "levelIndicator",
    }, ...children_32)))))], react.createElement("div", {
        className: "innerBottom",
    }, ...children_34))], react.createElement("div", {
        className: "inner",
    }, ...children_36))];
    return react.createElement("div", {
        className: "energyManagement",
    }, ...children_38);
});

