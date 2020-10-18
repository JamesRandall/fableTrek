module Interface.GameScreen.Units.Pixelated.Player

open Interface.GameScreen.Units.Common
open Fable.React
open Fable.React.Props

let pixelatedOpaquePlayer =
  pixelatedUnitSvg 38 38 [|
    rect [SVGAttr.X "18" ; SVGAttr.Y "0" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "2" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "2" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "4" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "4" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "4" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "4" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "6" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "6" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "6" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "6" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "8" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "8" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "8" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "8" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "12" ; SVGAttr.Y "10" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "10" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "10" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "10" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "10" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "22" ; SVGAttr.Y "10" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "12" ; SVGAttr.Y "12" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "12" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "12" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "12" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "12" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "22" ; SVGAttr.Y "12" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "10" ; SVGAttr.Y "14" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "12" ; SVGAttr.Y "14" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "14" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "14" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "14" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "14" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "22" ; SVGAttr.Y "14" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "24" ; SVGAttr.Y "14" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "10" ; SVGAttr.Y "16" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "12" ; SVGAttr.Y "16" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "16" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "16" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "16" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "16" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "22" ; SVGAttr.Y "16" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "24" ; SVGAttr.Y "16" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "0" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(125,205,207,255)"][]
    rect [SVGAttr.X "2" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "4" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "8" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "10" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "12" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "22" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "24" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "26" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "30" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "32" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "34" ; SVGAttr.Y "18" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(125,205,207,255)"][]
    rect [SVGAttr.X "0" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "2" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "4" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "6" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "8" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "10" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "12" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "22" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "24" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "26" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "28" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "30" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "32" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "34" ; SVGAttr.Y "20" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "2" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "4" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "6" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "8" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "10" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "12" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "22" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "24" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "26" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "28" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "30" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "32" ; SVGAttr.Y "22" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "2" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "4" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "6" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "8" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "10" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "12" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "22" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "24" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "26" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "28" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "30" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "32" ; SVGAttr.Y "24" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "4" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "6" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "8" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "10" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "12" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "22" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "24" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "26" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "28" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "30" ; SVGAttr.Y "26" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "4" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "6" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "8" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "10" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "12" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "22" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "24" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "26" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "28" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "30" ; SVGAttr.Y "28" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "6" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "8" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "10" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "12" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "14" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "16" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "18" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "20" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "22" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "24" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "26" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "28" ; SVGAttr.Y "30" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "6" ; SVGAttr.Y "32" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "8" ; SVGAttr.Y "32" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "10" ; SVGAttr.Y "32" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "12" ; SVGAttr.Y "32" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "22" ; SVGAttr.Y "32" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "24" ; SVGAttr.Y "32" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "26" ; SVGAttr.Y "32" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(16,26,26,255)"][]
    rect [SVGAttr.X "28" ; SVGAttr.Y "32" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "8" ; SVGAttr.Y "34" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
    rect [SVGAttr.X "10" ; SVGAttr.Y "34" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "24" ; SVGAttr.Y "34" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(83,137,138,255)"][]
    rect [SVGAttr.X "26" ; SVGAttr.Y "34" ; SVGAttr.Width "2" ; SVGAttr.Height "2" ; SVGAttr.Fill "rgba(91,149,150,255)"][]
  |]