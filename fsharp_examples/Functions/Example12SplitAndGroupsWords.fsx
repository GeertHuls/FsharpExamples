// source:
// http://usingprogramming.com/post/2017/08/23/getting-started-with-programming-and-getting-absolutely-nowhere-part-2

let split (c:char) (s:string) =
    s.Split(c)

let parts = "Some String With A Short Word" |> split ' '

let stringList = parts |> Array.toList

let groupStrings (acc:string list) str =
    match acc with
    | prev::tail when prev.Length <= 3 -> (sprintf "%s %s" prev str)::tail
    | _ -> str::acc

let groupedStrings = stringList |> List.fold groupStrings []

// Some + String ==> [""]
let tempRes0 = groupStrings [] "Some"
let tempRes = groupStrings  ["Some"] "String" 
let tempRes2 = groupStrings  ["String"; "Some"] "With"
let tempRes3 = groupStrings  ["With"; "String"; "Some"] "A"
let tempRes4 = groupStrings  ["A"; "With"; "String"; "Some"] "Short"


let finalResults = groupedStrings |> List.rev


// using split as infix operator
// http://usingprogramming.com/post/2017/08/25/getting-started-with-programming-and-getting-absolutely-nowhere-part-3

/// Splits a string into an array on the specified char.
let (<|>) (s:string) (c:char) = s |> split c
let parts2 = "Some String With A Short Word" <|> ' '
