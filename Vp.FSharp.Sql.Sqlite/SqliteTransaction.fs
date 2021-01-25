[<RequireQualifiedAccess>]
module Vp.FSharp.Sql.Sqlite.SqliteTransaction

open Vp.FSharp.Sql
open Vp.FSharp.Sql.Sqlite


let private beginTransactionAsync = Constants.Deps.BeginTransactionAsync

let commit cancellationToken isolationLevel connection body =
    Transaction.commit cancellationToken isolationLevel connection beginTransactionAsync body
let notCommit cancellationToken isolationLevel connection body =
    Transaction.notCommit cancellationToken isolationLevel connection beginTransactionAsync body
let commitOnOk cancellationToken isolationLevel connection body =
    Transaction.commitOnOk cancellationToken isolationLevel connection beginTransactionAsync body
let commitOnSome cancellationToken isolationLevel connection body =
    Transaction.commitOnSome cancellationToken isolationLevel connection beginTransactionAsync body

let defaultCommit connection body = Transaction.defaultCommit connection beginTransactionAsync body
let defaultNotCommit connection body = Transaction.defaultNotCommit connection beginTransactionAsync body
let defaultCommitOnOk connection body = Transaction.defaultCommitOnOk connection beginTransactionAsync body
let defaultCommitOnSome connection body = Transaction.defaultCommitOnSome connection beginTransactionAsync body
