module Interface.GameScreen.View

open Types
open Fable.React
open Fable.React.Props
open Game.Utils.GameWorld
open Game.Rules.Weapons

let transparentPopoverOverlay onClose =
  div [Class "transparentPopoverOverlay" ; OnClick (fun _ -> onClose ())] []

let root game gameDispatch model dispatch =
  let currentObjects = game |> currentSectorObjects |> Seq.toArray
  div [Class "gameScreen"] [
    match model.ShortRangeScannerMenuItems with | Some _ -> transparentPopoverOverlay (fun () -> HideShortRangeScannerMenu |> dispatch) | None -> fragment [] []
    div [Class "outerContainer"] [
      div [Class "innerContainer"] [
        ShortRangeScanner.view model.IsUiDisabled currentObjects game.Player model.ShortRangeScannerMenuItems (model.CurrentTarget) dispatch gameDispatch
        div [Class "bottomBar"] [
          div [Class "scoreAndStartdate"] [
            button [] [
              svg [SVGAttr.Width "100%" ; SVGAttr.Height "100%" ; SVGAttr.ViewBox "0 0 448 512"] [
                path [SVGAttr.Custom("fill-rule", "evenodd") ; SVGAttr.Fill "#ddd" ; D "M48.048 304h73.798v128c0 26.51 21.49 48 48 48h108.308c26.51 0 48-21.49 48-48V304h73.789c42.638 0 64.151-51.731 33.941-81.941l-175.943-176c-18.745-18.745-49.137-18.746-67.882 0l-175.952 176C-16.042 252.208 5.325 304 48.048 304zM224 80l176 176H278.154v176H169.846V256H48L224 80z"] []
              ]
            ]
          ]
          div [Class "bottomBarButtons"] []
        ]
        div [Class "sideBar"] [
          EnergyManagement.view {| player = game.Player |}
          Weapons.view {| player = game.Player ; gameDispatch = gameDispatch ; gameObjects = currentObjects |}
        ]
        div [Class "fireButtons"] [
          div[] [button [OnClick (fun _ -> FirePhasers |> dispatch) ; Disabled (model.IsUiDisabled || not(game.Player |> canFirePhasers))] [str "Fire Phasers"]]
          div[] [button [Disabled (model.IsUiDisabled || not(game.Player |> canFireTorpedoes))] [str "Fire Torpedoes"]]
        ]
      ]
    ]
  ]    
