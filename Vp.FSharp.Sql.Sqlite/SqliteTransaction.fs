[<RequireQualifiedAccess>]
module Vp.FSharp.Sql.Sqlite.SqliteTransaction

open Vp.FSharp.Sql
open Vp.FSharp.Sql.Sqlite


let private beginTransactionAsync = Constants.Deps.BeginTransactionAsync

/// Create and commit an automatically generated transaction with the given connection, isolation,
/// cancellation token and transaction body.
let commit cancellationToken isolationLevel connection body =
    Transaction.commit cancellationToken isolationLevel connection beginTransactionAsync body

/// Create and do not commit an automatically generated transaction with the given connection, isolation,
/// cancellation token and transaction body.
let notCommit cancellationToken isolationLevel connection body =
    Transaction.notCommit cancellationToken isolationLevel connection beginTransactionAsync body

/// Create and commit an automatically generated transaction with the given connection, isolation,
/// cancellation token and transaction body.
/// The commit phase only occurs if the transaction body returns Ok.
let commitOnOk cancellationToken isolationLevel connection body =
    Transaction.commitOnOk cancellationToken isolationLevel connection beginTransactionAsync body

/// Create and commit an automatically generated transaction with the given connection, isolation,
/// cancellation token and transaction body.
/// The commit phase only occurs if the transaction body returns Some.
let commitOnSome cancellationToken isolationLevel connection body =
    Transaction.commitOnSome cancellationToken isolationLevel connection beginTransactionAsync body

/// Create and commit an automatically generated transaction with the given connection and transaction body.
let defaultCommit connection body = Transaction.defaultCommit connection beginTransactionAsync body

/// Create and do not commit an automatically generated transaction with the given connection and transaction body.
let defaultNotCommit connection body = Transaction.defaultNotCommit connection beginTransactionAsync body

/// Create and commit an automatically generated transaction with the given connection and transaction body.
/// The commit phase only occurs if the transaction body returns Ok.
let defaultCommitOnOk connection body = Transaction.defaultCommitOnOk connection beginTransactionAsync body

/// Create and commit an automatically generated transaction with the given connection and transaction body.
/// The commit phase only occurs if the transaction body returns Some.
let defaultCommitOnSome connection body = Transaction.defaultCommitOnSome connection beginTransactionAsync body
