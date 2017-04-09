// given a function x : ('a -> 'b)
// given a function y : ('b -> 'c)
// the composition of x and y would be: ('a -> 'c)


// using forward operator:
// x >> y
// using backward operator:
// y << x
// both 2 examples both produce ('a -> 'c)

// foward commpotistion operator signature:
// >> : ('a -> 'b) -> ('b -> 'c) -> 'a -> 'c

// Demoes:

let minus1 x = x - 1
let times2 = (*) 2

minus1 9
times2 8

times2 (minus1 9)
let minus1ThenTimes2 = times2 << minus1
minus1ThenTimes2 9
(times2 << minus1) 9
times2 << minus1 <| 9

minus1 (times2 9)
let times2ThenMinus1 = times2 >> minus1
times2ThenMinus1 9
