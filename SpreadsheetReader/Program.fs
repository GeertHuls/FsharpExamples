namespace SpreadsheetReader

open System.IO;
open NPOI.XSSF.UserModel;
open NPOI.SS.UserModel;

module Program =

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
