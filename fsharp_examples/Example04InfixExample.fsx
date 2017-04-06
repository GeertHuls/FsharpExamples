
3 + 1
// + is also a function

(+) 3 1
// use + as a regular function

let add1 = (+) 1
//curry infix function

add1 3
//results to 4

1 + 2 + 3
// how infix operators work with more than 2 params:

((+) ((+) 1 2) 3)
// same as above

let distance x y = x - y |> abs
// the distance function with infix operator looks like this:

let (|><|) x y = x - y |> abs

5 |><| 2
// this is the distance function, the result is 3

// use multiple infix distance functions:
5 |><| 2 |><| 10
// result = 7
