import * as react from "react";
import { empty } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";

export function attachWindowEvent(f, node, eventType) {
    node.addEventListener(eventType, f);
    return {
        Dispose() {
            node.removeEventListener(eventType, f);
        },
    };
}

export function attachWindowEventWithDisposer(f, node, eventType, disposer) {
    node.addEventListener(eventType, f);
    return {
        Dispose() {
            disposer();
            node.removeEventListener(eventType, f);
        },
    };
}

export function debouncedSize(ref, setSize) {
    react.useEffect(() => (() => {
        let timeoutId = void 0;
        const respondToSize = () => {
            const matchValue = ref.current;
            if (matchValue == null) {
            }
            else {
                const elementRef = matchValue;
                const element = elementRef;
                let width;
                const value = element.clientWidth;
                width = (~(~value));
                let height;
                const value_1 = element.clientHeight;
                height = (~(~value_1));
                if (timeoutId == null) {
                }
                else {
                    const tid = timeoutId;
                    window.clearTimeout(tid);
                }
                timeoutId = window.setTimeout((_arg1) => {
                    timeoutId = (void 0);
                    return setSize([width, height]);
                }, 150, empty());
            }
        };
        respondToSize();
        return attachWindowEventWithDisposer((_arg2) => {
            respondToSize();
        }, window, "resize", () => {
            if (timeoutId == null) {
            }
            else {
                const tid_1 = timeoutId;
                window.clearTimeout(tid_1);
            }
        });
    })().Dispose, []);
}

