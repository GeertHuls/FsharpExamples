open NPOI.SS.UserModel
open System
#r @"..\..\packages\NPOI\lib\net40\NPOI.dll"
#r @"..\..\packages\NPOI\lib\net40\NPOI.OOXML.dll"
#r @"..\..\packages\NPOI\lib\net40\NPOI.OpenXml4Net.dll"

open System.IO
open NPOI.XSSF.UserModel
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

// Generic version - takes a predicate as extra parameter:
let listCompare2 (other: 'a list) (pred: 'a -> 'a -> bool) (el: 'a) =
    other |> List.filter (pred el) |> List.length = 0


// Example of map function with record types:
let changePerson u: ListPerson = {
    u with MiddleName = Some "blah";
        FirstName = "Change firstName"
}

// Here the changed person keeps the properties of its original except for the first and middle name
let changedPerson = changePerson { FirstName="fn"; LastName="ln"; MiddleName= Some "some name"; Email="em"; MagicId="mid" }


// We distinct on every list person's property but the middlename:
let distinctPerson = List.distinctBy (fun u -> { u with MiddleName = None })
let filterBy other = List.filter (listCompare other) >> distinctPerson

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

let toStr o = o.ToString()

let  explode : string -> char array = Seq.toArray
let implode : char array -> string = String
let filterStr func = explode >> Array.filter func >> implode
let digitsOnly = filterStr Char.IsDigit

let setValue (s : string) (cell:ICell) =
    s |> cell.SetCellValue

let setValueOpt (s : string option) (cell:ICell) =
    s |> Option.map (fun s -> cell |> setValue s) |> ignore

let getVal (cell : ICell) : string option = 
    cell
    |> Option.ofObj
    |> Option.map (fun cell ->
        cell.CellType
        |> (function | CellType.Boolean -> cell.BooleanCellValue |> toStr | CellType.Numeric -> cell.NumericCellValue |> toStr | _ -> cell.StringCellValue))


// We use the options to tell us if we have a valid spec or not, when matched with Seq.choose it will filter away all the None values.
let getSpecMember (row : IRow) =
    let get = row.GetCell >> getVal
    match 0 |> get, 1 |> get, 2 |> get, 3 |> get, 4 |> get with
    | Some fName, mName, Some lName, Some email, Some magicId ->
        Some { FirstName = fName; MiddleName = mName; LastName = lName; Email = email; MagicId = magicId }
    | _ -> None

let newListMembers =
    [1..newSheet.LastRowNum]
    |> Seq.choose (newSheet.GetRow >> Option.ofObj)
    |> Seq.choose getSpecMember
    |> Seq.toList

let oldListMembers =
    [1..oldSheet.LastRowNum]
    |> Seq.choose (oldSheet.GetRow >> Option.ofObj)
    |> Seq.choose getSpecMember
    |> Seq.toList


printfn "Getting members not on old list."
let newListUnique = newListMembers |> filterBy oldListMembers

printfn "Getting members not on new list."
let oldListUnique = newListMembers |> filterBy oldListMembers

let fillRow el (row : IRow) =
    0 |> row.CreateCell |> setValue el.FirstName
    1 |> row.CreateCell |> setValueOpt el.MiddleName
    2 |> row.CreateCell |> setValue el.LastName
    3 |> row.CreateCell |> setValue el.Email
    4 |> row.CreateCell |> setValue el.MagicId

let fillTemplateRow el (row : IRow) =
    0 |> row.CreateCell |> setValue el.FirstName
    1 |> row.CreateCell |> setValueOpt el.MiddleName
    2 |> row.CreateCell |> setValue el.LastName
    3 |> row.CreateCell |> setValue el.Email
    4 |> row.CreateCell |> setValue el.MagicId
    5 |> row.CreateCell |> setValue (el.MagicId |> digitsOnly)

// Write new excel file:
let newSheetName = "Process_Spec_New_Add"
let oldSheetName = "Process_Spec_Old_Add"
let templateSheetName = "Process_Spec_Template"

let header = { FirstName = "First Name"; MiddleName = Some "Middle Name"; LastName = "Last Name"; Email = "Email"; MagicId = "Magic ID" }
let addSheet func (workbook : IWorkbook) name list =
    match name |> workbook.GetSheet |> Option.ofObj with
    | Some _ -> name |> workbook.GetSheetIndex |> workbook.RemoveSheetAt
    | None -> ()

    let sheet = name |> workbook.CreateSheet
    (header::list) |> List.iteri (fun i el -> i |> sheet.CreateRow |> func el)

addSheet fillRow newBook newSheetName newListUnique
addSheet fillRow newBook oldSheetName oldListUnique
addSheet fillTemplateRow newBook templateSheetName newListUnique

let write (book : IWorkbook) =
    use fs = File.Create("Spec3_Testing_Finished.xlsx")
    book.Write(fs)

write newBook