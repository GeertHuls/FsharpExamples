
// Grouping of unnamed, ordered value
// Often used to store intermediate results within a calculation
// or to return multiple values from a function

// Values can have different types:
(2, "hat", 2.78, false)

// Access tuple value via position

// First element:
fst ("Bob", 55)
// Second element:
snd ("Bob", 55)

// the fst and snd function are only defined for pairs, does not work with triples or more
// pattern matching often is used to get values from tuples

// Deconstruction:
let (name, age) = ("Bob", 55)


// Records
// Tuples are great for local use, records are used to pass accros functions
// Records add names to values

type Person = {
    name: string;
    age: int
}

// Create a new Person value:
{name = "Bob"; age = 55}

// access name
{name = "Bob"; age = 55}.age


// Unlike tuples, records can have members, methods and props.
type Person2 = {
    name: string;
    age: int
} with member this.CanDrive = this.age > 17
 
{name = "Bob"; age = 55}.CanDrive

// Modifying records:
// Use with keyword to do this:
let bob = {name="Bob"; age=55}
let bobClone = {bob with age = 56}
