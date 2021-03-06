module Interface.GameScreen.View

open Types
open Fable.React
open Fable.React.Props
open Game.Utils.GameWorld
open Game.Rules.Weapons
open Game.Types
open Types
open Interface.Browser.Helpers


let transparentPopoverOverlay onClose =
  div [Class "transparentPopoverOverlay" ; OnClick (fun _ -> onClose ())] []

let root = FunctionComponent.Of(fun (props:{| Game:Game ; GameDispatch:GameMsg->unit ; Model:Model ; Dispatch:GameScreenMsg->unit |}) ->
  let game = props.Game
  let gameDispatch = props.GameDispatch
  let dispatch = props.Dispatch
  let model = props.Model

  let size = Hooks.useState(-1,-1)
  let starfieldContainerRef = Hooks.useRef None
  debouncedSize starfieldContainerRef size.update
  
  let currentObjects = game |> currentSectorObjects |> Seq.toArray
  div [Class "gameScreen" ; RefHook starfieldContainerRef] [
    if model.IsWarping then
      div [Class "starfield" ; OnClick (fun _ -> EndWarpTo |> dispatch) ] [
        if (size.current |> fst) > -1 then StarField.view ({| Width = size.current |> fst ; Height = size.current |> snd |}) else fragment [] []
      ]
    else
      fragment [] []
    // Units.Vector.renderUnitStrip
    match model.ShortRangeScannerMenuItems with | Some _ -> transparentPopoverOverlay (fun () -> HideShortRangeScannerMenu |> dispatch) | None -> fragment [] []
    if model.IsLongRangeScannerVisible then transparentPopoverOverlay (fun () -> HideLongRangeScanner |> dispatch) else fragment [] []
    if model.IsDamageControlVisible then transparentPopoverOverlay (fun () -> HideDamageControl |> dispatch) else fragment [] []
    //if model.IsCaptainsLogVisible then transparentPopoverOverlay (fun () -> HideCaptainsLog |> dispatch) else fragment [] []
    div [Class "outerContainer"] [
      if model.IsEnemyTurn then div [Class "message"] [Interface.Common.label "Enemy turn in progress"] else fragment [] []
      div [Class "innerContainer"] [
        ShortRangeScanner.view model.IsUiDisabled model.Explosions currentObjects game.Player model.ShortRangeScannerMenuItems (model.CurrentTarget) dispatch gameDispatch
        div [Class "bottomBar"] [
          div [Class "scoreAndStartdate"] [
            button [] [
              svg [SVGAttr.Width "100%" ; SVGAttr.Height "100%" ; SVGAttr.ViewBox "0 0 448 512"] [
                path [SVGAttr.Custom("fill-rule", "evenodd") ; SVGAttr.Fill "#ddd" ; D "M48.048 304h73.798v128c0 26.51 21.49 48 48 48h108.308c26.51 0 48-21.49 48-48V304h73.789c42.638 0 64.151-51.731 33.941-81.941l-175.943-176c-18.745-18.745-49.137-18.746-67.882 0l-175.952 176C-16.042 252.208 5.325 304 48.048 304zM224 80l176 176H278.154v176H169.846V256H48L224 80z"] []
              ]
            ]
            div [] [
              Interface.Common.label (sprintf "Stardate %.1f" game.Stardate)
              Interface.Common.label (sprintf "Score %d" game.Score)
            ]
          ]
          div [Class "bottomBarButtons"] [
            div[Class "buttonContainer"] [
              if model.IsLongRangeScannerVisible then
                LongRangeScanner.view
                  {|
                    WarpDestinationOption = model.WarpDestination
                    DiscoveredSectors = game.DiscoveredSectors
                    GameObjects = game.GameObjects
                    Player = game.Player
                    IsWarping = model.IsWarping
                    Dispatch = dispatch
                    GameDispatch = gameDispatch
                  |}
              else fragment [] []
              button [Class "plain" ; Disabled model.IsUiDisabled ; OnClick (fun _ -> ShowLongRangeScanner |> dispatch)] [str "Long Range"]
            ]
            div[Class "buttonContainer"] [
              if model.IsDamageControlVisible then DamageControl.view game else fragment [] []
              button [Class "plain" ; Disabled model.IsUiDisabled ; OnClick (fun _ -> ShowDamageControl |> dispatch)] [str "Damage Control"]
            ]
            div[Class "buttonContainer"] [button [Class "plain" ; Disabled model.IsUiDisabled] [str "Log"]]
          ]
        ]
        div [Class "sideBar"] [
          EnergyManagement.view {| player = game.Player ; dispatch = dispatch |}
          Weapons.view {| player = game.Player ; gameDispatch = gameDispatch ; gameObjects = currentObjects |}
        ]
        div [Class "fireButtons"] [
          div[] [button [OnClick (fun _ -> FirePhasers |> dispatch) ; Disabled (model.IsUiDisabled || not(game.Player |> canFirePhasers))] [str "Fire Phasers"]]
          div[] [button [Disabled (model.IsUiDisabled || not(game.Player |> canFireTorpedoes))] [str "Fire Torpedoes"]]
        ]
      ]
    ]
  ]    
)
