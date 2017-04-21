module Exceptions

try
  failwith "An error message"
with
  | Failure msg -> printfn "Failed with %s" msg


try

  try
    failwith "An error message"
  with
    | Failure msg -> printfn "Failed with %s" msg

finally
  printfn "This always evaluates"

// Custom exceptions:

exception CountingException of string * int

try
  raise (CountingException("number", 1))
with
  | CountingException(msg, num) ->
    printfn "This is number %d" num

// .net exceptions:

try
  1 / 0
  // raise (new System.Exception("General error")))
with
  | :? System.DivideByZeroException as e ->
    printfn "Error  %s" e.Message
    0
  | :? System.Exception as ex when ex.Message = "General error" ->
    printfn "Some other error %s" ex.Message
    0