import { unitSvg } from "./Common.js";
import { SVGAttr } from "../../../.fable/Fable.React.5.1.0/Fable.React.Props.fs.js";
import { Css_rgb, Css_rgba } from "../../Common.js";
import * as react from "react";
import { keyValueList } from "../../../.fable/fable-library.3.0.0-nagareyama-rc-005/MapUtil.js";

const Renderers_strokeWidth = 3;

const Renderers_bgOpacity = 0.1;

export const Renderers_star = unitSvg([(() => {
    const props = [new SVGAttr(1, 50), new SVGAttr(2, 50), new SVGAttr(24, 50), new SVGAttr(6, Css_rgba(252, 186, 3, Renderers_bgOpacity)), new SVGAttr(35, Renderers_strokeWidth), new SVGAttr(30, "rgb(252,186,3)")];
    return react.createElement("circle", keyValueList(props, 1));
})()]);

export function Renderers_opaquePlayer(shouldFill, opacity) {
    let props;
    return unitSvg([(props = [new SVGAttr(35, Renderers_strokeWidth), new SVGAttr(6, Css_rgba(0, 255, 255, shouldFill ? Renderers_bgOpacity : 0)), new SVGAttr(30, Css_rgba(0, 255, 255, opacity)), new SVGAttr(3, "M 50 0 L 25 58 L 0 50 L 25 100 L 50 83 L 75 100 L 100 50 L 75 58 L 50 0")], react.createElement("path", keyValueList(props, 1)))]);
}

export function Renderers_player(shouldFill) {
    return Renderers_opaquePlayer(shouldFill, 1);
}

export function Renderers_enemyScout(shouldFill) {
    let props;
    return unitSvg([(props = [new SVGAttr(35, Renderers_strokeWidth), new SVGAttr(6, Css_rgba(255, 0, 0, shouldFill ? Renderers_bgOpacity : 0)), new SVGAttr(30, Css_rgb(255, 0, 0)), new SVGAttr(3, "M 50 0 L 10 100 L 50 70 L 90 100 L 50 0")], react.createElement("path", keyValueList(props, 1)))]);
}

export function Renderers_enemyCruiser(shouldFill) {
    let props;
    return unitSvg([(props = [new SVGAttr(35, Renderers_strokeWidth), new SVGAttr(6, Css_rgba(255, 0, 0, shouldFill ? Renderers_bgOpacity : 0)), new SVGAttr(30, Css_rgb(255, 0, 0)), new SVGAttr(3, "M 50 0 L 0 90 L 40 100 L 60 100 L 100 90 L 50 0")], react.createElement("path", keyValueList(props, 1)))]);
}

export function Renderers_enemyDreadnought(shouldFill) {
    let props, props_2, props_4;
    return unitSvg([(props = [new SVGAttr(43, 0), new SVGAttr(56, 0), new SVGAttr(40, 100), new SVGAttr(14, 100), new SVGAttr(35, Renderers_strokeWidth), new SVGAttr(6, Css_rgba(255, 0, 0, shouldFill ? Renderers_bgOpacity : 0)), new SVGAttr(30, Css_rgb(255, 0, 0))], react.createElement("rect", keyValueList(props, 1))), (props_2 = [new SVGAttr(43, 10), new SVGAttr(56, 10), new SVGAttr(40, 30), new SVGAttr(14, 30), new SVGAttr(35, Renderers_strokeWidth), new SVGAttr(6, Css_rgba(0, 0, 0, 0.5)), new SVGAttr(30, Css_rgb(255, 128, 0))], react.createElement("rect", keyValueList(props_2, 1))), (props_4 = [new SVGAttr(43, 50), new SVGAttr(56, 50), new SVGAttr(40, 40), new SVGAttr(14, 40), new SVGAttr(35, Renderers_strokeWidth), new SVGAttr(6, Css_rgba(0, 0, 0, 0.5)), new SVGAttr(30, Css_rgb(255, 128, 0))], react.createElement("rect", keyValueList(props_4, 1)))]);
}

export function renderGameObject(go) {
    const matchValue = go.Attributes;
    switch (matchValue.tag) {
        case 2: {
            return Renderers_star;
        }
        case 0: {
            const enemy = matchValue.fields[0];
            const matchValue_1 = enemy.ShipClass;
            switch (matchValue_1.tag) {
                case 1: {
                    return Renderers_enemyCruiser(true);
                }
                case 2: {
                    return Renderers_enemyDreadnought(true);
                }
                default: {
                    return Renderers_enemyScout(true);
                }
            }
        }
        default: {
            return react.createElement(react.Fragment, {});
        }
    }
}

export const renderUnitStrip = (() => {
    let children, children_2, children_4, children_6;
    const children_8 = [(children = [Renderers_enemyScout(false)], react.createElement("div", {}, ...children)), (children_2 = [Renderers_enemyCruiser(false)], react.createElement("div", {}, ...children_2)), (children_4 = [Renderers_enemyDreadnought(false)], react.createElement("div", {}, ...children_4)), (children_6 = [Renderers_player(false)], react.createElement("div", {}, ...children_6))];
    return react.createElement("div", {
        className: "unitStrip",
    }, ...children_8);
})();

export function renderPlayer() {
    return Renderers_player(true);
}

