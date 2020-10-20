module Interface.GameScreen.DamageControl

open Game.Types
open Fable.React
open Fable.React.Props
open Interface.Common

let view game =
  let systems = game.Player.SystemsAsList
  let canRepair = game |> Game.Rules.Damage.canRepair
  div [Class "damageControl"] [
    div [Class "damageControlContainer"] (
      [
        div [Class "damageControlFooter"] [
          if not(canRepair) then
              label (sprintf "Total repair time: %.1f days" (game.Player |> Game.Rules.Damage.totalRepairTime))
              button [Class "plain"] [str "Repair"]
          else
            label "All systems fully operational"
        ]
      ]
      |> Seq.append (
        systems
        |> Seq.collect(fun (text, hp) -> 
          [ label text ; levelIndicator hp ]
        )
      )
    )

    div [Class "arrowDownContainer"] [
      div [Class "arrowDown"] []
    ]
  ]
