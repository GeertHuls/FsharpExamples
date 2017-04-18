// Namespace examples, holding a set of types:
namespace This.Is.MyNamespace
type Class1() = 
  member this.X = "F#"

namespace This.Is.Another.MyNamespace
type Class2() =
  member this.Y = new This.Is.MyNamespace.Class1()


// Or open namespace instead of fully qualified class name:

namespace This.Is.Another.MyNamespace2

open This.Is.MyNamespace

type Class3() =
  member this.Y = new Class1()


namespace A

type Person = {name:string}
type Thing = {a:int}

namespace B

type Person = {age:int; otherPerson: A.Person}

open A
type ThingWrapper = {t:Thing}

// This is a value, and won't compile in namespaces.
// Use modules instead for this:
//let square x = x * x // ERROR
