import { Record } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Types.js";
import { GameMsg, UpdateGameStateMsg, GameWorldPosition_get_Max, GameWorldPosition, GameObject__get_IsEnemy, Position$reflection } from "../../Game/Types.js";
import { record_type, int32_type, bool_type } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Reflection.js";
import { groupBy } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Map.js";
import { hashSafe, equalsSafe } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Util.js";
import { contains, tryFind, replicate, sumBy, length, filter, map } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";
import { FunctionComponent_Of_2F363EB5 } from "../../.fable/Fable.React.5.1.0/Fable.React.FunctionComponent.fs.js";
import { DOMAttr, CSSProp, HTMLAttr } from "../../.fable/Fable.React.5.1.0/Fable.React.Props.fs.js";
import { join, printf, toText } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/String.js";
import * as react from "react";
import { keyValueList } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/MapUtil.js";
import { Movement_canMove } from "../../Game/Rules.js";
import { GameScreenMsg } from "./Types.js";
import { singleton, ofArray } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";
import { Position_galacticCoordinateIterator } from "../../Game/Utils.js";
import { label } from "../Common.js";
import { Browser_Types_Event__Event_get_Value } from "../../.fable/Fable.React.5.1.0/Fable.React.Extensions.fs.js";
import { parse } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Double.js";

export class SectorSummary extends Record {
    constructor(Position, IsStarbaseInSector, IsPlayerInSector, EnemyCount, StarCount) {
        super();
        this.Position = Position;
        this.IsStarbaseInSector = IsStarbaseInSector;
        this.IsPlayerInSector = IsPlayerInSector;
        this.EnemyCount = (EnemyCount | 0);
        this.StarCount = (StarCount | 0);
    }
}

export function SectorSummary$reflection() {
    return record_type("Interface.GameScreen.LongRangeScanner.SectorSummary", [], SectorSummary, () => [["Position", Position$reflection()], ["IsStarbaseInSector", bool_type], ["IsPlayerInSector", bool_type], ["EnemyCount", int32_type], ["StarCount", int32_type]]);
}

export function calculateSummaries(gameObjects, player) {
    let source_4;
    source_4 = groupBy((go) => go.Position.GalacticPosition, gameObjects, {
        Equals: equalsSafe,
        GetHashCode: hashSafe,
    });
    return map((tupledArg) => {
        let source_2;
        const position = tupledArg[0];
        const sectorObjects = tupledArg[1];
        return new SectorSummary(position, false, equalsSafe(player.Position.GalacticPosition, position), (source_2 = (filter(GameObject__get_IsEnemy, sectorObjects)), (length(source_2))), (sumBy((so_1) => {
            if (so_1.Attributes.tag === 2) {
                return 1;
            }
            else {
                return 0;
            }
        }, sectorObjects, {
            GetZero: () => 0,
            Add: (x_1, y_1) => (x_1 + y_1),
        })));
    }, source_4);
}

