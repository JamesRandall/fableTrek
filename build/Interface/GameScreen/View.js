import * as react from "react";
import { FunctionComponent_Of_2F363EB5 } from "../../.fable/Fable.React.5.1.0/Fable.React.FunctionComponent.fs.js";
import { debouncedSize } from "../Browser.Helpers.js";
import { GameWorld_currentSectorObjects } from "../../Game/Utils.js";
import { ofSeq } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";
import { singleton, append, delay } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";
import { view } from "./StarField.js";
import { GameScreenMsg } from "./Types.js";
import { label } from "../Common.js";
import { view as view_1 } from "./ShortRangeScanner.js";
import { keyValueList } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/MapUtil.js";
import { HTMLAttr, DOMAttr, SVGAttr } from "../../.fable/Fable.React.5.1.0/Fable.React.Props.fs.js";
import { printf, toText } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/String.js";
import { view as view_2 } from "./LongRangeScanner.js";
import { view as view_3 } from "./DamageControl.js";
import { view as view_4 } from "./EnergyManagement.js";
import { view as view_5 } from "./Weapons.js";
import { Weapons_canFireTorpedoes, Weapons_canFirePhasers } from "../../Game/Rules.js";

export function transparentPopoverOverlay(onClose) {
    return react.createElement("div", {
        className: "transparentPopoverOverlay",
        onClick: (_arg1) => {
            onClose();
        },
    });
}

