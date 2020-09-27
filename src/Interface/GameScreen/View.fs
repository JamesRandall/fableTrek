module Interface.GameScreen.View

open Types
open Fable.React
open Fable.React.Props
open Game.Utils.GameWorld

let root game model dispatch =
  div [Class "gameScreen"] [
    div [Class "outerContainer"] [
      div [Class "innerContainer"] [
        ShortRangeScanner.view {| gameObjects = game |> currentSectorObjects |> Seq.toArray |}
        div [Class "bottomBar"] []
        div [Class "sideBar"] []
      ]
    ]
  ]    
