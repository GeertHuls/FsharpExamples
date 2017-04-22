module CSharpInterop
//#r @"..\CSharpLibrary\bin\Debug\CSharpLibrary.dll"

// Example C# class:
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace CSharpLibrary
//{
//    public class Numbers
//    {
//        public int FirstCountingNumber() 
//        { return 1; }
//    }

//    public interface ICanAddNumbers
//    {
//        int Add(int a, int b);
//    }
//}

// F# usage:
//let n = new CSharpLibrary.Numbers()
//n.FirstCountingNumber()


// Implement C# interface:
//open CSharpLibrary
//type Consumer () =
//  interface ICanAddNumbers with
//    member this.Add (a, b) = a + b


// In FSharp, you can instantiate an interface, whithout the need of a class:
//{new ICanAddNumbers
// with members this.Add (a, b)=
//   a + b}

// Other example:
//namespace Interop
//open CSharpLibrary

//type Consumer() = 
//    let c1 = new Numbers()
//    member this.X = c1.FirstCountingNumber()

//    interface ICanAddNumbers with
//        member this.Add (a,b) = a + b


// Example out params:
//let (isSuccess, value) = Double.TryParse("3.14159")
// type returned from TryParse is: (bool * float)

//open System

//Double.TryParse("3.14159") |> (fun result ->
//  match result with
//    | (true, value) -> printfn "%f" value
//		| (false, _) -> printfn "could not parse")

// OR:

//Double.TryParse("3.14159") |> function
//    | (true, value) -> printfn "%f" value
//    | (false, _) -> printfn "could not parse"

//let (yeahOrNeah, value) = Double.TryParse("3.141five9")
