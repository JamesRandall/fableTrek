import { equalsSafe } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Util.js";
import { GameScreenMsg, MenuItem } from "./Types.js";
import { Weapons_canAddTarget, Movement_canMove } from "../../Game/Rules.js";
import { GameWorldPosition, GameWorldPosition_get_Max, Position, GameMsg, UpdateGameStateMsg } from "../../Game/Types.js";
import { Position_sectorCoordinateIterator, Position_isAdjacent } from "../../Game/Utils.js";
import { rangeNumber, singleton as singleton_1, delay, isEmpty, replicate, append, map, tryFind } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";
import * as react from "react";
import { label } from "../Common.js";
import { FunctionComponent_Of_2F363EB5 } from "../../.fable/Fable.React.5.1.0/Fable.React.FunctionComponent.fs.js";
import { debouncedSize } from "../Browser.Helpers.js";
import { printf, toText } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/String.js";
import { DOMAttr, SVGAttr, CSSProp, Prop, HTMLAttr } from "../../.fable/Fable.React.5.1.0/Fable.React.Props.fs.js";
import { ofSeq, singleton, ofArray } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";
import { keyValueList } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/MapUtil.js";
import { renderPlayer, renderGameObject } from "./Units/Vector.js";
import { scannerAnimationDurationCss } from "../Animation.js";
import { newGuid } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Guid.js";
import { pixelatedScout } from "./Units/PixelatedScout.js";
import { fold } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Array.js";

export function Menu_items(player, optionalGameObject, position) {
    let arg0_12, arg0_6, arg0_3, arg0_1;
    if (optionalGameObject == null) {
        if (equalsSafe(player.Position, position)) {
            return [(new MenuItem(0, "USS Discovery"))];
        }
        else if (Movement_canMove(player, position)) {
            return [(arg0_12 = ["Move to", (new UpdateGameStateMsg(3, position))], (new MenuItem(1, arg0_12)))];
        }
        else {
            return [(new MenuItem(0, "Insufficient energy to move here"))];
        }
    }
    else {
        const gameObject = optionalGameObject;
        const matchValue = gameObject.Attributes;
        switch (matchValue.tag) {
            case 1: {
                const matchValue_2 = [equalsSafe(player.Position.GalacticPosition, position.GalacticPosition), Position_isAdjacent(player.Position.SectorPosition, position.SectorPosition), player.DockedWith == null];
                let pattern_matching_result;
                if (matchValue_2[0]) {
                    if (matchValue_2[1]) {
                        if (matchValue_2[2]) {
                            pattern_matching_result = 0;
                        }
                        else {
                            pattern_matching_result = 1;
                        }
                    }
                    else {
                        pattern_matching_result = 2;
                    }
                }
                else {
                    pattern_matching_result = 2;
                }
                switch (pattern_matching_result) {
                    case 0: {
                        return [(arg0_6 = ["Dock", (new UpdateGameStateMsg(7, position))], (new MenuItem(1, arg0_6)))];
                    }
                    case 1: {
                        return [(new MenuItem(1, ["Dock", new UpdateGameStateMsg(8)]))];
                    }
                    case 2: {
                        return [(new MenuItem(0, "Move next to the starbase to dock"))];
                    }
                }
            }
            case 2: {
                return [(new MenuItem(0, "A star. A really big star."))];
            }
            default: {
                const enemy = matchValue.fields[0];
                let matchValue_1;
                matchValue_1 = tryFind((p) => equalsSafe(p, position), player.Targets);
                if (matchValue_1 == null) {
                    if (Weapons_canAddTarget(player, gameObject)) {
                        return [(arg0_3 = ["Add target", (new UpdateGameStateMsg(5, position))], (new MenuItem(1, arg0_3)))];
                    }
                    else {
                        return [(new MenuItem(0, "Cannot add more targets"))];
                    }
                }
                else {
                    return [(arg0_1 = ["Remove target", (new UpdateGameStateMsg(6, position))], (new MenuItem(1, arg0_1)))];
                }
            }
        }
    }
}

