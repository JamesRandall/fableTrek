module Interface.GameScreen.Units.Pixelated.Scout

open Interface.GameScreen.Units.Common
open Fable.React
open Fable.React.Props
open Fable.Core.JsInterop
open Browser.Types
open Interface.GameScreen.Types


let pixelatedScout = FunctionComponent.Of (fun (props:{| dispatch:GameScreenMsg->unit |}) ->
  let width = 92
  let height = 90

  Hooks.useEffect((fun () ->
    let explodingScouts = Browser.Dom.document.querySelectorAll(".explodingScout")
    let random = System.Random()
    let randomX () = sprintf "%dpx" (random.Next(width*2)-width)
    let randomY () = sprintf "%dpx" (random.Next(height*2)-height)
    {0..(explodingScouts.length-1)}
    |> Seq.iter (fun scoutIndex ->
      let scout = explodingScouts.Item scoutIndex
      {0..(scout.childNodes.length-1)}
      |> Seq.iter (fun pixelIndex ->
        let pixel = scout.childNodes.Item pixelIndex
        pixel?style?opacity <- 0.0
        pixel?style?transform <- (sprintf "translate3d(%s,%s,0)" (randomX()) (randomY()))
      )
    )
  ), [||])

  (*Fable.Core.JS.setTimeout (fun _ ->    
    let explodingScouts = Browser.Dom.document.querySelectorAll(".explodingScout")
    let random = System.Random()
    let randomX () = sprintf "%dpx" (random.Next(width*2)-width)
    let randomY () = sprintf "%dpx" (random.Next(height*2)-height)
    {0..(explodingScouts.length-1)}
    |> Seq.iter (fun scoutIndex ->
      let scout = explodingScouts.Item scoutIndex
      {0..(scout.childNodes.length-1)}
      |> Seq.iter (fun pixelIndex ->
        let pixel = scout.childNodes.Item pixelIndex
        pixel?style?opacity <- 0.0
        pixel?style?transform <- (sprintf "translate3d(%s,%s,0)" (randomX()) (randomY()))
      )
    )
  ) 2000 |> ignore*)

(*
      let sc = explodingScouts.Item scoutIndex
      {0..(sc.childNodes.length-1)}
      |> Seq.iter (fun i ->
        let item = sc.childNodes.Item i
        item?style?transform <- "translateX(30px)"
        // Fable.Core.JS.console.log("translating")
      )
      //sc?style?transform = "translate3d(30px,10px,0)" |> ignore
      //sc?style?transform = "translate3d(30px,10px,0)" |> ignore
    ) 2000 |> ignore
  )    
