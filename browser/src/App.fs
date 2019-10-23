module App

open Fable.Core.JsInterop
open Fable.Import

let window = Browser.Dom.window

// Get our canvas context 
// As we'll see later, myCanvas is mutable hence the use of the mutable keyword
// the unbox keyword allows to make an unsafe cast. Here we assume that getElementById will return an HTMLCanvasElement 
let mutable myCanvas : Browser.Types.HTMLCanvasElement = unbox window.document.getElementById "myCanvas"  // myCanvas is defined in public/index.html

// Get the context
let ctx = myCanvas.getContext_2d()

// All these are immutables values
let steps = 20
let squareSize = 20

// gridWidth needs a float wo we cast tour int operation to a float using the float keyword
let gridWidth = float (steps * squareSize) 

// resize our canvas to the size of our grid
// the arrow <- indicates we're mutating a value. It's a special operator in F#.
myCanvas.width <- 1000.
myCanvas.height <- 1000.
let w = myCanvas.width
let h = myCanvas.height

// print the grid size to our debugger console
printfn "%i" steps

// prepare our canvas operations
// [0..steps] // this is a list
//   |> Seq.iter( fun x -> // we iter through the list using an anonymous function
//       let v = float ((x) * squareSize) 
//       ctx.moveTo(v, 0.)
//       ctx.lineTo(v, gridWidth)
//       ctx.moveTo(0., v)
//       ctx.lineTo(gridWidth, v)
//     ) 

ctx.moveTo(w /2., h/2.)

let circlesteps = 40
let stepsize = 14.
[0..circlesteps] |>
  Seq.fold( fun curr step ->    
    let dir = step % 4
    let currx, curry = curr
    ctx.lineTo (currx, curry) |> ignore
    match dir with 
    | 0 -> currx + stepsize * float(step), curry
    | 1 -> currx, curry + stepsize * float(step)
    | 2 -> currx - stepsize * float(step), curry
    | 3 -> currx, curry - stepsize * float(step)
    | _ -> currx, curry
  ) (w/2., h/2.) 
  |> ignore

ctx.strokeStyle <- !^"#777" // color

// draw our grid
ctx.stroke() 

// write Fable
ctx.textAlign <- "center"
ctx.fillText("Fable on Canvas", gridWidth * 0.5, gridWidth * 0.5)

printfn "done!"