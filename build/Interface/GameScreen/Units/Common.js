import { SVGAttr } from "../../../.fable/Fable.React.5.1.0/Fable.React.Props.fs.js";
import * as react from "react";
import { keyValueList } from "../../../.fable/fable-library.3.0.0-nagareyama-rc-005/MapUtil.js";
import { printf, toText } from "../../../.fable/fable-library.3.0.0-nagareyama-rc-005/String.js";

export function unitSvg(inner) {
    const props_2 = [["style", {
        width: "100%",
        height: "100%",
    }], new SVGAttr(39, "0 0 100 100"), new SVGAttr(23, "xMinYMid keep")];
    const children_2 = [react.createElement("g", keyValueList([new SVGAttr(37, "translate(5,5) scale(0.9,0.9)"), ["shape-rendering", "crispEdges"]], 1), ...inner)];
    return react.createElement("svg", keyValueList(props_2, 1), ...children_2);
}

export function pixelatedUnitSvg(width, height, inner) {
    let clo1, clo2;
    const props_2 = [["style", {
        width: "100%",
        height: "100%",
    }], new SVGAttr(39, (clo1 = toText(printf("0 0 %d %d")), clo2 = clo1(width), clo2(height))), new SVGAttr(23, "xMinYMid keep")];
    const children_2 = [react.createElement("g", keyValueList([new SVGAttr(37, "translate(5,5) scale(0.9,0.9)"), ["shape-rendering", "crispEdges"]], 1), ...inner)];
    return react.createElement("svg", keyValueList(props_2, 1), ...children_2);
}

