module Interface.GameScreen.View

open Types
open Fable.React
open Fable.React.Props
open Game.Utils.GameWorld

let root game model dispatch =
  div [Class "gameScreen"] [
    div [Class "outerContainer"] [
      div [Class "innerContainer"] [
        ShortRangeScanner.view {| gameObjects = game |> currentSectorObjects |> Seq.toArray ; player = game.Player |}
        EnergyManagement.view {| player = game.Player |}
        div [Class "bottomBar"] []
        div [Class "sideBar"] [
          div [Class "energy"] []
          div [Class "weapons"] []
        ]
        div [Class "fireButtons"] [
          div[] [button [] [str "Fire Phasers"]]
          div[] [button [] [str "Fire Torpedoes"]]
        ]
      ]
    ]
  ]    
