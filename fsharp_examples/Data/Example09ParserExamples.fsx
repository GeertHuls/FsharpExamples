// A parsers is a program that converts a string of symbols into a structured value.
// Fsharp has features to make it well suited for parsing.

// Parser Combinator Library: is a collection of function that parses different kinds of input
// and are designed to be composed toghether.

// A parser typically contains 2 parts: the parsers and the combinators.
// The parsers are functions that when applied to an input string of characters, produce a result
// and the remaining stream of characters.
// The combinators are helpers that augment parsers to gramar constructions such as sequencing,
// choice and repetition.

// Fparsec is a popular parser combinatory library for F#.AbstractClassAttribute

// Very simple parser example:

//run anyChar "abcdefg"
//    |> sprintf "%A"
// val it: string = "Success: 'a'"

// anyChar is the parser function.
// anyChar parses a single character from the input.
// run is the function that takes the parser and the string and applies the parser to the string.
// Both run and anyChar are Fparsec functions.
// The result from run, is formatted with sprintf.
// The result contains information about success or failure of the parser and the value that was
// produced. Here we say that parser was successful and the value produced was the character 'a'.

// To be a bit more selective about what we parse, there is the pchar function:
//let parsesA = pchar 'a'
// pchar 'a', creates a parser for the character 'a'

// run parsesA "abcdefg"
//    |> sprintf "%+A"
// val it: string = "Success: 'a'"

// Consumes the first character and produces the value 'a'

// Parser example for a comma and space listed floats
// parse 1.1, 3.7
//let plistoffloats =
//    (sepBy pfloat (pchar ',' .>> spaces))
// Fparsec has a combinator called 'sepBy' that helps us parse a list of values.
// sepBy has 2 arguments, both of which are parsers:
// The first argument is the parser for the values (pfloat).
// The second arugment (between parenthesis) is another parser, but this one is for parsing
// separators between list items. Our list items are separated by a comma, followed by 0 or more spaces.
// If spaces where not allowed, one could simple use (pchar ','). However, it needs to deal with spaces.
// So the pchar comma parser is combined with the spaces parser, using the dot gt gt operator to 
// sequence the parsers. Spaces is also a parser function that skips over any sequence of whitespace
// characters. The dot gt gt combinator applies the combinator on the left (pchar ','), then applies
// the combinator on the right (spaces) and returns the result to the combinator on the left (sepBy pfloat).

// Test the parser
// run plistoffloats "1.1, 3.7"
// val it: ParserResult<float list,unit> = Success: [1.1; 3.7]

type Point = {x: float; y:float}
// Now try this parser with a pair of floats:
// parse 1.1, 3.7
// Fparsec contains a function called pipe3 that combines 3 parsers:

//let plistoffloats2 =
//    pipe3 pfloat (pchar ',' .>> spaces) pfloat
//    (func x z y -> { x = x, y = y}) // return a value of type Point

// the pipe3 function takes 4 arguments, the first 3 are parsers and the final argument
// is a function that combines the result of the 3 parsers.
// The result of the x value, is the first ploat, the z value is the (pchar ',' .>> spaces) parser
// and the y value is the result of the second pfloat.

// run plistoffloats2 "1.1, 3.7"
// val it = Success: {x = 1.1; y = 3.7}

// Extend parser with pairs of floats, wrapped in parenthesis:
// parse (1.1, 3.7)
//let ppoint' = 
//    between
//    (pchar '(')
//    (pchar ')')
//    plistoffloats2
// run ppoint' "(1.1, 3.7)"
// val it = Success: {x= 1.1; y=3.7;}

// Fparsec has another usefull combinator called 'between'. Between is usefull for parsing 
// things, wrapped between things. Between takes 3 parser function as arguments.
