module Interface.GameScreen.Units

open Fable.React
open Fable.React.Props

open Game.Types

module Renderers =

  let private strokeWidth = 3.0
  let private bgOpacity = 0.1

  let private unitSvg inner =
    svg [Style [Width "100%" ; Height "100%"] ; ViewBox "0 0 100 100" ; SVGAttr.PreserveAspectRatio "xMinYMid keep"] [
      g [SVGAttr.Transform "translate(5,5) scale(0.9,0.9)"] inner
    ]
  
  let star =
    unitSvg [
      circle [Cx 50 ; Cy 50 ; R 50 ; SVGAttr.Fill (sprintf "rgba(252,186,3,%f)" bgOpacity) ; SVGAttr.StrokeWidth strokeWidth ; SVGAttr.Stroke "rgb(252,186,3)"] []
    ]

  let opaquePlayer opacity =
    unitSvg [
      path [
        SVGAttr.StrokeWidth strokeWidth
        SVGAttr.Fill (sprintf "rgba(0,255,255,%f)" bgOpacity)
        SVGAttr.Stroke (sprintf "rgba(0,255,255,%f)" opacity)
        D "M 50 0 L 25 58 L 0 50 L 25 100 L 50 83 L 75 100 L 100 50 L 75 58 L 50 0"] []
    ]

  let player = opaquePlayer 1.0

  let enemyScout =
    unitSvg [
      path [
        SVGAttr.StrokeWidth strokeWidth
        SVGAttr.Fill (sprintf "rgba(255,0,0,%f)" bgOpacity)
        SVGAttr.Stroke "rgb(255,0,0)"
        D "M 50 0 L 10 100 L 50 70 L 90 100 L 50 0"] []
    ]

  let enemyCruiser =
    unitSvg [
      path [
        SVGAttr.StrokeWidth strokeWidth
        SVGAttr.Fill (sprintf "rgba(255,0,0,%f)" bgOpacity)
        SVGAttr.Stroke "rgb(255,0,0)"
        D "M 50 0 L 0 90 L 40 100 L 60 100 L 100 90 L 50 0"] []
    ]
    
  let enemyDreadnought =
    unitSvg [
      rect [
        SVGAttr.X 0
        SVGAttr.Y 0
        SVGAttr.Width 100
        SVGAttr.Height 100
        SVGAttr.StrokeWidth strokeWidth
        SVGAttr.Fill (sprintf "rgba(255,0,0,%f)" bgOpacity)
        SVGAttr.Stroke "rgb(255,0,0)"
      ] []
      rect [
        SVGAttr.X 10
        SVGAttr.Y 10
        SVGAttr.Width 30
        SVGAttr.Height 30
        SVGAttr.StrokeWidth strokeWidth
        SVGAttr.Fill "rgba(0,0,0,0.5)"
        SVGAttr.Stroke "rgb(255,128,0)"
      ] []
      rect [
        SVGAttr.X 50
        SVGAttr.Y 50
        SVGAttr.Width 40
        SVGAttr.Height 40
        SVGAttr.StrokeWidth strokeWidth
        SVGAttr.Fill "rgba(0,0,0,0.5)"
        SVGAttr.Stroke "rgb(255,128,0)"
      ] []
    ]

open Renderers

let renderGameObject go =
  match go.Attributes with
  | StarAttributes ->
    star
  | EnemyAttributes enemy ->
    match enemy.ShipClass with
    | Scout _ -> enemyScout
    | Cruiser _ -> enemyCruiser
    | Dreadnought _ -> enemyDreadnought
  | _ -> fragment [] []

let renderPlayer () =
  player