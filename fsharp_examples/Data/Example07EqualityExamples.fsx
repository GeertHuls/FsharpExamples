// Equality is a curious mixture of functional and oo constructs.
// Programming in a functional style is all about programming with values,
// so equality with values is very important.

// Primitives, lists, arrays, tuples, records and discrimiated union have structural equality.

// The equality operator is '='
// The inequality operator is '<>'

[1;2;3] = [1;2;3]
// true

(1, 'a') = (1, 'a')
// true

// Objects are compared with references
