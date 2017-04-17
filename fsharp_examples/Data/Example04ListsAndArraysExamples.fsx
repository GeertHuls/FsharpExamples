

[]
// val it: a' list = []

// List literal:
[1;2;3]

// Add 1 at the head of the list:
1 :: [2;3]

1::(2::(3::[]))
// [1;2;3]


// List ranges:
[1..5]

// List from 1 to 5, counting up by 2
[1..2..5]
// [1;3;5]

// Count down:
[10..-1..0]

// List concatenation:
['a', 'b'] @ ['c','d']


// List comprehensions
// contains 3 part:
[for x in 1..10 do yield 2 * x]
// 1) the source of values: 1 to 10
// 2) a pattern that uses an identitfier for the elements of the collection: for x
// 3) and the output function that produces a value from the named elements: do yield 2 * x

// a simplified syntax for the above is this:
[for x in 1..10 -> 2 * x]

// Nest list comprehensions
[
    for r in 1..8 do
    for c in 1..8 do
        if r <> c then
            yield (r,c)
]
// each r value is paired with each c value

// Arrays:
// Arrays are a lot like list but with different performance characteristics
// and the ability to directly index items in the array. A list is a linked list.

[|1;2;3|].[1]
[|1;2;3|].[1..]
[|1;2;3|].[0..1]

// Can also use comprehension syntax:
[|for x in [1..3]
    do yield 2 * x|]
