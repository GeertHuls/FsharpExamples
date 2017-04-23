// Collection type examples:
// 1) the list: this is the basic ordered collection
// 2) the array: for when random access is required
// 3) the sequence: this is the third and is abstract

// seq<'T>: is a lazily evaluated ordered collection
// The sequence supports ranges:
// seq {0..2..10}

// A list consumes more memory because each element is added to the list.
// Think of 1 million elements.
// However if I would create a sequence containing also 1 million elements,
// it hapens immediately and the memory allocation is tiny. Because a sequence
// only creates its elements as they are requested.

// Create large list of ints:
[0L..System.Int64.MaxValue]
// (this will throw an OutOfMemoryException)

// Create a sequence, but memory overhead is tiny:
seq  {0L..System.Int64.MaxValue}
// This whill work!

// Sequences support the same comprehension syntax for creating new sequences
// from existing sequences or from the carthesian product of existing sequences.

// seq {for i in [1..10] -> i*i}

// Functions are available in Collection.Seq module. This contains the familiar function
// that will work with lists and arrays such as map, filter and folds.

seq {0..2..10}                      

// Use comprehension syntax:  
seq {for i in [1..10] -> i*i}
// same as above:
seq {for i in seq {1..10} -> i*i}

// this builds a chess board:
let board = 
    seq {
        for row in [1..8] do
            for column in [1..8] do
                // pown is raising a number to the power of...
                yield pown (-1) (row + column)
    } 
    |> Seq.map (fun i -> if i = -1 then "x " else "o ")

let prnt i v =
    if i > 0 && i % 8 = 0 then 
        printfn ""
    printf "%s" v

// Let the iteri function apply the print function to the board sequence.
Seq.iteri prnt board
    
