module Interface.GameScreen.Weapons
open Game.Types
open Fable.React
open Fable.React.Props
open Interface.Common
open Interface.Common.Css

let noTargetAcquired =
  [
    svg [Style [Width "100%" ; Height "121px"] ; ViewBox "0 0 100 100" ; SVGAttr.PreserveAspectRatio "xMinYMid keep"] [
      g [SVGAttr.Stroke (rgb 0 128 0) ; SVGAttr.StrokeWidth 2] [
        path [D "M 40 1 L 60 1 M 50 1 L 50 15"] []
        path [D "M 1 40 L 1 60 M 1 50 L 15 50"] []
        path [D "M 40 99 L 60 99 M 50 99 L 50 85"] []
        path [D "M 99 40 L 99 60 M 99 50 L 85 50"] []
      ]
    ]
    div [Class "noTargetAcquiredLabel"] [str "No Target"]
  ]

let torpedoesView (torpedoes:RangeValue<int<torpedo>>) =
  div [Class "torpedoes"] (
    {1..(torpedoes.Max |> int)}
    |> Seq.map (fun t ->
      div [Class ("torpedo " + (if t <= (torpedoes.Current |> int) then "torpedoActive" else "torpedoInactive"))] []
    )
  )

let targets (targets:GameWorldPosition list) gameObjects gameDispatch =
  let acquiredTargets = targets |> Seq.map (fun targetPosition ->
    let optionalTarget = targetPosition |> Game.Utils.GameWorld.objectAtPosition gameObjects
    match optionalTarget with
    | Some target ->
      match target.Attributes with
      | EnemyAttributes enemy ->
        div [Class "target" ; OnClick (fun _ -> target.Position |> RemoveTarget |> UpdateGameState |> gameDispatch )] [
          div [Class "graphicAndCoords"] [
            label target.Position.SectorPosition.AsString
            div [Class "graphicContainer"] [div [Class "graphic"] [Units.renderGameObject target]]
          ]          
          div [Class "summary"] [
            label "E"
            levelIndicator enemy.Energy
            label "S"
            levelIndicator enemy.Shields
            label "H"
            levelIndicator enemy.HitPoints
          ]
        ]
      | _ -> div [Class "target"] noTargetAcquired
    | None ->
      div [Class "target"] noTargetAcquired
  )
  let blankTargets = {(targets.Length)..2} |> Seq.map (fun _ ->
    div [Class "target"] noTargetAcquired
  )
  div [Class "targets"] (blankTargets |> Seq.append acquiredTargets)
  
  (*[
    div [Class "target"] []
    div [Class "target"] []
    div [Class "target"] []
  ]*)

let view = FunctionComponent.Of(fun (props:{| player:Player ; gameDispatch:(GameMsg -> unit) ; gameObjects:GameObject array |}) ->
  div [Class "weapons"] [
    div [Class "inner"] [
      label "Phaser Power"
      rangeInput props.player.PhaserPower (fun newValue ->  float newValue * 1.<gigawatt> |> SetPhaserPower |> UpdateGameState |> props.gameDispatch)
      label "Phaser Temp"
      invertedLevelIndicator props.player.PhaserTemperature
      label "Phasers"
      levelIndicator props.player.Phasers
      label "Torpedoes"
      torpedoesView props.player.Torpedos
      label "Launcher"
      levelIndicator props.player.TorpedoLaunchers
    ]
    targets props.player.Targets props.gameObjects props.gameDispatch
  ]
)