export const view = FunctionComponent_Of_2F363EB5((props) => {
    let children_34, children_14, props_13, css, count_1, value_2, children_12, source_2, children_32, children_28, range, rangeValue, percentage, rt, foregroundClass, children_20, children_18, props_19, css_2, arg10_6, arg10_7, rt_2, clo1_7, clo1_6, rangeValue_2, percentage_1, rt_3, foregroundClass_2, children_26, children_24, props_25, css_3, arg10_8, arg10_9, rt_5, clo1_9, clo1_8, props_33, children_38;
    const warpDestinationOption = props.WarpDestinationOption;
    const discoveredSectors = props.DiscoveredSectors;
    const gameObjects = props.GameObjects;
    const player = props.Player;
    const dispatch = props.Dispatch;
    const gameDispatch = props.GameDispatch;
    const intLabel = (tupledArg) => {
        let clo1, s, clo1_1;
        const colorClass = tupledArg[0];
        const intValue = tupledArg[1] | 0;
        const props_1 = [new HTMLAttr(25, (clo1 = toText(printf("label %s")), clo1(colorClass)))];
        const children = [(s = (clo1_1 = toText(printf("%d")), clo1_1(intValue)), s)];
        return react.createElement("div", keyValueList(props_1, 1), ...children);
    };
    const textLabel = (tupledArg_1) => {
        let clo1_2, s_1, clo1_3;
        const colorClass_1 = tupledArg_1[0];
        const strValue = tupledArg_1[1];
        const props_3 = [new HTMLAttr(25, (clo1_2 = toText(printf("label %s")), clo1_2(colorClass_1)))];
        const children_2 = [(s_1 = (clo1_3 = toText(printf("%s")), clo1_3(strValue)), s_1)];
        return react.createElement("div", keyValueList(props_3, 1), ...children_2);
    };
    let canWarp;
    if (warpDestinationOption == null) {
        canWarp = false;
    }
    else {
        const warpDestination = warpDestinationOption;
        canWarp = Movement_canMove(player, new GameWorldPosition(warpDestination, player.Position.SectorPosition));
    }
    let beginWarpClick;
    if (warpDestinationOption == null) {
        beginWarpClick = ((value) => {
            void value;
        });
    }
    else {
        const warpDestination_1 = warpDestinationOption;
        beginWarpClick = ((_arg1) => {
            dispatch((new GameScreenMsg(12, warpDestination_1)));
        });
    }
    const summaries = calculateSummaries(gameObjects, player);
    let templateColumns;
    let arg00;
    let count;
    const value_1 = (GameWorldPosition_get_Max().GalacticPosition.X + 1) | 0;
    count = value_1;
    arg00 = replicate(count, "1fr ");
    templateColumns = join("", arg00);
    const children_40 = [(children_34 = [(children_14 = [(props_13 = [(css = ofArray([new CSSProp(125, "grid"), new CSSProp(177, 0), new CSSProp(186, templateColumns), new CSSProp(187, (count_1 = ((value_2 = ((GameWorldPosition_get_Max().GalacticPosition.Y + 1) | 0), (value_2)) | 0), replicate(count_1, "1fr ")))]), ["style", keyValueList(css, 1)])], (children_12 = (source_2 = Position_galacticCoordinateIterator(), (map((position) => {
        let clo1_5, clo2_1, css_1, matchValue, children_6, summary, children_4, children_8;
        let optionalSummary;
        optionalSummary = tryFind((s_2) => equalsSafe(s_2.Position, position), summaries);
        let baseCellClass;
        const evenOdd = ((((position.Y) + ((position.X) % 2)) % 2) === 0) ? "even" : "odd";
        const arg20 = equalsSafe(player.Position.GalacticPosition, position) ? "playerSector" : "";
        const clo1_4 = toText(printf("scannerCell %s %s"));
        const clo2 = clo1_4(evenOdd);
        baseCellClass = clo2(arg20);
        let patternInput;
        const baseClickHandler = equalsSafe(player.Position.GalacticPosition, position) ? ((_arg2) => {
            dispatch(new GameScreenMsg(11));
        }) : ((_arg3) => {
            dispatch((new GameScreenMsg(10, position)));
        });
        if (warpDestinationOption == null) {
            patternInput = [baseCellClass, baseClickHandler];
        }
        else {
            const warpDestination_2 = warpDestinationOption;
            patternInput = (equalsSafe(warpDestination_2, position) ? [(clo1_5 = toText(printf("%s %s")), clo2_1 = clo1_5(baseCellClass), clo2_1("warpDestination")), (value_5) => {
                void value_5;
            }] : [baseCellClass, baseClickHandler]);
        }
        const onClickHandler = patternInput[1];
        const cellClass = patternInput[0];
        const props_11 = [(css_1 = ofArray([new CSSProp(173, position.X + 1), new CSSProp(178, position.Y + 1)]), ["style", keyValueList(css_1, 1)])];
        const children_10 = [(matchValue = (contains(position, discoveredSectors)), matchValue ? ((optionalSummary == null) ? (children_6 = [intLabel(["safe", 0]), intLabel(["noStarbase", 0]), intLabel(["star", 0])], react.createElement("div", {
            className: cellClass,
            onClick: onClickHandler,
        }, ...children_6)) : (summary = optionalSummary, (children_4 = [intLabel([(summary.EnemyCount > 0) ? "danger" : "safe", summary.EnemyCount]), intLabel(summary.IsStarbaseInSector ? ["noStarbase", 0] : ["starbase", 1]), intLabel(["star", summary.StarCount])], react.createElement("div", {
            className: cellClass,
            onClick: onClickHandler,
        }, ...children_4)))) : (children_8 = [textLabel(["undiscovered", "?"]), textLabel(["undiscovered", "?"]), textLabel(["undiscovered", "?"])], react.createElement("div", {
            className: cellClass,
            onClick: onClickHandler,
        }, ...children_8)))];
        return react.createElement("div", keyValueList(props_11, 1), ...children_10);
    }, source_2))), react.createElement("div", keyValueList(props_13, 1), ...children_12)))], react.createElement("div", {
        className: "scannerBody",
    }, ...children_14)), (children_32 = [(children_28 = [label("Speed"), (range = player.WarpSpeed, react.createElement("input", {
        type: "range",
        className: "rangeGreen",
        min: range.Min,
        max: range.Max,
        value: range.Current,
        onChange: (ev) => {
            let arg0_3, arg0_2;
            const newValue = Browser_Types_Event__Event_get_Value(ev);
            gameDispatch((arg0_3 = (arg0_2 = (parse(newValue) * 1), (new UpdateGameStateMsg(1, arg0_2))), (new GameMsg(1, arg0_3))));
        },
    })), label("Engines"), (rangeValue = player.WarpDrive, (percentage = (rt = rangeValue, ((rt.Current) - (rt.Min)) / ((rt.Max) - (rt.Min))), (foregroundClass = ("levelIndicatorForeground" + ((percentage > 0.5) ? "Healthy" : ((percentage > 0.25) ? "Warning" : "Danger"))), (children_20 = [(children_18 = [(props_19 = [new HTMLAttr(25, foregroundClass), (css_2 = singleton(new CSSProp(394, (arg10_6 = (arg10_7 = ((rt_2 = rangeValue, ((rt_2.Current) - (rt_2.Min)) / ((rt_2.Max) - (rt_2.Min))) * 100), (clo1_7 = toText(printf("%.0f")), clo1_7(arg10_7))), (clo1_6 = toText(printf("%s%%")), clo1_6(arg10_6))))), ["style", keyValueList(css_2, 1)])], react.createElement("div", keyValueList(props_19, 1)))], react.createElement("div", {
        className: "levelIndicatorBackground",
    }, ...children_18))], react.createElement("div", {
        className: "levelIndicator",
    }, ...children_20))))), label("Deflector"), (rangeValue_2 = player.DeflectorDish, (percentage_1 = (rt_3 = rangeValue_2, ((rt_3.Current) - (rt_3.Min)) / ((rt_3.Max) - (rt_3.Min))), (foregroundClass_2 = ("levelIndicatorForeground" + ((percentage_1 > 0.5) ? "Healthy" : ((percentage_1 > 0.25) ? "Warning" : "Danger"))), (children_26 = [(children_24 = [(props_25 = [new HTMLAttr(25, foregroundClass_2), (css_3 = singleton(new CSSProp(394, (arg10_8 = (arg10_9 = ((rt_5 = rangeValue_2, ((rt_5.Current) - (rt_5.Min)) / ((rt_5.Max) - (rt_5.Min))) * 100), (clo1_9 = toText(printf("%.0f")), clo1_9(arg10_9))), (clo1_8 = toText(printf("%s%%")), clo1_8(arg10_8))))), ["style", keyValueList(css_3, 1)])], react.createElement("div", keyValueList(props_25, 1)))], react.createElement("div", {
        className: "levelIndicatorBackground",
    }, ...children_24))], react.createElement("div", {
        className: "levelIndicator",
    }, ...children_26)))))], react.createElement("div", {
        className: "gauges",
    }, ...children_28)), (props_33 = [new HTMLAttr(25, "warp"), new HTMLAttr(39, (!canWarp)), new DOMAttr(40, beginWarpClick)], react.createElement("button", keyValueList(props_33, 1), "Engage"))], react.createElement("div", {
        className: "scannerFooter",
    }, ...children_32))], react.createElement("div", {
        className: "scannerOuter",
    }, ...children_34)), (children_38 = [react.createElement("div", {
        className: "arrowDown",
    })], react.createElement("div", {
        className: "arrowDownContainer",
    }, ...children_38))];
    return react.createElement("div", {
        className: "longRangeScanner",
    }, ...children_40);
});

