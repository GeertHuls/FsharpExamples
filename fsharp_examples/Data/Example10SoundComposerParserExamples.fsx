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
