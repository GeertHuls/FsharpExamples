// Generics is type which contains a least 1 type parameter
// Are sometimes also called polymorphic types.
// Type paramaters are prefixed with a single quote e.g.: 'a

type NamedValue<'a> = {name: string; value: 'a}

// This is the type with the closed generic of int, with a value of 1:
{name = "Thing"; value = 1}
