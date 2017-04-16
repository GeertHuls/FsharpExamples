"It was the best of times,
it was the worst of times,"

"""This "string" includes double quotes"""

@"This ""string"" includes double quotes"

// try this with and without the @
@"Verbatim strings\n don't\t encode\n escape sequences"

// string indexing:
let intro = "It was the best of times,"
intro.[0]
intro.[1]

"It was the best of times,
it was the worst of times,".[3..5]

"It was the best of times,
it was the worst of times,".[..5]

"It was the best of times,
it was the worst of times,".[3..]


// core.string examples:
String.forall System.Char.IsDigit "0324243"
// result = true

String.init 10 (fun i -> i * 10 |> string)
// result = 0102030405060708090

String.init 10 (fun i -> string i)
// result = 012345679
