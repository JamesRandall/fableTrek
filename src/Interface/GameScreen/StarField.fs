module Interface.GameScreen.StarField

open Fable.React
open Fable.React.Props
open Fable.Core.JsInterop
open Browser.Dom
open Browser.Types

let private random = System.Random()
let private speed = 0.04

let private newCoord size =
  (random.Next(0,size)-(size/2)) |> float

let private getRandomColor () =
  let diceRoll = random.Next(0,100)
  if diceRoll < 25 then
    "rgb(230,3,3)"
  elif diceRoll < 50 then
    "rgb(252,186,3)"
  elif diceRoll < 75 then
    "rgb(192,192,192)"
  else
    "cyan"

type Star =
  { X: float
    Y: float
    PreviousX: float
    PreviousY: float
    Z: float
    Color: string
  }
  static member Create width height =
    let newX = width |> newCoord
    let newY = height |> newCoord
    let newStar =
      { X = newX
        Y = newY
        PreviousX = newX
        PreviousY = newY        
        Z = (random.Next(0,400) |> float)/100.
        Color = getRandomColor ()
      }
    newStar
  member st.Update width height =
    let newZ = st.Z + speed
    let updatedStar =
      { st with PreviousX = st.X
                PreviousY = st.Y
                Z = newZ
                X = st.X + (st.X*(speed*0.2)*newZ)
                Y = st.Y + (st.Y*(speed*0.2)*newZ)
      }
    if (updatedStar.X > (width/2.+50.)) || (updatedStar.X < (-width/2.-50.)) || (updatedStar.Y > (height/2.+50.)) || (updatedStar.Y < (-height/2.-50.)) then
      let newX = width |> int |> newCoord
      let newY = height |> int |> newCoord
      { updatedStar with X = newX
                         Y = newY
                         PreviousX = newX
                         PreviousY = newY
                         Z = 0.
      }
    else
      updatedStar
    

let view = FunctionComponent.Of(fun (props:{|Width:int ; Height:int |}) ->
  let halfWidth = (props.Width / 2) |> float
  let halfHeight = (props.Height / 2) |> float

  let renderStars (context:CanvasRenderingContext2D) stars =
    context.fillRect(-halfWidth-1.,-halfHeight-1.,halfWidth*2.+2.,halfHeight*2.+2.)
    stars |> Seq.iter(fun s ->
      context.lineWidth <- s.Z
      context.strokeStyle <- !^s.Color
      context.beginPath()
      context.moveTo(s.X, s.Y)
      context.lineTo(s.PreviousX, s.PreviousY)
      context.stroke()
    )
  
  Hooks.useEffect((fun _ ->
    let mutable stars = {0..200} |> Seq.map (fun _ -> Star.Create props.Width props.Height) |> Seq.toArray
    let context = ((document.getElementById "starfield") :?> HTMLCanvasElement).getContext_2d()
    context.fillStyle <-  !^"rgba(0,0,0,0.1)"
    context.translate(halfWidth, halfHeight)

    let rec updateAndDraw _ =
      stars <- stars |> Array.map (fun s -> s.Update (halfWidth*2.) (halfHeight*2.))
      renderStars context stars
      let starfield = (document.getElementById "starfield") 
      if not(isNull starfield) then // if the element has gone then we don't rerender
        window.requestAnimationFrame updateAndDraw |> ignore

    updateAndDraw 0.

  ), [||])
  
  canvas [
    Id "starfield"
    HTMLAttr.Width props.Width 
    HTMLAttr.Height props.Height
    Style [
      CSSProp.Background "radial-gradient(ellipse at center, #242938, #000 100%)"
    ]
  ] []
)