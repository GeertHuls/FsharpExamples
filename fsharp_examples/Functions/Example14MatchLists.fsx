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

