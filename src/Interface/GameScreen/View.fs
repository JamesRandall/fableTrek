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
        ShortRangeScanner.view model.IsUiDisabled currentObjects game.Player model.ShortRangeScannerMenuItems (model.FiringTargets |> Seq.tryHead) dispatch gameDispatch
        div [Class "bottomBar"] []
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
