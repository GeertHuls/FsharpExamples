module ModulesDemoB2

open A2

type Person = {age:int; otherPerson: A2.Person}
type ThingWrapper = {t:Thing}

// This works because modules are allowed to contain values.
let square x = x * x
