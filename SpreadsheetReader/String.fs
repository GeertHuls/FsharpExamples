namespace SpreadsheetReader

module Char =
    /// Returns true if a char is equal to the double-quote (").
    let isDQuote = (=) '"'
    /// Returns true if a char is equal to the single-quote (').
    let isSQuote = (=) '''
    /// Returns true if a char is equal to the double-quote (") or single-quote (').
    let isQuote c = c |> isDQuote || c |> isSQuote

module String =
    open System
    /// Gets the char array of a string.
    let explode : string -> char array = Seq.toArray
    /// Assembles a string from a char array.
    let implode : char array -> string = String
    /// Splits a string using the StringSplitOptions on the specified char.
    let splitOpt (options : StringSplitOptions) (c : char) (str : string) = str.Split([|c|], options)
    /// Splits a string on the specified char using the default StringSplitOptions.
    let split (c : char) (str : string) = str.Split(c)
    /// Determines if a string contains a substring.
    let contains (search : string) (subject : string) = subject.Contains(search)
    /// Performs a String.Trim() which removes leading and trailing whitespace.
    let trim (str : string) = str.Trim()

    let private processStr func = explode >> func >> implode
    let private filterStr func = explode >> Array.filter func >> implode

    /// Removes all double-quote characters from a string.
    let stripDQuotes = filterStr (Char.isDQuote >> not)
    /// Removes all single-quote characters from a string
    let stripSQuotes = filterStr (Char.isSQuote >> not)
    /// Removes all double- or single-quote characters from a string in one pass.
    let stripQuotes = filterStr (Char.isQuote >> not)
    /// Filters a string to contain only digits.
    let digitsOnly = filterStr Char.IsDigit
    /// Filters a string to contain only letters.
    let lettersOnly = filterStr Char.IsLetter
