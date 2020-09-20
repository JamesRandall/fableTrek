module Router

open Browser
open Fable.React.Props
open Elmish.Navigation
open Elmish.UrlParser

type Page =
  | StartScreenPage
  | GameScreenPage

let private toHash page =
  match page with
  | StartScreenPage -> "#/"
  | GameScreenPage -> "#/game"

let pageParser =
  oneOf [
    map GameScreenPage (s "game")
    map StartScreenPage top
  ]

let href route =
  Href (toHash route)

let modifyUrl route =
    route |> toHash |> Navigation.modifyUrl

let newUrl route =
    route |> toHash |> Navigation.newUrl

let modifyLocation route =
    window.location.href <- toHash route

let currentRoute () =
  parseHash pageParser window.location  