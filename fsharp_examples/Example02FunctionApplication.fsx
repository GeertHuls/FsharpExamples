let square x = x * x;
square 3
// result = 9

// first the square of 3 is applied, next 1 is added:
square 3 + 1
// result = 10

// has the same result as above:
(square 3) + 1
// result = 10

// this will first add 1 to 3 and then apply the squary function
square (3 + 1)
// result = 16

// F# is left associative

//Function with multiple arguments
let distance x y = x - y |> abs

distance 5 2
// result = 3

// this is also 3 because of the absolute value:
distance 2 5
// result = 3

distance 5 2 + 1
// result = 4

// as above
(distance 5 2) + 1
// result = 4