export function Menu_view(menuOptions, gameDispatch) {
    let children_2, children_6;
    const children_8 = [(children_2 = [react.createElement("div", {
        className: "menuArrowUp",
    })], react.createElement("div", {
        className: "menuArrowUpContainer",
    }, ...children_2)), (children_6 = (map((mi) => {
        if (mi.tag === 1) {
            const text_1 = mi.fields[0][0];
            const action = mi.fields[0][1];
            const children_4 = [label(text_1)];
            return react.createElement("button", {
                onClick: (_arg1) => {
                    gameDispatch((new GameMsg(1, action)));
                },
            }, ...children_4);
        }
        else {
            const text = mi.fields[0];
            return label(text);
        }
    }, menuOptions.MenuItems)), react.createElement("div", {
        className: "menuBody",
    }, ...children_6))];
    return react.createElement("div", {
        className: "menu menuCentered",
    }, ...children_8);
}

export const phaserOverlay = FunctionComponent_Of_2F363EB5((props) => {
    let value_1, tuple, value_3, tuple_1, value_5, tuple_2, value_7, tuple_3, props_3, css_1, arg10_1, tuple_4, clo1_1, arg10_2, tuple_5, clo1_2, arg10_3, arg20_1, tuple_6, tuple_7, clo1_3, clo2_1, children_2;
    const player = props.player;
    const gridWidthPercentage = props.gridWidthPercentage;
    const gridHeightPercentage = props.gridHeightPercentage;
    const size = react.useState([0, 0]);
    const cachedTarget = react.useState(new Position(-1, -1));
    let patternInput;
    const matchValue = props.optionalTarget;
    if (matchValue == null) {
        patternInput = [0, cachedTarget[0]];
    }
    else {
        const target = matchValue;
        patternInput = [1, (!equalsSafe(cachedTarget[0], target.SectorPosition)) ? (cachedTarget[1](target.SectorPosition), target.SectorPosition) : target.SectorPosition];
    }
    const opacity = patternInput[0];
    const currentTarget = patternInput[1];
    const container = react.useRef(void 0);
    debouncedSize(container, (arg00) => {
        size[1](arg00);
    });
    const fromX = (((player.Position.SectorPosition.X) * gridWidthPercentage) + (gridWidthPercentage / 2)) * (value_1 = ((tuple = (size[0]), (tuple[0])) | 0), (value_1));
    const fromY = (((player.Position.SectorPosition.Y) * gridHeightPercentage) + (gridHeightPercentage / 2)) * (value_3 = ((tuple_1 = (size[0]), (tuple_1[1])) | 0), (value_3));
    const toX = (((currentTarget.X) * gridWidthPercentage) + (gridWidthPercentage / 2)) * (value_5 = ((tuple_2 = (size[0]), (tuple_2[0])) | 0), (value_5));
    const toY = (((currentTarget.Y) * gridHeightPercentage) + (gridHeightPercentage / 2)) * (value_7 = ((tuple_3 = (size[0]), (tuple_3[1])) | 0), (value_7));
    let linePath;
    const clo1 = toText(printf("M %f %f L %f %f"));
    const clo2 = clo1(fromX);
    const clo3 = clo2(fromY);
    const clo4 = clo3(toX);
    linePath = clo4(toY);
    const props_5 = [new HTMLAttr(25, "phaserOverlay"), new Prop(2, container), ["style", {
        opacity: opacity,
    }]];
    const children_4 = [(props_3 = [(css_1 = ofArray([new CSSProp(394, (arg10_1 = ((tuple_4 = (size[0]), (tuple_4[0])) | 0), (clo1_1 = toText(printf("%dpx")), clo1_1(arg10_1)))), new CSSProp(189, (arg10_2 = ((tuple_5 = (size[0]), (tuple_5[1])) | 0), (clo1_2 = toText(printf("%dpx")), clo1_2(arg10_2))))]), ["style", keyValueList(css_1, 1)]), new SVGAttr(39, (arg10_3 = ((tuple_6 = (size[0]), (tuple_6[0])) | 0), arg20_1 = ((tuple_7 = (size[0]), (tuple_7[1])) | 0), (clo1_3 = toText(printf("0 0 %d %d")), clo2_1 = clo1_3(arg10_3), clo2_1(arg20_1))))], (children_2 = [react.createElement("path", {
        d: linePath,
        stroke: "orange",
        strokeWidth: 3,
    })], react.createElement("svg", keyValueList(props_3, 1), ...children_2)))];
    return react.createElement("div", keyValueList(props_5, 1), ...children_4);
});

