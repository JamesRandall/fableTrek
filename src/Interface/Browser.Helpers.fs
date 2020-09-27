module Interface.Browser.Helpers

open Browser.Types

let attachWindowEvent (f: Event->unit) (node: Window) (eventType: string) =
    node.addEventListener(eventType, f)
    { new System.IDisposable with
        member __.Dispose() = node.removeEventListener(eventType, f) }

let attachWindowEventWithDisposer (f: Event->unit) (node: Window) (eventType: string) (disposer:unit -> unit)=
    node.addEventListener(eventType, f)
    { new System.IDisposable with
        member __.Dispose() =
          disposer ()
          node.removeEventListener(eventType, f) }

open Fable.React
open Fable.React.Props

let debouncedSize (ref:IRefHook<option<Element>>) setSize = 
  Hooks.useEffectDisposable((fun () ->
    let mutable timeoutId = None
    let respondToSize () =
      match ref.current with
      | Some elementRef ->
          let element:Browser.Types.Element = elementRef
          let width = element.clientWidth |> int
          let height = element.clientHeight |> int
          match timeoutId with
          | Some tid -> Browser.Dom.window.clearTimeout(tid)
          | None -> ()
          timeoutId <- Some (Browser.Dom.window.setTimeout((fun _ -> timeoutId <- None ; setSize(width, height)), 150, []))
      | None -> ()
    
    respondToSize ()    
    attachWindowEventWithDisposer (fun _ -> respondToSize ()) Browser.Dom.window "resize" (fun () -> match timeoutId with | Some tid -> Browser.Dom.window.clearTimeout(tid) | None -> ())
  ), [||])