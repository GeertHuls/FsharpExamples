// http://usingprogramming.com/post/2017/08/30/getting-started-with-programming-and-getting-absolutely-nowhere-part-5

let intList = [1;2;3;4]

// a function that check wheter list.length is equal to 0:
let isListEmpty l = l |> List.length = 0
let emptyResult = isListEmpty intList

// a function that checks wheter the number 4 is included in the list
let doesListContainsTheNumber4 l =
    l
    |> List.filter (fun i -> i = 4)
    |> List.length > 0

let listContainsTheNumber4 = doesListContainsTheNumber4 intList


type ListPerson = {
    FirstName : string
    MiddleName : string option
    LastName : string
    Email : string
    MagicId : string
}

let compare (m1 : ListPerson) (m2 : ListPerson) =
    // Compare mandatory values first
    m1.FirstName = m2.FirstName &&
    m1.LastName = m2.LastName &&
    m1.Email = m2.Email &&
    m1.MagicId = m2.MagicId &&
    // Default the match to `true` if either is `None` so that the previous conditions are the only influence
    match m1.MiddleName, m2.MiddleName with
    | Some a, Some b -> a = b
    | _ -> true

let compareCurry = compare { FirstName="fn"; LastName="ln"; MiddleName=None; Email="em"; MagicId="mid" }

let listCompare other el =
    other |> List.filter (compare el) |> List.length = 0

