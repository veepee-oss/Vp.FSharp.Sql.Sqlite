[<RequireQualifiedAccess>]
module Vp.FSharp.Sql.Sqlite.SqliteNullDbValue

open Vp.FSharp.Sql


/// Return SQLite DB Null value if the given option is None, otherwise the underlying wrapped in Some.
let ifNone toDbValue = NullDbValue.ifNone toDbValue SqliteDbValue.Null

/// Return SQLite DB Null value if the given option is Error, otherwise the underlying wrapped in Ok.
let ifError toDbValue = NullDbValue.ifError toDbValue (fun _ -> SqliteDbValue.Null)
