import { printf, toText } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/String.js";
import { SVGAttr, HTMLAttr } from "../.fable/Fable.React.5.1.0/Fable.React.Props.fs.js";
import * as react from "react";
import { keyValueList } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/MapUtil.js";
import { append } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";

export function Css_rgb(r, g, b) {
    const clo1 = toText(printf("rgb(%d,%d,%d)"));
    const clo2 = clo1(r);
    const clo3 = clo2(g);
    return clo3(b);
}

export function Css_rgba(r, g, b, a) {
    const clo1 = toText(printf("rgba(%d,%d,%d,%f)"));
    const clo2 = clo1(r);
    const clo3 = clo2(g);
    const clo4 = clo3(b);
    return clo4(a);
}

export const GameColors_danger = (a) => Css_rgba(230, 3, 3, a);

export const GameColors_warning = (a) => Css_rgba(252, 186, 3, a);

export const GameColors_healthy = (a) => Css_rgba(0, 230, 0, a);

export const GameColors_indicatorBackgroundColor = Css_rgba(0, 0, 0, 0.4);

export function labelAtRow(row, text) {
    const props = [new HTMLAttr(25, "label"), ["style", {
        gridRow: row,
    }]];
    return react.createElement("div", keyValueList(props, 1), text);
}

export function label(text) {
    return react.createElement("div", {
        className: "label",
    }, text);
}

export function floatLabel(floatValue) {
    let s, clo1;
    const children = [(s = (clo1 = toText(printf("%.0f")), clo1(floatValue)), s)];
    return react.createElement("div", {
        className: "label",
    }, ...children);
}

export function arc(x, y, radius, startAngle, endAngle, attributes) {
    const polarToCartesian = (centerX, centerY, radius_1, angleInDegrees) => {
        const angleInRadians = ((angleInDegrees - 90) * 3.141592653589793) / 180;
        return [centerX + (radius_1 * Math.cos(angleInRadians)), centerY + (radius_1 * Math.sin(angleInRadians))];
    };
    const patternInput = polarToCartesian(x, y, radius, endAngle);
    const startY = patternInput[1];
    const startX = patternInput[0];
    const patternInput_1 = polarToCartesian(x, y, radius, startAngle);
    const endY = patternInput_1[1];
    const endX = patternInput_1[0];
    const arcSweep = ((endAngle - startAngle) <= 180) ? "0" : "1";
    let d;
    const clo1 = toText(printf("M %f %f A %f %f 0 %s 0 %f %f "));
    const clo2 = clo1(startX);
    const clo3 = clo2(startY);
    const clo4 = clo3(radius);
    const clo5 = clo4(radius);
    const clo6 = clo5(arcSweep);
    const clo7 = clo6(endX);
    d = clo7(endY);
    let props;
    props = append(attributes, [new SVGAttr(3, d)]);
    return react.createElement("path", keyValueList(props, 1));
}