*)

  Fable.Core.JS.console.log("rendering pixelated scout")
  
  pixelatedUnitSvg width height  [|
    g [Class "explodingScout"] [|
      rect [SVGAttr.X "46" ; SVGAttr.Y "0" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "48" ; SVGAttr.Y "0" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "44" ; SVGAttr.Y "2" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "46" ; SVGAttr.Y "2" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "48" ; SVGAttr.Y "2" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "42" ; SVGAttr.Y "4" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "44" ; SVGAttr.Y "4" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "46" ; SVGAttr.Y "4" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "48" ; SVGAttr.Y "4" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "50" ; SVGAttr.Y "4" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "42" ; SVGAttr.Y "6" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "48" ; SVGAttr.Y "6" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "50" ; SVGAttr.Y "6" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "42" ; SVGAttr.Y "8" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "48" ; SVGAttr.Y "8" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "50" ; SVGAttr.Y "8" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "40" ; SVGAttr.Y "10" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "42" ; SVGAttr.Y "10" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "48" ; SVGAttr.Y "10" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "50" ; SVGAttr.Y "10" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "40" ; SVGAttr.Y "12" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "50" ; SVGAttr.Y "12" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "38" ; SVGAttr.Y "14" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "40" ; SVGAttr.Y "14" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "50" ; SVGAttr.Y "14" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "52" ; SVGAttr.Y "14" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "38" ; SVGAttr.Y "16" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "52" ; SVGAttr.Y "16" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "38" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "52" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "36" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "38" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "52" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "54" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "36" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "54" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "34" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "36" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "54" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "56" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "34" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "56" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "34" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "56" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "32" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "34" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "56" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "58" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "32" ; SVGAttr.Y "32" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "58" ; SVGAttr.Y "32" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "30" ; SVGAttr.Y "34" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "32" ; SVGAttr.Y "34" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "58" ; SVGAttr.Y "34" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "60" ; SVGAttr.Y "34" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "30" ; SVGAttr.Y "36" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "60" ; SVGAttr.Y "36" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "30" ; SVGAttr.Y "38" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "60" ; SVGAttr.Y "38" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "28" ; SVGAttr.Y "40" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "30" ; SVGAttr.Y "40" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "60" ; SVGAttr.Y "40" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "62" ; SVGAttr.Y "40" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "28" ; SVGAttr.Y "42" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "62" ; SVGAttr.Y "42" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "26" ; SVGAttr.Y "44" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "28" ; SVGAttr.Y "44" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "62" ; SVGAttr.Y "44" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "64" ; SVGAttr.Y "44" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "26" ; SVGAttr.Y "46" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "28" ; SVGAttr.Y "46" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "64" ; SVGAttr.Y "46" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "26" ; SVGAttr.Y "48" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "28" ; SVGAttr.Y "48" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "64" ; SVGAttr.Y "48" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "24" ; SVGAttr.Y "50" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "26" ; SVGAttr.Y "50" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "28" ; SVGAttr.Y "50" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "64" ; SVGAttr.Y "50" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "66" ; SVGAttr.Y "50" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "24" ; SVGAttr.Y "52" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "26" ; SVGAttr.Y "52" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "66" ; SVGAttr.Y "52" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "22" ; SVGAttr.Y "54" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "24" ; SVGAttr.Y "54" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "26" ; SVGAttr.Y "54" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "66" ; SVGAttr.Y "54" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "68" ; SVGAttr.Y "54" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "70" ; SVGAttr.Y "54" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "22" ; SVGAttr.Y "56" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "68" ; SVGAttr.Y "56" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "70" ; SVGAttr.Y "56" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "22" ; SVGAttr.Y "58" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "68" ; SVGAttr.Y "58" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "70" ; SVGAttr.Y "58" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "20" ; SVGAttr.Y "60" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "22" ; SVGAttr.Y "60" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "44" ; SVGAttr.Y "60" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "46" ; SVGAttr.Y "60" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "48" ; SVGAttr.Y "60" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "68" ; SVGAttr.Y "60" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "70" ; SVGAttr.Y "60" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "72" ; SVGAttr.Y "60" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "20" ; SVGAttr.Y "62" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "42" ; SVGAttr.Y "62" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "44" ; SVGAttr.Y "62" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "46" ; SVGAttr.Y "62" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "48" ; SVGAttr.Y "62" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "50" ; SVGAttr.Y "62" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "70" ; SVGAttr.Y "62" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "72" ; SVGAttr.Y "62" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "18" ; SVGAttr.Y "64" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "20" ; SVGAttr.Y "64" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "38" ; SVGAttr.Y "64" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "40" ; SVGAttr.Y "64" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "42" ; SVGAttr.Y "64" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "48" ; SVGAttr.Y "64" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "50" ; SVGAttr.Y "64" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "52" ; SVGAttr.Y "64" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "70" ; SVGAttr.Y "64" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "72" ; SVGAttr.Y "64" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "74" ; SVGAttr.Y "64" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "18" ; SVGAttr.Y "66" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "36" ; SVGAttr.Y "66" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "38" ; SVGAttr.Y "66" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "52" ; SVGAttr.Y "66" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "54" ; SVGAttr.Y "66" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "72" ; SVGAttr.Y "66" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "74" ; SVGAttr.Y "66" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "18" ; SVGAttr.Y "68" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "34" ; SVGAttr.Y "68" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "36" ; SVGAttr.Y "68" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "54" ; SVGAttr.Y "68" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "56" ; SVGAttr.Y "68" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "72" ; SVGAttr.Y "68" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "74" ; SVGAttr.Y "68" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "16" ; SVGAttr.Y "70" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "18" ; SVGAttr.Y "70" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "30" ; SVGAttr.Y "70" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "32" ; SVGAttr.Y "70" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "34" ; SVGAttr.Y "70" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "56" ; SVGAttr.Y "70" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "58" ; SVGAttr.Y "70" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "60" ; SVGAttr.Y "70" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "72" ; SVGAttr.Y "70" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "74" ; SVGAttr.Y "70" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "16" ; SVGAttr.Y "72" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "28" ; SVGAttr.Y "72" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "30" ; SVGAttr.Y "72" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "60" ; SVGAttr.Y "72" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "62" ; SVGAttr.Y "72" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "74" ; SVGAttr.Y "72" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "14" ; SVGAttr.Y "74" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "16" ; SVGAttr.Y "74" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "26" ; SVGAttr.Y "74" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "28" ; SVGAttr.Y "74" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "62" ; SVGAttr.Y "74" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "64" ; SVGAttr.Y "74" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "74" ; SVGAttr.Y "74" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "76" ; SVGAttr.Y "74" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "14" ; SVGAttr.Y "76" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "22" ; SVGAttr.Y "76" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "24" ; SVGAttr.Y "76" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "26" ; SVGAttr.Y "76" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "28" ; SVGAttr.Y "76" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "64" ; SVGAttr.Y "76" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "66" ; SVGAttr.Y "76" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "68" ; SVGAttr.Y "76" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "70" ; SVGAttr.Y "76" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "76" ; SVGAttr.Y "76" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "14" ; SVGAttr.Y "78" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "20" ; SVGAttr.Y "78" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "22" ; SVGAttr.Y "78" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "68" ; SVGAttr.Y "78" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "70" ; SVGAttr.Y "78" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "72" ; SVGAttr.Y "78" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "76" ; SVGAttr.Y "78" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "12" ; SVGAttr.Y "80" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "14" ; SVGAttr.Y "80" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "18" ; SVGAttr.Y "80" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "20" ; SVGAttr.Y "80" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "70" ; SVGAttr.Y "80" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "72" ; SVGAttr.Y "80" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "74" ; SVGAttr.Y "80" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(1,0,0,255)"][]
      rect [SVGAttr.X "76" ; SVGAttr.Y "80" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "78" ; SVGAttr.Y "80" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "12" ; SVGAttr.Y "82" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "14" ; SVGAttr.Y "82" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "16" ; SVGAttr.Y "82" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "18" ; SVGAttr.Y "82" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "72" ; SVGAttr.Y "82" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(254,0,0,255)"][]
      rect [SVGAttr.X "74" ; SVGAttr.Y "82" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "76" ; SVGAttr.Y "82" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "78" ; SVGAttr.Y "82" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "10" ; SVGAttr.Y "84" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "12" ; SVGAttr.Y "84" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "14" ; SVGAttr.Y "84" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "76" ; SVGAttr.Y "84" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "78" ; SVGAttr.Y "84" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "80" ; SVGAttr.Y "84" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "10" ; SVGAttr.Y "86" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "12" ; SVGAttr.Y "86" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "78" ; SVGAttr.Y "86" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "80" ; SVGAttr.Y "86" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "10" ; SVGAttr.Y "88" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
      rect [SVGAttr.X "80" ; SVGAttr.Y "88" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(255,0,0,255)"][]
    |]
  |]
)