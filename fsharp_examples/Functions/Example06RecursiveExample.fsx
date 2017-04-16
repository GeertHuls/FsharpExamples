// the rec keyword is a hint to the recursive nature:
let rec length = function // the function keyword indicates that this function is a pattern matching function
    | [] -> 0 // the argument is matched against 2 patterns here, either an empty list or a non-empty list
    | x::xs -> 1 + length xs // the list is shortend and the length function is applied0 recursively

// the argument is immediately passed in as pattern match and therefore we never have to give it a name

// The first case [], this is an empty list and if the list is empty then we can immediately return 0
// this is the base case for the recursive function

// the second case x::xs, when the list is not empty, then we use a list pattern to split it appart
// into the head of the list and the rest of the list, which is called the tail.
// the body of the function then is 1 + the lenght of the tail (xs). This is where the function
// resurses back.

length [1;2;5]
//result = 3, it counts the length of the list

//Example 2
//factorial n == n * (n-1) * (n-2)
//factorial 3 == 3 * 2 * 1 == 6
let rec factorial n =
    if n < 2 then
        1
    else
        n * factorial (n-1)

factorial 3
// result = 6