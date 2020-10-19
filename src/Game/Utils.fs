module Game.Utils

open Types

module GameWorld =
  let currentSectorObjects game =
    game.GameObjects |> Seq.filter (fun go -> go.Position.GalacticPosition = game.Player.Position.GalacticPosition)

  let objectInCurrentSector game position =
    game |> currentSectorObjects |> Seq.tryFind(fun go -> go.Position.SectorPosition = position)

  let positionInCurrentSector game position =
    { game.Player.Position with SectorPosition = position}

  let objectAtPosition gameObjects position =
    gameObjects |> Seq.tryFind(fun go -> go.Position = position)

  let replaceGameObject gameObjects newObject =
    gameObjects |> Array.map(fun go -> if go.Position = newObject.Position then newObject else go)

  let removeGameObject gameObjects toRemove =
    gameObjects |> Array.filter(fun go -> go <> toRemove)

module Position =
  let private random = System.Random(1) // always seed it with the same number while developing! Makes for a fixed predictable game.
  
  let randomPositions =    
    let newRandomPosition () =
      // Note that Next(min,max) - min is inclusive, max is exclusive
      { GalacticPosition =
          { X = random.Next(0, (GameWorldPosition.Max.GalacticPosition.X |> int) + 1) * 1<coordinatecomponent>
            Y = random.Next(0, (GameWorldPosition.Max.GalacticPosition.Y |> int) + 1) * 1<coordinatecomponent>
          }
        SectorPosition = 
          { X = random.Next(0, (GameWorldPosition.Max.SectorPosition.X |> int) + 1) * 1<coordinatecomponent>
            Y = random.Next(0, (GameWorldPosition.Max.SectorPosition.Y |> int) + 1) * 1<coordinatecomponent>
          }
      }
    seq { while true do yield newRandomPosition () }

  let randomSectorPositions galacticPosition =    
    let newRandomPosition () =
      // Note that Next(min,max) - min is inclusive, max is exclusive
      { GalacticPosition = galacticPosition
        SectorPosition = 
          { X = random.Next(0, (GameWorldPosition.Max.SectorPosition.X |> int) + 1) * 1<coordinatecomponent>
            Y = random.Next(0, (GameWorldPosition.Max.SectorPosition.Y |> int) + 1) * 1<coordinatecomponent>
          }
      }
    seq { while true do yield newRandomPosition () }

  let positionIsVacant gameObjects position =
    gameObjects |> Seq.tryFind (fun go -> go.Position = position) |> Option.isNone

  let findRandomAndVacantGalacticPosition gameObjects =
    randomPositions |> Seq.skipWhile (positionIsVacant gameObjects >> not) |> Seq.head

  let findRandomAndVacantSectorPosition gameObjects galacticPosition  =
    let sectorGameObjects = gameObjects |> Seq.filter(fun go -> go.Position.GalacticPosition = galacticPosition) |> Seq.toArray
    (galacticPosition |> randomSectorPositions |> Seq.skipWhile (positionIsVacant sectorGameObjects >> not) |> Seq.head).SectorPosition

  let sectorCoordinateIterator () =
    seq {
      for x in 0..(GameWorldPosition.Max.SectorPosition.X |> int) do
        for y in 0..(GameWorldPosition.Max.SectorPosition.Y |> int) do
          yield { X =x * 1<coordinatecomponent> ; Y = y * 1<coordinatecomponent> }
    }

  let galacticCoordinateIterator () =
    seq {
      for x in 0..(GameWorldPosition.Max.GalacticPosition.X |> int) do
        for y in 0..(GameWorldPosition.Max.GalacticPosition.Y |> int) do
          yield { X =x * 1<coordinatecomponent> ; Y = y * 1<coordinatecomponent> }
    }

  let isAdjacent position1 position2 =
    position1 <> position2 &&
    position1.X <= (position2.X+1<coordinatecomponent>) &&
    position1.X >= (position2.X-1<coordinatecomponent>) &&
    position1.Y <= (position2.Y+1<coordinatecomponent>) &&
    position1.Y >= (position2.Y-1<coordinatecomponent>)
    


    

  