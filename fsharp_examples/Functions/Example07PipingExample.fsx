sin 7. //the sin function expects a floating point argument, hence the trailing dot
// val it : float = 0.6569865987

sin 7.0
// same result as above

//sin 7
//error sin does not take and int value as argument


7. |> sin
// same result, different order

7. |> sin |> ((*) 2.)

let square x = x * x;

//3 |> square
// results to 9

3 |> double |> square
// results to 9.0



[1;2;3;4]
    |> List.filter (fun i -> i % 2 = 0)
    |> List.map ((*) 2)
    |> List.sum
// result = 12

// Pipe backwards:
sin (2. + 1.)
// val it : float = 0.1411200081

sin <| 2. + 1.
// same result as above

min 12 7 // min returns the smallest value
// result = 7

// using forward and backward pipe:
12 |> min <| 7
// the first argument is provided by the pipe forward operator
// the second argument is provided by the pipe backward operator
// result is 7

//some use this function to simulate an infix operator (see example 4)

// double pipe operator:
(12, 7) ||> min
// result = 7

//also possible with backward operator
min <|| (12, 7)
// result = 7

// also with tripple arguments:
//(1, 2, 3) |||> someTernaryFunction

