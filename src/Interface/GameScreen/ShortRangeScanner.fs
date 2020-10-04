module Interface.GameScreen.ShortRangeScanner

open Fable.React
open Fable.React.Props
open Game.Types
open Game.Utils.Position
open Units
open Types

module Menu = 
  let items player optionalGameObject position =
    match optionalGameObject with
    | Some gameObject ->
      match gameObject.Attributes with
      | EnemyAttributes enemy ->
        match player.Targets |> Seq.tryFind (fun p -> p = position) with
        | Some _ -> [|("Remove target", position |> RemoveTarget) |> MenuItem|]
        | None -> [|("Add target", position |> AddTarget) |> MenuItem|]
      | StarbaseAttributes _ ->
        match player.Position.GalacticPosition = position.GalacticPosition, isAdjacent player.Position.SectorPosition position.SectorPosition, Option.isNone player.DockedWith with
        | true, true, true -> [|("Dock", position |> Dock) |> MenuItem|]
        | true, true, false -> [|("Dock", Undock) |> MenuItem|]
        | _ -> [|"Move next to the starbase to dock" |> NoActionLabel|]
      | StarAttributes  -> [|"A star. A really big star." |> NoActionLabel|] 
    | None -> [|("Move to", position |> MoveTo) |> MenuItem|]

  let view position menuItems =
    div [] []  

let view (gameObjects:GameObject array) (player:Player) menuItems dispatch =
  //let containerSize = Hooks.useState (0,0)
  // let containerRef = Hooks.useRef None

  let gridWidthPercentage = 1. / ((GameWorldPosition.Max.SectorPosition.Y+1<coordinatecomponent>) |> float)
  let gridWidthPercentageAsString = sprintf "%f%%" (gridWidthPercentage * 100.)
  let gridHeightPercentage = 1. / ((GameWorldPosition.Max.SectorPosition.X+1<coordinatecomponent>) |> float)
  let gridHeightPercentageAsString = sprintf "%f%%" (gridHeightPercentage * 100.)
  let numberOfRows = ((GameWorldPosition.Max.SectorPosition.Y+1<coordinatecomponent>) |> int)
  let numberOfColumns = ((GameWorldPosition.Max.SectorPosition.X+1<coordinatecomponent>) |> int)
  let getLeft x = CSSProp.Left (sprintf "%f%%" ((x |> float) / ((GameWorldPosition.Max.SectorPosition.X+1<coordinatecomponent>) |> float) * 100.))
  let getTop y = CSSProp.Top (sprintf "%f%%" ((y |> float) / ((GameWorldPosition.Max.SectorPosition.Y+1<coordinatecomponent>) |> float) * 100.))
  let cssWidth = CSSProp.Width gridWidthPercentageAsString
  let cssHeight = CSSProp.Height gridHeightPercentageAsString

  let renderedSectorObjects =
    gameObjects
    |> Seq.map (fun go ->
      div [Class "gameObject" ; Style [getLeft go.Position.SectorPosition.X ; getTop go.Position.SectorPosition.Y ; cssWidth ; cssHeight ]] [
        div [Style [Height "80%" ; Width "80%"]] [go |> renderGameObject]
      ]
    )
    |> Seq.append [
      div [Class "gameObject" ; Style [getLeft player.Position.SectorPosition.X ; getTop player.Position.SectorPosition.Y ; cssWidth ; cssHeight ]] [
        div [Style [Height "80%" ; Width "80%"]] [renderPlayer ()]
      ]
    ]
  
  let overlayGrid =
    let gridTemplateRows = (Seq.replicate numberOfRows (sprintf "%s " gridHeightPercentageAsString)) |> Seq.toArray |> Array.fold (+) ""
    let gridTemplateColumns = (Seq.replicate numberOfColumns (sprintf "%s " gridWidthPercentageAsString)) |> Seq.toArray |> Array.fold (+) ""
    div [Class "overlayGrid" ; Style [CSSProp.GridTemplateRows gridTemplateRows ; CSSProp.GridTemplateColumns gridTemplateColumns ]] (
      Game.Utils.Position.sectorCoordinateIterator ()
      |> Seq.map(fun clickedPosition ->
        div [
          OnClick (fun _ ->
            let clickedGameWorldPosition = { player.Position with SectorPosition = clickedPosition }
            let objectAtPosition = gameObjects |> Seq.tryFind(fun go -> go.Position = clickedGameWorldPosition)
            let menuItems = Menu.items player objectAtPosition clickedGameWorldPosition
            (clickedGameWorldPosition, menuItems) |> ShowShortRangeScannerMenu |> dispatch)
          Style [
            GridRowStart ((clickedPosition.Y|>int) + 1)
            GridRowEnd ((clickedPosition.Y|>int) + 2)
            GridColumnStart ((clickedPosition.X|>int) + 1)
            GridColumnEnd ((clickedPosition.X|>int) + 2)
          ]
        ] [(*str (sprintf "%d,%d" x y)*)]
      )
    )

  let menu =
    match menuItems with
    | Some items ->
      let left = sprintf "%f%%" ((gridWidthPercentage * (items.Position.SectorPosition.X |> float) + gridWidthPercentage/2.)  * 100.)
      let top = sprintf "%f%%" ((gridHeightPercentage * (items.Position.SectorPosition.Y |> float) + gridHeightPercentage/2.) * 100.)
      div [Class "menu" ; Style [Left left ; Top top]] [str "hello"]
    | None -> fragment [] []


  let verticalLines =
    { 0..(numberOfColumns-2) }
    |> Seq.map(fun g ->
      let leftPercentage = sprintf "%f%%" ((g |> float) / ((GameWorldPosition.Max.SectorPosition.X+1<coordinatecomponent>) |> float) * 100.)
      div [Class "verticalLine" ; Style [CSSProp.Left leftPercentage ; cssWidth]] []
    )
  let horizontalLines =
    { 0..(numberOfRows-2) }
    |> Seq.map(fun g ->
      let topPercentage = sprintf "%f%%" ((g |> float) / ((GameWorldPosition.Max.SectorPosition.Y+1<coordinatecomponent>) |> float) * 100.)
      div [Class "horizontalLine" ; Style [CSSProp.Top topPercentage ; cssHeight]] []
    )    

  div [Class "shortRangeScanner"] (
    [menu]
    |> Seq.append [overlayGrid]
    |> Seq.append renderedSectorObjects 
    |> Seq.append verticalLines
    |> Seq.append horizontalLines    
  )