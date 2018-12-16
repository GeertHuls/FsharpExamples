namespace SpreadsheetReader

module Array =
    /// Flattens an array of arrays to a single-dimensional array. (Equivalent to Array.collect id)
    let flatten<'a> : 'a array array -> 'a array = Array.collect id

module List =
    /// Flattens a list of lists to a single-dimensional list. (Equivalent to List.collect id)
    let flatten<'a> : 'a list list -> 'a list = List.collect id

    let listCompare other pred el =
      other |> List.filter (pred el) |> List.length = 0

module Seq =
    /// Flattens a sequence of sequences to a single-dimensional sequence. (Equivalent to Seq.collect id)
    let flatten<'a> : 'a seq seq -> 'a seq = Seq.collect id