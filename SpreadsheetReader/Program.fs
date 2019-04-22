namespace SpreadsheetReader

open System
open System.IO;
open NPOI.XSSF.UserModel;
open NPOI.SS.UserModel;

type Parameters = {
    NewListFilename : string
    OldListFilename : string
    SourceSheetName : string
}

module Program =

// http://usingprogramming.com/post/2017/09/01/getting-started-with-programming-and-getting-absolutely-nowhere-part-6
// default args:
  let fromDefault defaults args =
    match args with
    | [|newListFilename; oldListFilename; sourceSheetName|] -> Some { defaults with NewListFilename = newListFilename; OldListFilename = oldListFilename; SourceSheetName = sourceSheetName }
    | [|newListFilename; oldListFilename|] -> Some { defaults with NewListFilename = newListFilename; OldListFilename = oldListFilename }
    | [|newListFilename|] -> Some { defaults with NewListFilename = newListFilename }
    | _ -> None

  let addSheet func (workbook : IWorkbook) name list =
    let header = { FirstName = "First Name"; MiddleName = Some "Middle Name"; LastName = "Last Name"; Email = "Email"; MagicId = "Magic ID" }

    match name |> workbook.GetSheet |> Option.ofObj with
    | Some _ -> name |> workbook.GetSheetIndex |> workbook.RemoveSheetAt
    | None -> ()

    let sheet = name |> workbook.CreateSheet
    (header::list) |> List.iteri (fun i el -> i |> sheet.CreateRow |> func el)

  let newBookFilename = @"..\..\SpreadsheetReader\spreadsheets\Step3_Testing_New.xlsx"
  let newBook =
    use fs = new FileStream(newBookFilename, FileMode.Open, FileAccess.Read)
    XSSFWorkbook(fs)

  let oldBookFilename = @"..\..\SpreadsheetReader\spreadsheets\Step3_Testing_Old.xlsx"
  let oldBook =
    use fs = new FileStream(oldBookFilename, FileMode.Open, FileAccess.Read)
    XSSFWorkbook(fs)

  let newSheet = "Process_Spec" |> newBook.GetSheet
  let oldSheet = "Process_Spec" |> oldBook.GetSheet

  let newListMembers =
    [1..newSheet.LastRowNum]
    |> Seq.choose (newSheet.GetRow >> Option.ofObj)
    |> Seq.choose Merger.getSpecMember
    |> Seq.toList

  let oldListMembers =
    [1..oldSheet.LastRowNum]
    |> Seq.choose (oldSheet.GetRow >> Option.ofObj)
    |> Seq.choose Merger.getSpecMember
    |> Seq.toList

  // Getting members not on old list.
  let newListUnique = newListMembers |> Merger.filterBy oldListMembers
  // Getting members not on new list.
  let oldListUnique = oldListMembers |> Merger.filterBy newListMembers

  let run =
    // Write new excel file:
    let newSheetName = "Process_Spec_New_Add"
    let oldSheetName = "Process_Spec_Old_Add"
    let templateSheetName = "Process_Spec_Template"

    addSheet Merger.fillRow newBook newSheetName newListUnique
    addSheet Merger.fillRow newBook oldSheetName oldListUnique
    addSheet Merger.fillTemplateRow newBook templateSheetName newListUnique

    Merger.write newBook

  [<EntryPoint>]
  let main argv = 
    let defaults = { NewListFilename = ""; OldListFilename = ""; SourceSheetName = "Process_Spec" }

    let parameters =
        match argv |> fromDefault defaults with
        | Some p when p.NewListFilename <> "" && p.OldListFilename <> "" -> p
        | Some p when p.NewListFilename <> "" ->
            printf "Enter the old file name to load: "
            { p with OldListFilename = Console.ReadLine() |> String.stripDQuotes }
        | _ ->
            printf "Enter the new file name to load: "
            let temp = { defaults with NewListFilename = Console.ReadLine() |> String.stripDQuotes }
            printf "Enter the old file name to load: "
            { temp with OldListFilename = Console.ReadLine() |> String.stripDQuotes }

    match argv |> fromDefault defaults with
    | Some p when p.NewListFilename <> "" && p.OldListFilename <> "" ->
        printfn "Done."
    | _ ->
        printfn "Done. Press enter to exit."
        Console.ReadLine() |> ignore

    printfn "Getting members not on new list."
    0
