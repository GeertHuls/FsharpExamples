// Introduced in fsharp 3.0 in 2012.
// Is unique to fsharp.
// Fsharp has a powerfull type system and embraces programing with types.

// Automatically create types.
// Type providers are a way to easily get types into your program.
// They generate types at compile time so you don't have to create them yourself.
// Usually the type provider generates types by inspecting some external resource.

// Create types from:
// - SQL
// - Application configuration files (used to read app settings at compile time)
// - CSV
// - JSON
// - XML
// - R (the statistical programming language)

// Type providers type check external resources.
// If the resource was changed and does not match the program, it won't compile.

// Demo CSV type provider:

#r "../../packages/FSharp.Data/lib/net40/FSharp.Data.dll"
open FSharp.Data

// type providers use anglebrackets (<>) to pass data to the type provider:

// The schema param: says to skip the first 7 columns and then override the type of the last
// column to be an optional date.
let landings = CsvProvider<"typeproviders_Meteorite_Landings.csv",Schema = ",,,,,,date?">.GetSample()

let landingsWithYears = 
    landings.Rows
    |> Seq.filter (fun r -> 
                    r.Year.HasValue //gets intellisence from the columns in the csv!!!
                    && not (System.Double.IsNaN(r.``Mass (g)``))
                    && r.Year.Value.Year > 1770)

landingsWithYears  
  |> Seq.sortByDescending (fun r -> r.``Mass (g)``)
  |> Seq.map (fun r -> (r.Year,r.Name, r.``Mass (g)``)) // map to a tupple
  |> List.ofSeq
  |> printfn "%A"

#load "../../packages/fsharp.charting/FSharp.Charting.fsx"
open FSharp.Charting
landingsWithYears
  |> Seq.sortBy (fun r -> r.Year.Value)
  |> Seq.groupBy (fun r -> r.Year.Value.Year)
  |> Seq.map (fun (year,group) ->
                let largestByYear = group |> Seq.maxBy (fun r -> r.``Mass (g)``)
                (year, largestByYear.``Mass (g)``/1000.))
  |> Chart.Line
