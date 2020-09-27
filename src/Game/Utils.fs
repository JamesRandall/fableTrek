module Game.Utils

open Types

module GameWorld =
  let currentSectorObjects game =
    game.GameObjects |> Seq.filter (fun go -> go.Position.GalacticPosition = game.Player.Position.GalacticPosition)

module Position =
  let private random = System.Random(1) // always seed it with the same number while developing! Makes for a fixed predictable game.
  
  let randomPositions =    
    let newRandomPosition () =
      { GalacticPosition =
          { X = random.Next(0, GameWorldPosition.Max.GalacticPosition.X |> int) * 1<coordinatecomponent>
            Y = random.Next(0, GameWorldPosition.Max.GalacticPosition.Y |> int) * 1<coordinatecomponent>
          }
        SectorPosition = 
          { X = random.Next(0, GameWorldPosition.Max.SectorPosition.X |> int) * 1<coordinatecomponent>
            Y = random.Next(0, GameWorldPosition.Max.SectorPosition.Y |> int) * 1<coordinatecomponent>
          }
      }
    seq { while true do yield newRandomPosition () }

  let positionIsVacant gameObjects position =
    gameObjects |> Seq.tryFind (fun go -> go.Position = position) |> Option.isNone

  let findRandomAndVacantGalacticPosition gameObjects =
    randomPositions |> Seq.skipWhile (positionIsVacant gameObjects >> not) |> Seq.head

  let sectorCoordinateIterator () =
    seq {
      for x in 0..(GameWorldPosition.Max.SectorPosition.X |> int) do
        for y in 0..(GameWorldPosition.Max.SectorPosition.Y |> int) do
          yield (x * 1<coordinatecomponent> ,y * 1<coordinatecomponent>)
    }