import { partialApply, randomNext } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Util.js";
import { Record } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Types.js";
import { record_type, string_type, float64_type } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Reflection.js";
import { FunctionComponent_Of_2F363EB5 } from "../../.fable/Fable.React.5.1.0/Fable.React.FunctionComponent.fs.js";
import { map, rangeNumber, iterate } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";
import * as react from "react";
import { map as map_1 } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Array.js";
import { HTMLAttr } from "../../.fable/Fable.React.5.1.0/Fable.React.Props.fs.js";
import { keyValueList } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/MapUtil.js";

const random = {};

const speed = 0.04;

function newCoord(size) {
    const value = (randomNext(0, size) - (~(~(size / 2)))) | 0;
    return value;
}

function getRandomColor() {
    const diceRoll = randomNext(0, 100) | 0;
    if (diceRoll < 25) {
        return "rgb(230,3,3)";
    }
    else if (diceRoll < 50) {
        return "rgb(252,186,3)";
    }
    else if (diceRoll < 75) {
        return "rgb(192,192,192)";
    }
    else {
        return "cyan";
    }
}

export class Star extends Record {
    constructor(X, Y, PreviousX, PreviousY, Z, Color) {
        super();
        this.X = X;
        this.Y = Y;
        this.PreviousX = PreviousX;
        this.PreviousY = PreviousY;
        this.Z = Z;
        this.Color = Color;
    }
}

export function Star$reflection() {
    return record_type("Interface.GameScreen.StarField.Star", [], Star, () => [["X", float64_type], ["Y", float64_type], ["PreviousX", float64_type], ["PreviousY", float64_type], ["Z", float64_type], ["Color", string_type]]);
}

export function Star_Create(width, height) {
    let value;
    let newX;
    newX = newCoord(width);
    let newY;
    newY = newCoord(height);
    const newStar = new Star(newX, newY, newX, newY, (value = (randomNext(0, 400) | 0), (value)) / 100, getRandomColor());
    return newStar;
}

export function Star__Update(st, width, height) {
    const newZ = st.Z + speed;
    const updatedStar = new Star(st.X + ((st.X * (speed * 0.2)) * newZ), st.Y + ((st.Y * (speed * 0.2)) * newZ), st.X, st.Y, newZ, st.Color);
    if ((((updatedStar.X > ((width / 2) + 50)) ? true : (updatedStar.X < (((-width) / 2) - 50))) ? true : (updatedStar.Y > ((height / 2) + 50))) ? true : (updatedStar.Y < (((-height) / 2) - 50))) {
        let newX;
        let size;
        size = (~(~width));
        newX = newCoord(size);
        let newY;
        let size_1;
        size_1 = (~(~height));
        newY = newCoord(size_1);
        return new Star(newX, newY, newX, newY, 0, updatedStar.Color);
    }
    else {
        return updatedStar;
    }
}

export const view = FunctionComponent_Of_2F363EB5((props) => {
    let halfWidth;
    const value = (~(~(props.Width / 2))) | 0;
    halfWidth = value;
    let halfHeight;
    const value_1 = (~(~(props.Height / 2))) | 0;
    halfHeight = value_1;
    const renderStars = (context, stars) => {
        context.fillRect((-halfWidth) - 1, (-halfHeight) - 1, (halfWidth * 2) + 2, (halfHeight * 2) + 2);
        iterate((s) => {
            context.lineWidth = s.Z;
            context.strokeStyle = s.Color;
            context.beginPath();
            context.moveTo(s.X, s.Y);
            context.lineTo(s.PreviousX, s.PreviousY);
            context.stroke();
        }, stars);
    };
    react.useEffect(() => {
        let stars_1;
        let source_2;
        const source_1 = rangeNumber(0, 1, 200);
        source_2 = map((_arg1) => Star_Create(props.Width, props.Height), source_1);
        stars_1 = Array.from(source_2);
        const context_1 = document.getElementById("starfield").getContext('2d');
        context_1.fillStyle = "rgba(0,0,0,0.1)";
        context_1.translate(halfWidth, halfHeight);
        const updateAndDraw = (_arg1_1) => {
            const array = stars_1;
            stars_1 = map_1((s_1) => {
                const arg00_1 = halfWidth * 2;
                const arg10_1 = halfHeight * 2;
                return Star__Update(s_1, arg00_1, arg10_1);
            }, array);
            const arg10_2 = stars_1;
            const clo1 = partialApply(1, renderStars, [context_1]);
            clo1(arg10_2);
            const starfield = document.getElementById("starfield");
            if (!(starfield == null)) {
                const value_2 = window.requestAnimationFrame(updateAndDraw);
                void value_2;
            }
        };
        updateAndDraw(0);
    }, []);
    const props_1 = [new HTMLAttr(59, "starfield"), new HTMLAttr(123, props.Width), new HTMLAttr(51, props.Height), ["style", {
        background: "radial-gradient(ellipse at center, #242938, #000 100%)",
    }]];
    return react.createElement("canvas", keyValueList(props_1, 1));
});

