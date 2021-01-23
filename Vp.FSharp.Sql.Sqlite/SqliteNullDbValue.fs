[<RequireQualifiedAccess>]
module Vp.FSharp.Sql.Sqlite.SqliteNullDbValue

open Vp.FSharp.Sql


let ifNone toDbValue = NullDbValue.ifNone toDbValue SqliteDbValue.Null
let ifError toDbValue = NullDbValue.ifError toDbValue (fun _ -> SqliteDbValue.Null)
