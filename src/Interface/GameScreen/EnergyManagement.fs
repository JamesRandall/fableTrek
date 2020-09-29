module Interface.GameScreen.EnergyManagement
open Game.Types
open Fable.React
open Fable.React.Props

let inline levelIndicator (rangeValue:RangeValue<'T>) =
  let percentage = rangeValue.Percentage
  let foregroundClass = "levelIndicatorForeground" + (if percentage > 0.5 then "Healthy" elif percentage > 0.25 then "Warning" else "Danger")
  div [Class "levelIndicator"] [
    div [Class "levelIndicatorBackground"] [
      div [Class foregroundClass] []
    ]
  ]

let labelAtRow row text =
  div [Class "label" ; Style [CSSProp.GridRow row]] [str text]

let label text =
  div [Class "label"] [str text]

let floatLabel floatValue =
  div [Class "label"] [str (sprintf "%.0f" floatValue)]

let view = FunctionComponent.Of(fun (props:{| player:Player |}) ->
  div [Class "energyManagement"] [
    label "Main"
    levelIndicator props.player.Energy
    div [Class "shields"] [
      div [Class "labelValuePair"] [label "Fore" ; label props.player.ForeShields.PercentageAsString]
      div [Class "labelValuePair"] [label "Star" ; label props.player.StarboardShields.PercentageAsString]
      div [Class "labelValuePair"] [label "Aft" ; label props.player.AftShields.PercentageAsString]
      div [Class "labelValuePair"] [label "Port" ; label props.player.PortShields.PercentageAsString]
    ]
    div [Style [BackgroundColor "rgba(0,0,0,0.3)" ; Height 250]] []
    label "Generators"
    levelIndicator props.player.ShieldGenerator
  ]
)