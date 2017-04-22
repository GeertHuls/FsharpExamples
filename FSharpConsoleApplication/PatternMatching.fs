module PatternMatching

// Pattern matching is matching values accross and equation
// Available wherever identifiers are bound
// Simple pattern match example, also called destructoral assignment:
let (a,b) = (1,2)

printf "a = %d" a
printf "b = %d" b


// The value pass to addPair is pattern matched to this expression:
let addPair (f,s) =
  f + s

addPair (2,4)

// this a more verbose way of the addPair functin, achieving the same thing:

let addPair2 p =
  match p with
    | (f, 0) -> f
    | (0, s) -> s
    | (f, s) -> f + s

addPair2 (0, 2)

let fizzbuzzer i =
    match i with
        | _ when i % 3 = 0 && i % 5 = 0 -> "fizzbuzz"
        | _ when i % 3 = 0 -> "fizz"
        | _ when i % 5 = 0 -> "buzz"
        | _ -> string i

[1..100] |> List.map fizzbuzzer


// Things that can be matched:

// - Constants
// - Tuples: can be matched on their dimension or values
// - Records: same as for tuples
// - Discriminated union cases
// - Lists: this is usefull if you know some items from the list
// - Arrays
// - Type tests
// - null