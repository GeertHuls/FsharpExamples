
// namespaces are the dotnet framework style solution for the
// same as modules.

// The difference is that namespaces cannot contain values,
// only type declarations. This makes it more suitable
// for OO code where namespaces will group a set of classes.

// In a functional style, we would like to put functions in
// a module and thus namespaces will not work.
namespace FSharp.Examples

// Learn more about F# at http://fsharp.org
// See the 'F# Tutorial' project for more help.

// modules are the ML style solution for grouping code and
// managing name collisions.
module MainFuncs = 
[<EntryPoint>]
let main argv = 
    printfn "%A" argv
    0 // return an integer exit code
