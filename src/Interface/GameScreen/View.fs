module Interface.GameScreen.View

open Types
open Fable.React
open Fable.React.Props
open Game.Utils.GameWorld

let root game gameDispatch model dispatch =
  div [Class "gameScreen"] [
    div [Class "outerContainer"] [
      div [Class "innerContainer"] [
        ShortRangeScanner.view {| gameObjects = game |> currentSectorObjects |> Seq.toArray ; player = game.Player |}
        div [Class "bottomBar"] []
        div [Class "sideBar"] [
          EnergyManagement.view {| player = game.Player |}
          Weapons.view {| player = game.Player ; gameDispatch = gameDispatch |}
        ]
        div [Class "fireButtons"] [
          div[] [button [] [str "Fire Phasers"]]
          div[] [button [] [str "Fire Torpedoes"]]
        ]
      ]
    ]
  ]    
