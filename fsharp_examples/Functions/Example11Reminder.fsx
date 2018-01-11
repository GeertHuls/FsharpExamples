
// source:
// http://usingprogramming.com/post/2017/08/21/getting-started-with-programming-and-getting-absolutely-nowhere

type User = {
    Id : int
    Name : string
    Email : string
    Phone : string option
}

let ebrown = {
    Id = 1
    Name = "Elliott"
    Email = "example@example.com"
    Phone = None
}

let createReminder thing user =
    sprintf "Hello %s, this is a reminder to do the thing (%s)." user.Name thing

// given a function x : ('a -> 'b)
// given a function y : ('b -> 'c)
// the composition of x and y would be: ('a -> 'c)
let remind =
    createReminder "Write a Blog Post" >> printfn "%s"


let test2 =
    createReminder "Write a Blog Post"

let test =
    printfn "%s"

let reminder =
    ebrown |> remind