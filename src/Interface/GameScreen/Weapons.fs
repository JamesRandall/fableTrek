module Interface.GameScreen.Weapons
open Game.Types
open Fable.React
open Fable.React.Props
open Interface.Common

let torpedoesView (torpedoes:RangeValue<int<torpedo>>) =
  div [Class "torpedoes"] (
    {1..(torpedoes.Max |> int)}
    |> Seq.map (fun t ->
      div [Class ("torpedo " + (if t <= (torpedoes.Current |> int) then "torpedoActive" else "torpedoInactive"))] []
    )
  )

let targets (targets:GameWorldPosition list) gameObjects gameDispatch =
  let acquiredTargets = targets |> Seq.map (fun target ->
    div [Class "target"] []
  )
  let blankTargets = {(targets.Length)..2} |> Seq.map (fun target ->
    div [Class "target"] []
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
      rangeInput props.player.PhaserPower (fun newValue ->  float newValue * 1.<gigawatt> |> SetPhaserPower |> UpdatePlayerState |> props.gameDispatch)
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

