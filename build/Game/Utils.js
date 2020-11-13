import { equals, equalsSafe, randomNext } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Util.js";
import { rangeNumber, map as map_1, collect, head, skipWhile, singleton, enumerateWhile, delay, tryFind, filter } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Seq.js";
import { Position, GameWorldPosition_get_Max, GameWorldPosition } from "./Types.js";
import { map } from "../.fable/fable-library.3.0.0-nagareyama-rc-005/Array.js";

const random = {};

export function rollDice() {
    return randomNext(0, 100);
}

export function GameWorld_currentSectorObjects(game) {
    return filter((go) => equalsSafe(go.Position.GalacticPosition, game.Player.Position.GalacticPosition), game.GameObjects);
}

export function GameWorld_objectInCurrentSector(game, position) {
    let source;
    source = GameWorld_currentSectorObjects(game);
    return tryFind((go) => equalsSafe(go.Position.SectorPosition, position), source);
}

export function GameWorld_positionInCurrentSector(game, position) {
    return new GameWorldPosition(game.Player.Position.GalacticPosition, position);
}

export function GameWorld_objectAtPosition(gameObjects, position) {
    return tryFind((go) => equalsSafe(go.Position, position), gameObjects);
}

export function GameWorld_replaceGameObject(gameObjects, newObject) {
    return map((go) => {
        if (equalsSafe(go.Position, newObject.Position)) {
            return newObject;
        }
        else {
            return go;
        }
    }, gameObjects);
}

export function GameWorld_removeGameObject(gameObjects, toRemove) {
    return gameObjects.filter((go) => (!equals(go, toRemove)));
}

const Position_random = {};

export const Position_randomPositions = (() => {
    const newRandomPosition = () => {
        let value, value_1, value_2, value_3;
        return new GameWorldPosition(new Position(randomNext(0, (value = (GameWorldPosition_get_Max().GalacticPosition.X | 0), (value)) + 1) * 1, randomNext(0, (value_1 = (GameWorldPosition_get_Max().GalacticPosition.Y | 0), (value_1)) + 1) * 1), new Position(randomNext(0, (value_2 = (GameWorldPosition_get_Max().SectorPosition.X | 0), (value_2)) + 1) * 1, randomNext(0, (value_3 = (GameWorldPosition_get_Max().SectorPosition.Y | 0), (value_3)) + 1) * 1));
    };
    return delay(() => enumerateWhile(() => true, delay(() => singleton(newRandomPosition()))));
})();

export function Position_randomSectorPositions(galacticPosition) {
    const newRandomPosition = () => {
        let value, value_1;
        return new GameWorldPosition(galacticPosition, new Position(randomNext(0, (value = (GameWorldPosition_get_Max().SectorPosition.X | 0), (value)) + 1) * 1, randomNext(0, (value_1 = (GameWorldPosition_get_Max().SectorPosition.Y | 0), (value_1)) + 1) * 1));
    };
    return delay(() => enumerateWhile(() => true, delay(() => singleton(newRandomPosition()))));
}

export function Position_positionIsVacant(gameObjects, position) {
    let option;
    option = tryFind((go) => equalsSafe(go.Position, position), gameObjects);
    return option == null;
}

export function Position_findRandomAndVacantGalacticPosition(gameObjects) {
    let source_1;
    source_1 = skipWhile((arg) => {
        let value;
        value = Position_positionIsVacant(gameObjects, arg);
        return !value;
    }, Position_randomPositions);
    return head(source_1);
}

export function Position_findRandomAndVacantSectorPosition(gameObjects, galacticPosition) {
    let source_3, source_2;
    let sectorGameObjects;
    let source_1;
    source_1 = filter((go) => equalsSafe(go.Position.GalacticPosition, galacticPosition), gameObjects);
    sectorGameObjects = Array.from(source_1);
    return (source_3 = (source_2 = (Position_randomSectorPositions(galacticPosition)), (skipWhile((arg) => {
        let value;
        value = Position_positionIsVacant(sectorGameObjects, arg);
        return !value;
    }, source_2))), (head(source_3))).SectorPosition;
}

export function Position_sectorCoordinateIterator() {
    return delay(() => {
        let value_1;
        return collect((x) => {
            let value;
            return map_1((y) => (new Position(x * 1, y * 1)), rangeNumber(0, 1, (value = (GameWorldPosition_get_Max().SectorPosition.Y | 0), (value))));
        }, rangeNumber(0, 1, (value_1 = (GameWorldPosition_get_Max().SectorPosition.X | 0), (value_1))));
    });
}

export function Position_galacticCoordinateIterator() {
    return delay(() => {
        let value_1;
        return collect((x) => {
            let value;
            return map_1((y) => (new Position(x * 1, y * 1)), rangeNumber(0, 1, (value = (GameWorldPosition_get_Max().GalacticPosition.Y | 0), (value))));
        }, rangeNumber(0, 1, (value_1 = (GameWorldPosition_get_Max().GalacticPosition.X | 0), (value_1))));
    });
}

export function Position_isAdjacent(position1, position2) {
    if ((((!equalsSafe(position1, position2)) ? (position1.X <= (position2.X + 1)) : false) ? (position1.X >= (position2.X - 1)) : false) ? (position1.Y <= (position2.Y + 1)) : false) {
        return position1.Y >= (position2.Y - 1);
    }
    else {
        return false;
    }
}