export const root = FunctionComponent_Of_2F363EB5((props) => {
    const game = props.Game;
    const gameDispatch = props.GameDispatch;
    const dispatch = props.Dispatch;
    const model = props.Model;
    const size = react.useState([-1, -1]);
    const starfieldContainerRef = react.useRef(void 0);
    debouncedSize(starfieldContainerRef, (arg00) => {
        size[1](arg00);
    });
    let currentObjects;
    let source;
    source = GameWorld_currentSectorObjects(game);
    currentObjects = Array.from(source);
    const children_54 = ofSeq(delay(() => {
        let children_1;
        return append(model.IsWarping ? singleton((children_1 = ofSeq(delay(() => {
            let tuple, Width, tuple_1, tuple_2;
            return ((tuple = (size[0]), (tuple[0])) > -1) ? singleton(view((Width = ((tuple_1 = (size[0]), (tuple_1[0])) | 0), {
                Height: (tuple_2 = (size[0]), (tuple_2[1])),
                Width: Width,
            }))) : singleton(react.createElement(react.Fragment, {}));
        })), react.createElement("div", {
            className: "starfield",
            onClick: (_arg1) => {
                dispatch(new GameScreenMsg(13));
            },
        }, ...children_1))) : singleton(react.createElement(react.Fragment, {})), delay(() => append((model.ShortRangeScannerMenuItems == null) ? singleton(react.createElement(react.Fragment, {})) : singleton(transparentPopoverOverlay(() => {
            dispatch(new GameScreenMsg(3));
        })), delay(() => append(model.IsLongRangeScannerVisible ? singleton(transparentPopoverOverlay(() => {
            dispatch(new GameScreenMsg(1));
        })) : singleton(react.createElement(react.Fragment, {})), delay(() => append(model.IsDamageControlVisible ? singleton(transparentPopoverOverlay(() => {
            dispatch(new GameScreenMsg(15));
        })) : singleton(react.createElement(react.Fragment, {})), delay(() => {
            let children_52;
            return singleton((children_52 = ofSeq(delay(() => {
                let children_7;
                return append(model.IsEnemyTurn ? singleton((children_7 = [label("Enemy turn in progress")], react.createElement("div", {
                    className: "message",
                }, ...children_7))) : singleton(react.createElement(react.Fragment, {})), delay(() => {
                    let children_50, children_36, children_18, children_14, children_12, children_16, clo1, clo1_1, children_34, children_23, children_28, children_32, children_38, children_48, children_42, props_41, children_46, props_45;
                    return singleton((children_50 = [view_1(model.IsUiDisabled, model.Explosions, currentObjects, game.Player, model.ShortRangeScannerMenuItems, model.CurrentTarget, dispatch, gameDispatch), (children_36 = [(children_18 = [(children_14 = [(children_12 = [react.createElement("path", keyValueList([["fill-rule", "evenodd"], new SVGAttr(6, "#ddd"), new SVGAttr(3, "M48.048 304h73.798v128c0 26.51 21.49 48 48 48h108.308c26.51 0 48-21.49 48-48V304h73.789c42.638 0 64.151-51.731 33.941-81.941l-175.943-176c-18.745-18.745-49.137-18.746-67.882 0l-175.952 176C-16.042 252.208 5.325 304 48.048 304zM224 80l176 176H278.154v176H169.846V256H48L224 80z")], 1))], react.createElement("svg", {
                        width: "100%",
                        height: "100%",
                        viewBox: "0 0 448 512",
                    }, ...children_12))], react.createElement("button", {}, ...children_14)), (children_16 = [label((clo1 = toText(printf("Stardate %.1f")), clo1(game.Stardate))), label((clo1_1 = toText(printf("Score %d")), clo1_1(game.Score)))], react.createElement("div", {}, ...children_16))], react.createElement("div", {
                        className: "scoreAndStartdate",
                    }, ...children_18)), (children_34 = [(children_23 = ofSeq(delay(() => append(model.IsLongRangeScannerVisible ? singleton(view_2({
                        DiscoveredSectors: game.DiscoveredSectors,
                        Dispatch: dispatch,
                        GameDispatch: gameDispatch,
                        GameObjects: game.GameObjects,
                        IsWarping: model.IsWarping,
                        Player: game.Player,
                        WarpDestinationOption: model.WarpDestination,
                    })) : singleton(react.createElement(react.Fragment, {})), delay(() => singleton(react.createElement("button", {
                        className: "plain",
                        disabled: model.IsUiDisabled,
                        onClick: (_arg2) => {
                            dispatch(new GameScreenMsg(0));
                        },
                    }, "Long Range")))))), react.createElement("div", {
                        className: "buttonContainer",
                    }, ...children_23)), (children_28 = ofSeq(delay(() => append(model.IsDamageControlVisible ? singleton(view_3(game)) : singleton(react.createElement(react.Fragment, {})), delay(() => singleton(react.createElement("button", {
                        className: "plain",
                        disabled: model.IsUiDisabled,
                        onClick: (_arg3) => {
                            dispatch(new GameScreenMsg(14));
                        },
                    }, "Damage Control")))))), react.createElement("div", {
                        className: "buttonContainer",
                    }, ...children_28)), (children_32 = [react.createElement("button", {
                        className: "plain",
                        disabled: model.IsUiDisabled,
                    }, "Log")], react.createElement("div", {
                        className: "buttonContainer",
                    }, ...children_32))], react.createElement("div", {
                        className: "bottomBarButtons",
                    }, ...children_34))], react.createElement("div", {
                        className: "bottomBar",
                    }, ...children_36)), (children_38 = [view_4({
                        dispatch: dispatch,
                        player: game.Player,
                    }), view_5({
                        gameDispatch: gameDispatch,
                        gameObjects: currentObjects,
                        player: game.Player,
                    })], react.createElement("div", {
                        className: "sideBar",
                    }, ...children_38)), (children_48 = [(children_42 = [(props_41 = [new DOMAttr(40, (_arg4) => {
                        dispatch(new GameScreenMsg(4));
                    }), new HTMLAttr(39, model.IsUiDisabled ? true : (!(Weapons_canFirePhasers(game.Player))))], react.createElement("button", keyValueList(props_41, 1), "Fire Phasers"))], react.createElement("div", {}, ...children_42)), (children_46 = [(props_45 = [new HTMLAttr(39, model.IsUiDisabled ? true : (!(Weapons_canFireTorpedoes(game.Player))))], react.createElement("button", keyValueList(props_45, 1), "Fire Torpedoes"))], react.createElement("div", {}, ...children_46))], react.createElement("div", {
                        className: "fireButtons",
                    }, ...children_48))], react.createElement("div", {
                        className: "innerContainer",
                    }, ...children_50)));
                }));
            })), react.createElement("div", {
                className: "outerContainer",
            }, ...children_52)));
        }))))))));
    }));
    return react.createElement("div", {
        className: "gameScreen",
        ref: starfieldContainerRef,
    }, ...children_54);
});

