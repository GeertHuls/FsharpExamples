
// Choice <'T1, 'T2>
// Values may be of either type ('T1 or 'T2)
// Often used to represent the possibility of an error.

// In fsharp exceptions are rarely thrown, instead the choice
// type is used, to represent either the success value
// or the error value.

// Example function defination:
// val parse : st: string -> Choice<string, bool>
// The result is of type choice which either can be a string or a bool.
// The parser is parsing a string to a boolean, the string result is used to carry
// an error message in the event that the parsing fails.

open System

let div num den =
    num / den

div 15 3
div 15 0
// this will throw a divide by zero exception!


// Use the choice type to catch the error instead: 
let safeDiv num den =
    if den = 0. then
        Choice1Of2 "divide by zero is undefined"
    else
        Choice2Of2 (num / den)

safeDiv 15. 3.
safeDiv 15. 0.
// won't trow an exceptions!

// > let parse st = 
//     match st with 
//       | "t" -> true
//       | "f" -> false