module Interface.GameScreen.EnergyManagement
open Game.Types
open Fable.React
open Fable.React.Props

let view = FunctionComponent.Of(fun (props:{| player:Player |}) ->
  div [Class "energyManagement"] []
)