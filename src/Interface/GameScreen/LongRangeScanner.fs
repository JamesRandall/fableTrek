module Interface.GameScreen.LongRangeScanner

open Fable.React
open Fable.React.Props
open Interface.Common
open Game.Types

type SectorSummary =
  { Position: Position
    IsStarbaseInSector: bool
    IsPlayerInSector: bool
    EnemyCount: int
    StarCount:int
  }

let calculateSummaries (gameObjects:GameObject seq) (player:Player) =
  gameObjects
  |> Seq.groupBy (fun go -> go.Position.GalacticPosition)
  |> Seq.map(fun (position,sectorObjects) -> 
    { Position = position
      IsStarbaseInSector = false
      IsPlayerInSector = player.Position.GalacticPosition = position
      EnemyCount = sectorObjects |> Seq.filter(fun so -> so.IsEnemy) |> Seq.length
      StarCount = sectorObjects |> Seq.sumBy(fun so -> match so.Attributes with | StarAttributes _ -> 1 | _ -> 0)
    }
  )

let view discoveredSectors gameObjects player dispatch =  
  let intLabel (colorClass,intValue) =
    div [Class (sprintf "label %s" colorClass)] [str (sprintf "%d" intValue)]
  let textLabel (colorClass,strValue) =
    div [Class (sprintf "label %s" colorClass)] [str (sprintf "%s" strValue)]
  
  let summaries = calculateSummaries gameObjects player
  let templateColumns = "1fr " |> Seq.replicate (GameWorldPosition.Max.GalacticPosition.X + 1<coordinatecomponent> |> int) |> System.String.Concat
  div [Class "longRangeScanner"] [
    div [Class "scannerOuter"] [
      div [Class "scannerBody"] [
        div [Style [
          Display DisplayOptions.Grid
          GridGap 0
          GridTemplateColumns templateColumns
          GridTemplateRows ("1fr " |> Seq.replicate (GameWorldPosition.Max.GalacticPosition.Y + 1<coordinatecomponent> |> int))
        ]] (
          Game.Utils.Position.galacticCoordinateIterator () |> Seq.map (fun position ->
            let optionalSummary = summaries |> Seq.tryFind(fun s -> s.Position = position)
            let cellClass =
              if (((position.Y |> int) + ((position.X |> int) % 2)) % 2) = 0 then
                "scannerCell even"
              else
                "scannerCell odd"
            div [Style [GridColumn (position.X+1<coordinatecomponent>) ; GridRow (position.Y+1<coordinatecomponent>)]] [            
               (
                match discoveredSectors |> Seq.contains position with
                | true ->
                  match optionalSummary with
                  | Some summary ->              
                    div [Class (sprintf "%s %s" cellClass (if summary.IsPlayerInSector then "playerSector" else ""))]
                      [
                        intLabel ((if summary.EnemyCount > 0 then "danger" else "safe"),summary.EnemyCount)
                        intLabel (if summary.IsStarbaseInSector then ("noStarbase",0) else ("starbase", 1))
                        intLabel ("star",summary.StarCount)
                      ]              
                  | None ->
                    div [Class cellClass]
                      [
                        intLabel ("safe",0)
                        intLabel ("noStarbase",0)
                        intLabel ("star", 0)
                      ]
                | false ->
                  div [Class cellClass]
                    [
                      textLabel ("undiscovered","?")
                      textLabel ("undiscovered","?")
                      textLabel ("undiscovered","?")
                    ]
              )
            ]            
          )        
        )
      ]
      div [Class "scannerFooter"] [
        label "Speed"
        greenRangeInput player.WarpSpeed (fun newValue ->  float newValue * 1.<warp> |> SetWarpSpeed |> UpdateGameState |> dispatch)
        label "Engines"
        levelIndicator player.WarpDrive
        label "Deflector"
        levelIndicator player.DeflectorDish
      ]    
    ]
    
    div [Class "arrowDownContainer"] [
      div [Class "arrowDown"] []
    ]
  ]
