[<RequireQualifiedAccess>]
module Vp.FSharp.Sql.Sqlite.SqliteTransaction

open Vp.FSharp.Sql
open Vp.FSharp.Sql.Sqlite


let private beginTransactionAsync = Constants.Deps.BeginTransactionAsync
let private beginTransaction = Constants.Deps.BeginTransaction

/// Create and commit an automatically generated transaction with the given connection, isolation,
/// cancellation token and transaction body.
/// This function runs asynchronously.
let commit cancellationToken isolationLevel connection body =
    Transaction.commit cancellationToken isolationLevel connection beginTransactionAsync body

/// Create and commit an automatically generated transaction with the given connection, isolation,
/// and transaction body.
/// This function runs synchronously.
let commitSync isolationLevel connection body =
    Transaction.commitSync isolationLevel connection beginTransaction body

/// Create and do not commit an automatically generated transaction with the given connection, isolation,
/// cancellation token and transaction body.
/// This function runs asynchronously.
let notCommit cancellationToken isolationLevel connection body =
    Transaction.notCommit cancellationToken isolationLevel connection beginTransactionAsync body

/// Create and do not commit an automatically generated transaction with the given connection, isolation,
/// and transaction body.
/// This function runs synchronously.
let notCommitSync isolationLevel connection body =
    Transaction.notCommitSync isolationLevel connection beginTransaction body

/// Create and commit an automatically generated transaction with the given connection, isolation,
/// cancellation token and transaction body.
/// The commit phase only occurs if the transaction body returns Some.
/// This function runs asynchronously.
let commitOnSome cancellationToken isolationLevel connection body =
    Transaction.commitOnSome cancellationToken isolationLevel connection beginTransactionAsync body

/// Create and commit an automatically generated transaction with the given connection, isolation,
/// and transaction body.
/// The commit phase only occurs if the transaction body returns Some.
/// This function runs synchronously.
let commitOnSomeSync isolationLevel connection body =
    Transaction.commitOnSomeSync isolationLevel connection beginTransaction body

/// Create and commit an automatically generated transaction with the given connection, isolation,
/// cancellation token and transaction body.
/// The commit phase only occurs if the transaction body returns Ok.
/// This function runs asynchronously.
let commitOnOk cancellationToken isolationLevel connection body =
    Transaction.commitOnOk cancellationToken isolationLevel connection beginTransactionAsync body

/// Create and commit an automatically generated transaction with the given connection, isolation,
/// and transaction body.
/// The commit phase only occurs if the transaction body returns Ok.
/// This function runs synchronously.
let commitOnOkSync isolationLevel connection body =
    Transaction.commitOnOkSync isolationLevel connection beginTransaction body

/// Create and commit an automatically generated transaction with the given connection and transaction body.
/// This function runs asynchronously.
let defaultCommit connection body = Transaction.defaultCommit connection beginTransactionAsync body

/// Create and commit an automatically generated transaction with the given connection and transaction body.
/// This function runs synchronously.
let defaultCommitSync connection body = Transaction.defaultCommitSync connection beginTransaction body

/// Create and do not commit an automatically generated transaction with the given connection and transaction body.
/// This function runs asynchronously.
let defaultNotCommit connection body = Transaction.defaultNotCommit connection beginTransactionAsync body

/// Create and do not commit an automatically generated transaction with the given connection and transaction body.
/// This function runs synchronously.
let defaultNotCommitSync connection body = Transaction.defaultNotCommitSync connection beginTransaction body

/// Create and commit an automatically generated transaction with the given connection and transaction body.
/// The commit phase only occurs if the transaction body returns Ok.
/// This function runs asynchronously.
let defaultCommitOnSome connection body = Transaction.defaultCommitOnSome connection beginTransactionAsync body

/// Create and commit an automatically generated transaction with the given connection and transaction body.
/// The commit phase only occurs if the transaction body returns Ok.
/// This function runs synchronously.
let defaultCommitOnSomeSync connection body = Transaction.defaultCommitOnSomeSync connection beginTransaction body

/// Create and commit an automatically generated transaction with the given connection and transaction body.
/// The commit phase only occurs if the transaction body returns Some.
/// This function runs asynchronously.
let defaultCommitOnOk connection body = Transaction.defaultCommitOnOk connection beginTransactionAsync body

/// Create and commit an automatically generated transaction with the given connection and transaction body.
/// The commit phase only occurs if the transaction body returns Some.
/// This function runs synchronously.
let defaultCommitOnOkSync connection body = Transaction.defaultCommitOnOkSync connection beginTransaction body
