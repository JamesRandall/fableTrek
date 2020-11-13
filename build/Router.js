import { Union } from "./.fable/fable-library.3.0.0-nagareyama-rc-005/Types.js";
import { union_type } from "./.fable/fable-library.3.0.0-nagareyama-rc-005/Reflection.js";
import { parseHash, oneOf, top, s, map } from "./.fable/Fable.Elmish.Browser.3.0.4/parser.fs.js";
import { ofArray } from "./.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";
import { HTMLAttr } from "./.fable/Fable.React.5.1.0/Fable.React.Props.fs.js";
import { Navigation_newUrl, Navigation_modifyUrl } from "./.fable/Fable.Elmish.Browser.3.0.4/navigation.fs.js";

export class Page extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["StartScreenPage", "GameScreenPage"];
    }
}

export function Page$reflection() {
    return union_type("Router.Page", [], Page, () => [[], []]);
}

function toHash(page) {
    if (page.tag === 1) {
        return "#/game";
    }
    else {
        return "#/";
    }
}

export const pageParser = (() => {
    const parsers = ofArray([map(new Page(1), s("game")), map(new Page(0), top)]);
    return (state_1) => oneOf(parsers, state_1);
})();

export function href(route) {
    return new HTMLAttr(54, toHash(route));
}

export function modifyUrl(route) {
    let newUrl_1;
    newUrl_1 = toHash(route);
    return Navigation_modifyUrl(newUrl_1);
}

export function newUrl(route) {
    let newUrl_1;
    newUrl_1 = toHash(route);
    return Navigation_newUrl(newUrl_1);
}

export function modifyLocation(route) {
    window.location.href = toHash(route);
}

export function currentRoute() {
    return parseHash(pageParser, window.location);
}

