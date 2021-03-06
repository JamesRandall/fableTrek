module Interface.GameScreen.EnergyManagement
open Game.Types
open Fable.React
open Fable.React.Props
open Interface.Common
open Interface.Common.Css
open Interface.Browser.Helpers
open Types

let inline shieldColor raised arcNumber (shieldLevel:RangeValue<'t>) =
  let opacity = if raised then 1.0 else 0.4
  let p = shieldLevel.Percentage
  let shieldArc = if p <= 0.25 then 0 elif p <= 0.5 then 1 elif p <=0.75 then 2 else 3
  if arcNumber > shieldArc then
    rgba 255 0 0 0.2
  else
    let basedP = (p - (arcNumber|>float)*0.25) / (1. / 3.)
    opacity |> (if basedP <= 0.33 then GameColors.danger elif basedP <= 0.66 then GameColors.warning else GameColors.healthy)


let shields player =
  let shieldArcWidth = 6.
  let foreStart = 317.
  let foreEnd = 43.
  let starEnd = 313.
  let starStart = 227.
  let portStart = 47.
  let portEnd = 133.
  let aftStart = 137.
  let aftEnd = 223.
  
  let radii = [125. - shieldArcWidth ; 125. - shieldArcWidth*3. ; 125. - shieldArcWidth*5. ; 125. - shieldArcWidth*7.]

  let shieldColor = shieldColor player.ShieldsRaised

  svg [Style [Width "100%" ; Height "100%"] ; ViewBox "0 0 250 250" ; SVGAttr.PreserveAspectRatio "xMinYMid keep"] (
    radii |> Seq.mapi(fun i r ->
      arc 125. 125. r foreStart foreEnd [SVGAttr.Stroke (shieldColor (3-i) player.ForeShields) ; SVGAttr.StrokeWidth shieldArcWidth ; SVGAttr.Fill "none"]
    ) |> Seq.append (radii |> Seq.mapi(fun i r ->
      arc 125. 125. r starStart starEnd [SVGAttr.Stroke (shieldColor (3-i) player.StarboardShields) ; SVGAttr.StrokeWidth shieldArcWidth ; SVGAttr.Fill "none"]
    )) |> Seq.append (radii |> Seq.mapi(fun i r ->
      arc 125. 125. r portStart portEnd [SVGAttr.Stroke (shieldColor (3-i) player.PortShields) ; SVGAttr.StrokeWidth shieldArcWidth ; SVGAttr.Fill "none"]
    )) |> Seq.append (radii |> Seq.mapi(fun i r ->
      arc 125. 125. r aftStart aftEnd [SVGAttr.Stroke (shieldColor (3-i) player.AftShields) ; SVGAttr.StrokeWidth shieldArcWidth ; SVGAttr.Fill "none"]
    ))
  )

let view = FunctionComponent.Of(fun (props:{| player:Player ; dispatch: GameScreenMsg -> unit |}) ->
  let shieldContainerSize = Hooks.useState (0,0)
  let shieldContainerRef = Hooks.useRef None

  debouncedSize shieldContainerRef shieldContainerSize.update

  
  div [Class "energyManagement"] [
    div [Class "inner"] [
      div [Class "innerTop"] [
        label "Main"
        levelIndicator props.player.Energy
      ]
      div [Class "innerMiddle"] [
        div [Class "shieldsProperties"] [
          div [Class "labelValuePair"] [label "Fore" ; label props.player.ForeShields.PercentageAsString]
          div [Class "labelValuePair"] [label "Star" ; label props.player.StarboardShields.PercentageAsString]
          div [Class "labelValuePair"] [label "Aft" ; label props.player.AftShields.PercentageAsString]
          div [Class "labelValuePair"] [label "Port" ; label props.player.PortShields.PercentageAsString]
        ]
        div [Class "shieldsContainer" ; RefHook shieldContainerRef ; Style [Opacity (if props.player.ShieldsRaised then 1.0 else 0.6)] ; OnClick (fun _ -> GameRequestMsg.ToggleShields |> GameRequest |> props.dispatch)] [
          div [Class "shieldPlayerContainer"] [
            div [Class "shieldsPlayer"] [
              Units.Vector.Renderers.opaquePlayer true 1.0
            ]
          ]
          div [Class "shieldsGraphics" ; Style [Width (shieldContainerSize.current |> fst) ; Height (shieldContainerSize.current |> fst)]] [
            shields props.player
          ]
        ]
      ]
      div [Class "innerBottom"] [
        label "Generators"
        levelIndicator props.player.ShieldGenerator
      ]
    ]
  ]
)