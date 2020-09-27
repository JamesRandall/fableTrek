module Interface.StartScreen.View
open Types
open Fable.React
open Fable.React.Props

let root model dispatch =
  let startOnClick difficulty =
    OnClick (fun _ -> difficulty |> StartNewGame |> dispatch )
  div [Class "startScreen"] [
    div [Class "title"] [str "FableTrek"]
    div [Class "buttonContainer"] [
      div [Class "buttons"] [
        button [Disabled true] [str "Resume Game"]
        button [Game.Types.EasyDifficulty |> startOnClick] [str "Easy Game"]
        button [Game.Types.MediumDifficulty |> startOnClick] [str "Medium Game"]
        button [Game.Types.HardDifficulty |> startOnClick] [str "Hard Game"]
      ]
    ]
  ]    
