module RequireQualifiedAccessDemo

// List is a module, map is a function.
// Here the module qualified access is used to the map function:
List.map ((*)2) [1;2;3]

// same as above, execpt you get error:
// this declaration opens the module '...', which is marked 'requireQualifyAccess'
//open List
// map ((*)2) [1;2;3]
// the reason why the List module is marked with requireQualifyAccess,
// is because the array module also specifies a map function:

// this is one is designed to work with arrays:
Array.map ((*)2) [|1;2;3|]
