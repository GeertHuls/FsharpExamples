#r @"..\..\packages\FParsec\lib\net40-client\FParsecCS.dll"
#r @"..\..\packages\FParsec\lib\net40-client\FParsec.dll"

open FParsec

let test p str =
    match run p str with
    | Success(result, _, _)   -> printfn "Success: %A" result
    | Failure(errorMsg, _, _) -> printfn "Failure: %s" errorMsg

type MeasureFraction = Half | Quarter | Eighth | Sixteenth | Thirtyseconth 
type Length = { fraction: MeasureFraction; extended: bool }
type Note = A | ASharp | B | C | CSharp | D | DSharp | E | F | FSharp | G | GSharp
type Octave = One | Two | Three
type Sound = Rest | Tone of note: Note * octave: Octave
type Token = { length: Length; sound: Sound }

// this is a example text to be parsed:
let aspiration = "32.#d3"


let pmeasurefraction = (stringReturn "2" Half) <|> (stringReturn "4" Quarter) <|> (stringReturn "8" Eighth) <|> (stringReturn "16" Sixteenth) <|> (stringReturn "32" Thirtyseconth)
// (stringReturn "2" Half) meaning: If the first character is a 2, we'll consume that character and
// produce a result, which is a half.

// <|>: this is a choice combinator of fparsec.

// (stringReturn "4" Quarter): this is the second parser, this time for quarter notes.

// pmeasurefranction will first try to parse the '2', if that fails, it will parse the '4', then the '8',
// and soforth.

// Test:
test pmeasurefraction aspiration
// result: Success: Thirtyseconth

let pextendedParser = (stringReturn "." true) <|> (stringReturn "" false)

// The pextendedParser parser is looking for the dot character.

let plength = pipe2 
                pmeasurefraction            
                pextendedParser 
                (fun t e -> {fraction = t; extended = e})

// plength: combines parser pmeasurefraction and pextendedParser into a single parser that
// will parse the first 3 characters.

// Another test:
test plength "16"
// result: Success: {fraction = Sixteenth; extended = false}
test plength aspiration
// result: Success: {fraction = Thirtyseconth; extended = true;}
test plength "asfsad"
// result: Failure

let psharp = (stringReturn "#" true) <|> (stringReturn "" false)

// psharp, parses the # character, this is the same trick as we used for the dot.
// This only returns a boolean.

let psharpnote = pipe2 
                    psharp 
                    (anyOf "acdfg") 
                    (fun isSharp note -> 
                        match (isSharp, note) with
                        | (false, 'a') -> A
                        | (true, 'a') -> ASharp                    
                        | (false, 'c') -> C
                        | (true, 'c') -> CSharp
                        | (false, 'd') -> D
                        | (true, 'd') -> DSharp
                        | (false, 'f') -> F
                        | (true, 'f') -> FSharp
                        | (false, 'g') -> G
                        | (true, 'g') -> GSharp
                        | (_,unknown) -> sprintf "Unknown note %c" unknown |> failwith)

// pipe2, if psharpnote "#b" is invoked,
// then pipe2 will first pass the character '#' to the psharp function
// then will pass the 'b' character to the anyOf function.
// Important: all charcharters are parsed one by one.

let pnotsharpablenote = anyOf "be" |>> (function 
                        | 'b' -> B
                        | 'e' -> E
                        | unknown -> sprintf "Unknown note %c" unknown |> failwith)

// anyOf is fparsec parser function, this parser will parse any character in the string that we give it,
// in this case 'be'.
// |>>: this is the pipleline combinator, to apply a function to the result of the anyOf parser.
// | 'b' --> these are function expression to
// | 'e' --> match on the value. The first case is when the anyOf result is a 'b', and we will map
//           that to a B. The 'e' will be mapped to an E.
// | unknown --> to keep the compile happy, we'll also have to account for other cases.
//               failwith is a fsharp function, which takes a string as first argument. It throws an exception.

let pnote = pnotsharpablenote <|> psharpnote

// pnote parser is simply the choice bewteen a not sharpable not and a sharpable note.

//Test of pnote:
// Test non-sharpable case:
test pnote "b"
// Success: B

// Tet sharpable case:
test pnote "#a"
// Success: A

// Try a b sharp, (which does not exist)
test pnote "#b"
// Failure: Expecting: any char in 'acdfg'
