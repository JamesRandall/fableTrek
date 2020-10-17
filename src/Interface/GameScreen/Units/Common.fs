module Interface.GameScreen.Units.Common

open Fable.React
open Fable.React.Props

let unitSvg inner =
  svg [Style [Width "100%" ; Height "100%"] ; ViewBox "0 0 100 100" ; SVGAttr.PreserveAspectRatio "xMinYMid keep"] [
    g [SVGAttr.Transform "translate(5,5) scale(0.9,0.9)" ; SVGAttr.Custom("shape-rendering", "crispEdges")] inner
  ]

let pixelatedUnitSvg width height inner =
  svg [Style [Width "100%" ; Height "100%"] ; ViewBox (sprintf "0 0 %d %d" width height) ; SVGAttr.PreserveAspectRatio "xMinYMid keep"] [
    g [SVGAttr.Transform "translate(5,5) scale(0.9,0.9)" ; SVGAttr.Custom("shape-rendering", "crispEdges")] inner
  ]