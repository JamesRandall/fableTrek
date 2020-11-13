import { Record, Union } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Types.js";
import { list_type, option_type, bool_type, record_type, array_type, union_type, tuple_type, string_type } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/Reflection.js";
import { Position$reflection, GameWorldPosition$reflection, UpdateGameStateMsg$reflection } from "../../Game/Types.js";
import { empty } from "../../.fable/fable-library.3.0.0-nagareyama-rc-005/List.js";

export class MenuItem extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["NoActionLabel", "MenuItem"];
    }
}

export function MenuItem$reflection() {
    return union_type("Interface.GameScreen.Types.MenuItem", [], MenuItem, () => [[["Item", string_type]], [["Item", tuple_type(string_type, UpdateGameStateMsg$reflection())]]]);
}

export class ShortRangeScannerMenu extends Record {
    constructor(Position, MenuItems) {
        super();
        this.Position = Position;
        this.MenuItems = MenuItems;
    }
}

export function ShortRangeScannerMenu$reflection() {
    return record_type("Interface.GameScreen.Types.ShortRangeScannerMenu", [], ShortRangeScannerMenu, () => [["Position", GameWorldPosition$reflection()], ["MenuItems", array_type(MenuItem$reflection())]]);
}

export class Explosion extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["ExplodingEnemyScout"];
    }
}

export function Explosion$reflection() {
    return union_type("Interface.GameScreen.Types.Explosion", [], Explosion, () => [[["Item", GameWorldPosition$reflection()]]]);
}

export class Model extends Record {
    constructor(IsUiDisabled, IsLongRangeScannerVisible, ShortRangeScannerMenuItems, FiringTargets, CurrentTarget, Explosions, WarpDestination, IsWarping, IsDamageControlVisible, IsCaptainsLogVisible, IsEnemyTurn) {
        super();
        this.IsUiDisabled = IsUiDisabled;
        this.IsLongRangeScannerVisible = IsLongRangeScannerVisible;
        this.ShortRangeScannerMenuItems = ShortRangeScannerMenuItems;
        this.FiringTargets = FiringTargets;
        this.CurrentTarget = CurrentTarget;
        this.Explosions = Explosions;
        this.WarpDestination = WarpDestination;
        this.IsWarping = IsWarping;
        this.IsDamageControlVisible = IsDamageControlVisible;
        this.IsCaptainsLogVisible = IsCaptainsLogVisible;
        this.IsEnemyTurn = IsEnemyTurn;
    }
}

export function Model$reflection() {
    return record_type("Interface.GameScreen.Types.Model", [], Model, () => [["IsUiDisabled", bool_type], ["IsLongRangeScannerVisible", bool_type], ["ShortRangeScannerMenuItems", option_type(ShortRangeScannerMenu$reflection())], ["FiringTargets", list_type(GameWorldPosition$reflection())], ["CurrentTarget", option_type(GameWorldPosition$reflection())], ["Explosions", list_type(Explosion$reflection())], ["WarpDestination", option_type(Position$reflection())], ["IsWarping", bool_type], ["IsDamageControlVisible", bool_type], ["IsCaptainsLogVisible", bool_type], ["IsEnemyTurn", bool_type]]);
}

export function Model_get_Empty() {
    return new Model(false, false, void 0, empty(), void 0, empty(), void 0, false, false, false, false);
}

export class GameRequestMsg extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["ToggleShields", "WarpTo", "FirePhasersAtTarget"];
    }
}

export function GameRequestMsg$reflection() {
    return union_type("Interface.GameScreen.Types.GameRequestMsg", [], GameRequestMsg, () => [[], [["Item", Position$reflection()]], [["Item", GameWorldPosition$reflection()]]]);
}

export class GameScreenMsg extends Union {
    constructor(tag, ...fields) {
        super();
        this.tag = (tag | 0);
        this.fields = fields;
    }
    cases() {
        return ["ShowLongRangeScanner", "HideLongRangeScanner", "ShowShortRangeScannerMenu", "HideShortRangeScannerMenu", "FirePhasers", "FirePhasersAtNextTarget", "ShowExplosion", "CompleteExplosion", "DisableUi", "EnableUi", "SetWarpDestination", "RemoveWarpDestination", "BeginWarpTo", "EndWarpTo", "ShowDamageControl", "HideDamageControl", "GameRequest"];
    }
}

export function GameScreenMsg$reflection() {
    return union_type("Interface.GameScreen.Types.GameScreenMsg", [], GameScreenMsg, () => [[], [], [["Item", tuple_type(GameWorldPosition$reflection(), array_type(MenuItem$reflection()))]], [], [], [], [["Item", Explosion$reflection()]], [["Item", Explosion$reflection()]], [], [], [["Item", Position$reflection()]], [], [["Item", Position$reflection()]], [], [], [], [["Item", GameRequestMsg$reflection()]]]);
}

