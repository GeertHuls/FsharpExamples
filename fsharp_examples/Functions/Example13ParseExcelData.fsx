// source:
// http://usingprogramming.com/post/2017/08/28/getting-started-with-programming-and-getting-absolutely-nowhere-part-4

let someLetterVal (c : char) =
    match int c with
    | v when v >= int 'A' && v <= int 'Z' -> Some (v - int 'A')
    | v when v >= int 'a' && v <= int 'z' -> Some (v - int 'a')
    | _ -> None

// every number char will result in a some:
let someRes= '5' |> someLetterVal
// Array.choose only selects the none - some values!
// In the case below, the '9' char will be ommited!
let chooseRes = [|'A'; '9'; 'B'|] |> Array.choose someLetterVal


// Continue with the excel parsing exercise:
let explode (s:string) =
    [|for c in s -> c|]

let letterValWithBug (c : char) =
    match int c with
    | v when v >= int 'A' && v <= int 'Z' -> Some (v - int 'A')
    | v when v >= int 'a' && v <= int 'z' -> Some (v - int 'a')
    | v when v >= int '0' && v <= int '9' -> Some (v - int '0')
    | _ -> None

// So this gets us a function which we can test out that should allow us to turn "A"
// into the first column number, which when 0-based is 0:
let colValue = 'A' |> letterValWithBug

// Being an F# function that returns an Option, we can do something like the following:
let colValues = "AB" |> explode |> Array.choose letterValWithBug

// The problem with this is that we really need a single number, not two.
// For AB we want to get 27, so we could build a function (colNum) that defines the following:
let colNumBug =
    explode
    >> Array.choose letterValWithBug
    >> Array.rev
    >> Array.mapi (fun i x -> (float x) * System.Math.Pow(26., i |> float))
    >> Array.sum
    >> int

colNumBug "BC" // Bug: returns 28!
colNumBug "AB" // Bug: returns 1!

// We have a huge bug with this though: for our value of "AB" it's going to return 1, not 27 as we would expect.
// Our mistake is not so subtle: the (float x) * is the problem: when x is 0 (as it is for A), it will always return 0

// We could fix this by doing float x + 1.
let letterValThreePlacesFix (c : char) =
    match int c with
    | v when v >= int 'A' && v <= int 'Z' -> Some (1 + v - int 'A')
    | v when v >= int 'a' && v <= int 'z' -> Some (1 + v - int 'a')
    | v when v >= int '0' && v <= int '9' -> Some (1 + v - int '0')
    | _ -> None


// But now we think to ourselves, 'why am I doing the same step (1 + v - int ...) in three places?
// Can I do that in one place?' We sure can:

let letterVal (c : char) =
    let f (c : char) v = 1 + v - int c
    match int c with
    | v when v >= int 'A' && v <= int 'Z' -> v |> f 'A' |> Some
    | v when v >= int 'a' && v <= int 'z' -> v |> f 'a' |> Some
    | v when v >= int '0' && v <= int '9' -> v |> f '0' |> Some
    | _ -> None

let sub x y = y - x

let baseValue b i =
    System.Math.Pow(b, i)

let colNum =
    let elementVal i x = i |> float |> baseValue 26. |> (*) x
    explode
    >> Array.choose letterVal
    >> Array.rev
    >> Array.map float
    >> Array.mapi elementVal
    >> Array.sum
    >> int
    >> sub 1


colNum "BC" // Correct: 54
colNum "AB" // Correct: 27

// Applied to an entire array:
["A"; "Z"; "AA"; "AB"; "ZZ"; "AAA"] |> List.map colNum //  [0; 25; 26; 27; 701; 702]


let testF inpt = 
    match inpt with
    | 1 -> None
    | _ -> Some inpt

let testM i x = i * x

let testC =
 Array.choose testF
 >> Array.mapi (fun i x -> (float x) * System.Math.Pow(26., i |> float))

[|1;2;3;4|] |> testC

// letterVal 'B'
// System.Math.Pow(26., 2 |> float)

let colNum2 =
    explode
    >> Array.choose letterVal


// https://fsharpforfunandprofit.com/posts/function-signatures/
let flatten<'a> : 'a array array -> 'a array = Array.collect id
let doubleArr = [|[|1|]|]
let flat = flatten doubleArr


let listCount<'a> : 'a list -> int = List.length
let counted = [1;2;3;10;15] |> listCount

