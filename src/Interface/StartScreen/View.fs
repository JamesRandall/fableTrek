module Interface.StartScreen.View
open Types
open Fable.React
open Fable.React.Props

let inline startScreen children = domEl "startScreen" [] children

let root model dispatch =
  div [Class "startScreen"] [
    div [Class "title"] [str "< work in progress >"]
  ]    
