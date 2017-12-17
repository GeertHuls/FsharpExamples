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
