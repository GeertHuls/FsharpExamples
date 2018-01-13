namespace SpreadsheetReader

module Excel =

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
    String.explode
    >> Array.choose letterVal
    >> Array.rev
    >> Array.map float
    >> Array.mapi elementVal
    >> Array.sum
    >> int
 
  let colNumZBase = colNum >> sub 1
