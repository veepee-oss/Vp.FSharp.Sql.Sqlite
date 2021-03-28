namespace Vp.FSharp.Sql.Sqlite

open System.Data
open System.Data.SQLite
open System.Threading.Tasks

open Vp.FSharp.Sql


/// Native SQLite DB types.
/// See https://www.sqlite.org/datatype3.html
type SqliteDbValue =
    | Null
    | Integer of int64
    | Real of double
    | Text of string
    | Blob of byte array

/// SQLite Command Definition
type SqliteCommandDefinition =
    CommandDefinition<
        SQLiteConnection,
        SQLiteCommand,
        SQLiteParameter,
        SQLiteDataReader,
        SQLiteTransaction,
        SqliteDbValue>

/// SQLite Configuration
type SqliteConfiguration =
    SqlConfigurationCache<
        SQLiteConnection,
        SQLiteCommand>

/// SQLite Dependencies
type SqliteDependencies =
    SqlDependencies<
        SQLiteConnection,
        SQLiteCommand,
        SQLiteParameter,
        SQLiteDataReader,
        SQLiteTransaction,
        SqliteDbValue>

[<AbstractClass; Sealed>]
type internal Constants private () =

    static member DbValueToParameter name value =
        let parameter = SQLiteParameter()
        parameter.ParameterName <- name
        match value with
        | Null ->
            parameter.TypeName <- "NULL"
        | Integer value ->
            parameter.TypeName <- "INTEGER"
            parameter.Value <- value
        | Real value ->
            parameter.TypeName <- "REAL"
            parameter.Value <- value
        | Text value ->
            parameter.TypeName <- "TEXT"
            parameter.Value <- value
        | Blob value ->
            parameter.TypeName <- "BLOB"
            parameter.Value <- value
        parameter

    static member Deps : SqliteDependencies =
        let beginTransactionAsync (connection: SQLiteConnection) (isolationLevel: IsolationLevel) _ =
            ValueTask.FromResult(connection.BeginTransaction(isolationLevel))

        let executeReaderAsync (command: SQLiteCommand) _ =
            Task.FromResult(command.ExecuteReader())

        { CreateCommand = fun connection -> connection.CreateCommand()
          SetCommandTransaction = fun command transaction -> command.Transaction <- transaction
          BeginTransaction = fun connection -> connection.BeginTransaction
          BeginTransactionAsync = beginTransactionAsync
          ExecuteReader = fun command -> command.ExecuteReader()
          ExecuteReaderAsync = executeReaderAsync
          DbValueToParameter = Constants.DbValueToParameter }
