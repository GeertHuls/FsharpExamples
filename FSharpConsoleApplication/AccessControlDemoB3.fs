module ModulesDemoB3

open A3

type Person = {age:int; otherPerson: A3.Person}
//type ThingWrapper = {t:Thing} // compile error, Thing is private

// This works because modules are allowed to contain values.
let square x = x * x
