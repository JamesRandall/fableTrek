module Interface.GameScreen.Units.Vector

open Fable.React
open Fable.React.Props
open Game.Types
open Common

module Renderers =
  open Interface.Common.Css
  let private strokeWidth = 3.0
  let private bgOpacity = 0.1

  
  
  let star =
    unitSvg [
      circle [Cx 50 ; Cy 50 ; R 50 ; SVGAttr.Fill (rgba 252 186 3 bgOpacity) ; SVGAttr.StrokeWidth strokeWidth ; SVGAttr.Stroke "rgb(252,186,3)"] []
    ]

  let opaquePlayer shouldFill opacity =
    unitSvg [
      path [
        SVGAttr.StrokeWidth strokeWidth
        SVGAttr.Fill (rgba 0 255 255 (if shouldFill then bgOpacity else 0.0))
        SVGAttr.Stroke (rgba 0 255 255 opacity)
        D "M 50 0 L 25 58 L 0 50 L 25 100 L 50 83 L 75 100 L 100 50 L 75 58 L 50 0"] []
    ]

  let player shouldFill = opaquePlayer shouldFill 1.0
  
  let enemyScout shouldFill =    
    unitSvg [
      path [
        SVGAttr.StrokeWidth strokeWidth
        SVGAttr.Fill (rgba 255 0 0 (if shouldFill then bgOpacity else 0.0))
        SVGAttr.Stroke (rgb 255 0 0)
        D "M 50 0 L 10 100 L 50 70 L 90 100 L 50 0"] []
    ]
    //Pixelated.Scout.pixelatedScout ({| dispatch = (fun _ -> ()) |})

  let enemyCruiser shouldFill =
    unitSvg [
      path [
        SVGAttr.StrokeWidth strokeWidth
        SVGAttr.Fill (rgba 255 0 0 (if shouldFill then bgOpacity else 0.0))
        SVGAttr.Stroke (rgb 255 0 0)
        D "M 50 0 L 0 90 L 40 100 L 60 100 L 100 90 L 50 0"] []
    ]
    
  let enemyDreadnought shouldFill =
    unitSvg [
      rect [
        SVGAttr.X 0
        SVGAttr.Y 0
        SVGAttr.Width 100
        SVGAttr.Height 100
        SVGAttr.StrokeWidth strokeWidth
        SVGAttr.Fill (rgba 255 0 0 (if shouldFill then bgOpacity else 0.0))
        SVGAttr.Stroke (rgb 255 0 0)
      ] []
      rect [
        SVGAttr.X 10
        SVGAttr.Y 10
        SVGAttr.Width 30
        SVGAttr.Height 30
        SVGAttr.StrokeWidth strokeWidth
        SVGAttr.Fill (rgba 0 0 0 0.5)
        SVGAttr.Stroke (rgb 255 128 0)
      ] []
      rect [
        SVGAttr.X 50
        SVGAttr.Y 50
        SVGAttr.Width 40
        SVGAttr.Height 40
        SVGAttr.StrokeWidth strokeWidth
        SVGAttr.Fill (rgba 0 0 0 0.5)
        SVGAttr.Stroke (rgb 255 128 0)
      ] []
    ]

open Renderers

let renderGameObject go =
  match go.Attributes with
  | StarAttributes ->
    star
  | EnemyAttributes enemy ->
    match enemy.ShipClass with
    | Scout _ -> enemyScout true
    | Cruiser _ -> enemyCruiser true
    | Dreadnought _ -> enemyDreadnought true
  | _ -> fragment [] []

let renderUnitStrip =
  div [Class "unitStrip"] [
    div [] [enemyScout false]
    div [] [enemyCruiser false]
    div [] [enemyDreadnought false]
    div [] [player false]
  ]

let renderPlayer () =
  player true