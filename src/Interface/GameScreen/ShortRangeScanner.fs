module Interface.GameScreen.ShortRangeScanner

open Fable.React
open Fable.React.Props
open Game.Types
open Units


let view = FunctionComponent.Of(fun (props:{| gameObjects:GameObject array ; player:Player |}) ->
  //let containerSize = Hooks.useState (0,0)
  let containerRef = Hooks.useRef None

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
    props.gameObjects
    |> Seq.map (fun go ->
      div [Class "gameObject" ; Style [getLeft go.Position.SectorPosition.X ; getTop go.Position.SectorPosition.Y ; cssWidth ; cssHeight ]] [
        div [Style [Height "80%" ; Width "80%"]] [go |> renderGameObject]
      ]
    )
    |> Seq.append [
      div [Class "gameObject" ; Style [getLeft props.player.Position.SectorPosition.X ; getTop props.player.Position.SectorPosition.Y ; cssWidth ; cssHeight ]] [
        div [Style [Height "80%" ; Width "80%"]] [renderPlayer ()]
      ]
    ]
  
  let overlayGrid =
    let gridTemplateRows = (Seq.replicate numberOfRows (sprintf "%s " gridHeightPercentageAsString)) |> Seq.toArray |> Array.fold (+) ""
    let gridTemplateColumns = (Seq.replicate numberOfColumns (sprintf "%s " gridWidthPercentageAsString)) |> Seq.toArray |> Array.fold (+) ""
    div [Class "overlayGrid" ; Style [CSSProp.GridTemplateRows gridTemplateRows ; CSSProp.GridTemplateColumns gridTemplateColumns ]] (
      Game.Utils.Position.sectorCoordinateIterator ()
      |> Seq.map(fun (x,y) ->
        div [
          OnClick (fun _ -> Fable.Core.JS.console.log(sprintf "%d %d clicked" x y))
          Style [
            GridRowStart ((y|>int) + 1)
            GridRowEnd ((y|>int) + 2)
            GridColumnStart ((x|>int) + 1)
            GridColumnEnd ((x|>int) + 2)
          ]
        ] [(*str (sprintf "%d,%d" x y)*)]
      )
    )

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

  div [Class "shortRangeScanner" ; RefHook containerRef] (
    [overlayGrid]
    |> Seq.append renderedSectorObjects 
    |> Seq.append verticalLines
    |> Seq.append horizontalLines    
  )
)