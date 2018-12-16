namespace SpreadsheetReader
    
open System.IO;
open NPOI.SS.UserModel;

module Merger =

  // We use the options to tell us if we have a valid spec or not, when matched with Seq.choose it will filter away all the None values.
  let getSpecMember (row : IRow) =
    let get = row.GetCell >> Excel.getVal
    match 0 |> get, 1 |> get, 2 |> get, 3 |> get, 4 |> get with
    | Some fName, mName, Some lName, Some email, Some magicId ->
        Some { FirstName = fName; MiddleName = mName; LastName = lName; Email = email; MagicId = magicId }
    | _ -> None

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

  let distinctPerson = List.distinctBy (fun u -> { u with MiddleName = None })
  let filterBy other = List.filter (List.listCompare other compare) >> distinctPerson

  let fillRow el (row : IRow) =
    0 |> row.CreateCell |> Excel.setValue el.FirstName
    1 |> row.CreateCell |> Excel.setValueOpt el.MiddleName
    2 |> row.CreateCell |> Excel.setValue el.LastName
    3 |> row.CreateCell |> Excel.setValue el.Email
    4 |> row.CreateCell |> Excel.setValue el.MagicId

  let fillTemplateRow el (row : IRow) =
    0 |> row.CreateCell |> Excel.setValue el.FirstName
    1 |> row.CreateCell |> Excel.setValueOpt el.MiddleName
    2 |> row.CreateCell |> Excel.setValue el.LastName
    3 |> row.CreateCell |> Excel.setValue el.Email
    4 |> row.CreateCell |> Excel.setValue el.MagicId
    5 |> row.CreateCell |> Excel.setValue (el.MagicId |> String.digitsOnly)

  let write (book : IWorkbook) =
    use fs = File.Create("Spec3_Testing_Finished.xlsx")
    book.Write(fs)