export function view(isUiDisabled, explosions, gameObjects, player, menuItems, optionalPhaserTarget, dispatch, gameDispatch) {
    let value, value_1, children_8, source2, source1, props_6, css_2, clo1_4, clo2, children_6, props_4, children_4, clo1_5, clo1_6;
    const gridWidthPercentage = 1 / (value = ((GameWorldPosition_get_Max().SectorPosition.Y + 1) | 0), (value));
    let gridWidthPercentageAsString;
    const arg10 = gridWidthPercentage * 100;
    const clo1 = toText(printf("%f%%"));
    gridWidthPercentageAsString = clo1(arg10);
    const gridHeightPercentage = 1 / (value_1 = ((GameWorldPosition_get_Max().SectorPosition.X + 1) | 0), (value_1));
    let gridHeightPercentageAsString;
    const arg10_1 = gridHeightPercentage * 100;
    const clo1_1 = toText(printf("%f%%"));
    gridHeightPercentageAsString = clo1_1(arg10_1);
    let numberOfRows;
    const value_2 = (GameWorldPosition_get_Max().SectorPosition.Y + 1) | 0;
    numberOfRows = value_2;
    let numberOfColumns;
    const value_3 = (GameWorldPosition_get_Max().SectorPosition.X + 1) | 0;
    numberOfColumns = value_3;
    const getLeft = (x) => {
        let arg10_2, value_5, clo1_2;
        return new CSSProp(207, (arg10_2 = (((x) / (value_5 = ((GameWorldPosition_get_Max().SectorPosition.X + 1) | 0), (value_5))) * 100), (clo1_2 = toText(printf("%f%%")), clo1_2(arg10_2))));
    };
    const getTop = (y) => {
        let arg10_3, value_7, clo1_3;
        return new CSSProp(365, (arg10_3 = (((y) / (value_7 = ((GameWorldPosition_get_Max().SectorPosition.Y + 1) | 0), (value_7))) * 100), (clo1_3 = toText(printf("%f%%")), clo1_3(arg10_3))));
    };
    const cssWidth = new CSSProp(394, gridWidthPercentageAsString);
    const cssHeight = new CSSProp(189, gridHeightPercentageAsString);
    const renderedSectorObjects = singleton((children_8 = (source2 = (map((go) => {
        let css, props, children;
        const props_2 = [new HTMLAttr(25, "gameObject"), (css = ofArray([getLeft(go.Position.SectorPosition.X), getTop(go.Position.SectorPosition.Y), cssWidth, cssHeight]), ["style", keyValueList(css, 1)])];
        const children_2 = [(props = [["style", {
            height: "80%",
            width: "80%",
        }]], (children = [(renderGameObject(go))], react.createElement("div", keyValueList(props, 1), ...children)))];
        return react.createElement("div", keyValueList(props_2, 1), ...children_2);
    }, gameObjects)), (source1 = singleton((props_6 = [new HTMLAttr(25, "gameObject"), (css_2 = ofArray([new CSSProp(372, (clo1_4 = toText(printf("left %s, top %s")), clo2 = clo1_4(scannerAnimationDurationCss), clo2(scannerAnimationDurationCss))), getLeft(player.Position.SectorPosition.X), getTop(player.Position.SectorPosition.Y), cssWidth, cssHeight]), ["style", keyValueList(css_2, 1)])], (children_6 = [(props_4 = [["style", {
        height: "80%",
        width: "80%",
    }]], (children_4 = [renderPlayer()], react.createElement("div", keyValueList(props_4, 1), ...children_4)))], react.createElement("div", keyValueList(props_6, 1), ...children_6)))), append(source1, source2))), react.createElement("div", {
        className: "gameObjects",
    }, ...children_8)));
    let renderedExplosions;
    renderedExplosions = map((explosion) => {
        let copyOfStruct, css_4, props_10, children_10;
        const position = explosion.fields[0];
        const props_12 = [new HTMLAttr(25, "explosion"), new Prop(0, (copyOfStruct = newGuid(), copyOfStruct)), (css_4 = ofArray([getLeft(position.SectorPosition.X), getTop(position.SectorPosition.Y), cssWidth, cssHeight]), ["style", keyValueList(css_4, 1)])];
        const children_12 = [(props_10 = [["style", {
            height: "80%",
            width: "80%",
        }]], (children_10 = [pixelatedScout({
            dispatch: (_arg1) => {
            },
        })], react.createElement("div", keyValueList(props_10, 1), ...children_10)))];
        return react.createElement("div", keyValueList(props_12, 1), ...children_12);
    }, explosions);
    let overlayGrid;
    let gridTemplateRows;
    let array;
    const source_2 = replicate(numberOfRows, (clo1_5 = toText(printf("%s ")), clo1_5(gridHeightPercentageAsString)));
    array = Array.from(source_2);
    gridTemplateRows = fold((x_1, y_1) => (x_1 + y_1), "", array);
    let gridTemplateColumns;
    let array_1;
    const source_3 = replicate(numberOfColumns, (clo1_6 = toText(printf("%s ")), clo1_6(gridWidthPercentageAsString)));
    array_1 = Array.from(source_3);
    gridTemplateColumns = fold((x_2, y_2) => (x_2 + y_2), "", array_1);
    const props_18 = [new HTMLAttr(25, "overlayGrid"), ["style", {
        gridTemplateRows: gridTemplateRows,
        gridTemplateColumns: gridTemplateColumns,
    }]];
    let children_18;
    const source_6 = Position_sectorCoordinateIterator();
    children_18 = map((xyPosition) => {
        let css_7;
        const gameWorldPosition = new GameWorldPosition(player.Position.GalacticPosition, xyPosition);
        const props_16 = [new DOMAttr(40, (_arg2) => {
            if (isUiDisabled) {
            }
            else if (menuItems == null) {
                let objectAtPosition;
                objectAtPosition = tryFind((go_2) => equalsSafe(go_2.Position, gameWorldPosition), gameObjects);
                const menuItems_1 = Menu_items(player, objectAtPosition, gameWorldPosition);
                if (isEmpty(menuItems_1)) {
                }
                else {
                    dispatch((new GameScreenMsg(2, [gameWorldPosition, menuItems_1])));
                }
            }
        }), (css_7 = ofArray([new CSSProp(291, "relative"), new CSSProp(183, (xyPosition.Y) + 1), new CSSProp(179, (xyPosition.Y) + 2), new CSSProp(176, (xyPosition.X) + 1), new CSSProp(174, (xyPosition.X) + 2)]), ["style", keyValueList(css_7, 1)])];
        const children_16 = ofSeq(delay(() => {
            if (menuItems == null) {
                return singleton_1(react.createElement(react.Fragment, {}));
            }
            else {
                const menu = menuItems;
                return equalsSafe(menu.Position, gameWorldPosition) ? singleton_1(Menu_view(menu, gameDispatch)) : singleton_1(react.createElement(react.Fragment, {}));
            }
        }));
        return react.createElement("div", keyValueList(props_16, 1), ...children_16);
    }, source_6);
    overlayGrid = react.createElement("div", keyValueList(props_18, 1), ...children_18);
    let verticalLines;
    const source_7 = rangeNumber(0, 1, numberOfColumns - 2);
    verticalLines = map((g) => {
        let value_13;
        let leftPercentage;
        const arg10_7 = ((g) / (value_13 = ((GameWorldPosition_get_Max().SectorPosition.X + 1) | 0), (value_13))) * 100;
        const clo1_7 = toText(printf("%f%%"));
        leftPercentage = clo1_7(arg10_7);
        const props_20 = [new HTMLAttr(25, "verticalLine"), ["style", keyValueList([new CSSProp(207, leftPercentage), cssWidth], 1)]];
        return react.createElement("div", keyValueList(props_20, 1));
    }, source_7);
    let horizontalLines;
    const source_8 = rangeNumber(0, 1, numberOfRows - 2);
    horizontalLines = map((g_1) => {
        let value_15;
        let topPercentage;
        const arg10_8 = ((g_1) / (value_15 = ((GameWorldPosition_get_Max().SectorPosition.Y + 1) | 0), (value_15))) * 100;
        const clo1_8 = toText(printf("%f%%"));
        topPercentage = clo1_8(arg10_8);
        const props_22 = [new HTMLAttr(25, "horizontalLine"), ["style", keyValueList([new CSSProp(365, topPercentage), cssHeight], 1)]];
        return react.createElement("div", keyValueList(props_22, 1));
    }, source_8);
    const phasers = phaserOverlay({
        gridHeightPercentage: gridHeightPercentage,
        gridWidthPercentage: gridWidthPercentage,
        optionalTarget: optionalPhaserTarget,
        player: player,
    });
    let children_24;
    let source2_5;
    let source2_4;
    let source2_3;
    let source2_2;
    source2_2 = append([phasers], [overlayGrid]);
    source2_3 = append(renderedSectorObjects, source2_2);
    source2_4 = append(renderedExplosions, source2_3);
    source2_5 = append(verticalLines, source2_4);
    children_24 = append(horizontalLines, source2_5);
    return react.createElement("div", {
        className: "shortRangeScanner",
    }, ...children_24);
}

