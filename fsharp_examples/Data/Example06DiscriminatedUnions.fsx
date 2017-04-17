// Discriminated unions are type with a set of choices
// A type with a known set of different structures

// Types that can take different forms
// Also called sum types
// Can have methods and props

// Simple DU type:
type Day =
    Sunday
    | Monday
    | Tuesday
    | Wednesday
    | Thursday
    | Friday
    | Saturday

// DU types with data attached to the cases:
// this can be anything, from types, records and even other DU's
type Note = 
    A
    | ASharp
    | B
    | C
    | CSharp
    | D
    | DSharp
    | E
    | F
    | FSharp
    | G
    | GSharp

type Octave =
    One
    | Two
    | Three

// A sound is either a Rest, or a Note and and Octave
type Sound =
    Rest
    | Tone of Note * Octave

// Construct rest:
Rest
// val it: Sound = Rest

// Construct tone:
Tone (C, Two)
// val it: Sound = Tone (C,Two)

// DU's are often used with pattern matching because 
// the compiler knows all of the possible forms that a value may take,
// you can force the programmer to account for all the possibilities.

match Tone (C, Two) with
    | Tone (note, octave) -> sprintf "%A %A" note octave
    | Rest -> "---"
// val it: string = "C Two"

// FS 3.1 introduced an addition to DU:
// Named Union type fields
type SoundNameUnionFields =
    Rest
    | Tone of note: Note * octave: Octave

Tone(note = C, octave = Two)
