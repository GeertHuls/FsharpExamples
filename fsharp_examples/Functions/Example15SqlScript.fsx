#r "../../packages/NETStandard.Library.NETFramework/build/net461/lib/netstandard.dll"
#r "../../packages/SQLProvider/lib/netstandard2.0/FSharp.Data.SqlProvider.dll"

// https://github.com/Microsoft/visualfsharp/issues/3309


open FSharp.Data.Sql

// install: https://www.nuget.org/packages/SQLProvider/

let vendor = Common.DatabaseProviderTypes.MSSQLSERVER

// example: http://fsprojects.github.io/SQLProvider/

// type dbSchema = SqlDataProvider<
//     Common.DatabaseProviderTypes.MSSQLLocalDB,
//     "Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True">
// let db = dbSchema.GetDataContext()
