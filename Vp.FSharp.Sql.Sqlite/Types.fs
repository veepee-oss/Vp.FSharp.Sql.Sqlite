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
            parameter.TypeName <- (nameof Null).ToUpperInvariant()
        | Integer value ->
            parameter.TypeName <- (nameof Integer).ToUpperInvariant()
            parameter.Value <- value
        | Real value ->
            parameter.TypeName <- (nameof Real).ToUpperInvariant()
            parameter.Value <- value
        | Text value ->
            parameter.TypeName <- (nameof Text).ToUpperInvariant()
            parameter.Value <- value
        | Blob value ->
            parameter.TypeName <- (nameof Blob).ToUpperInvariant()
            parameter.Value <- value
        parameter

    static member Deps : SqliteDependencies =
        let beginTransactionAsync (connection: SQLiteConnection) (isolationLevel: IsolationLevel) _ =
            ValueTask.FromResult(connection.BeginTransaction(isolationLevel))

        let executeReaderAsync (command: SQLiteCommand) _ =
            Task.FromResult(command.ExecuteReader())

        { CreateCommand = fun connection -> connection.CreateCommand()
          SetCommandTransaction = fun command transaction -> command.Transaction <- transaction
          BeginTransactionAsync = beginTransactionAsync
          ExecuteReaderAsync = executeReaderAsync
          DbValueToParameter = Constants.DbValueToParameter }
