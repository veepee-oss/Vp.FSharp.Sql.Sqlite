namespace Vp.FSharp.Sql.Sqlite

open System.Data.SQLite

open Vp.FSharp.Sql


/// Native SQLite DB types.
/// See https://www.sqlite.org/datatype3.html
type SqliteDbValue =
    | Null
    | Integer of int64
    | Real of double
    | Text of string
    | Blob of byte array

type SqliteCommandDefinition = CommandDefinition<SQLiteConnection, SQLiteParameter, SqliteDbValue>

[<RequireQualifiedAccess>]
module SqliteCommand =

    let private dbValueToParameter name value =
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

    /// Initialize a command definition with the given text contained in the given string.
    let text value : SqliteCommandDefinition =
        SqlCommand.text value

    /// Initialize a command definition with the given text spanning over several strings (ie. list).
    let textFromList value : SqliteCommandDefinition =
        SqlCommand.textFromList value

    /// Update the command definition with the given parameters.
    let parameters value (commandDefinition: SqliteCommandDefinition) : SqliteCommandDefinition =
        SqlCommand.parameters value commandDefinition

    /// Update the command definition with the given cancellation token.
    let cancellationToken value (commandDefinition: SqliteCommandDefinition) : SqliteCommandDefinition =
        SqlCommand.cancellationToken value commandDefinition

    /// Update the command definition with the given timeout.
    let timeout value (commandDefinition: SqliteCommandDefinition) : SqliteCommandDefinition =
        SqlCommand.timeout value commandDefinition

    /// Update the command definition and sets whether the command should be prepared or not.
    let prepare value (commandDefinition: SqliteCommandDefinition) : SqliteCommandDefinition =
        SqlCommand.prepare value commandDefinition

    /// Update the command definition and sets whether the command should be wrapped in the given transaction.
    let transaction value (commandDefinition: SqliteCommandDefinition) : SqliteCommandDefinition =
        SqlCommand.transaction value commandDefinition

    /// Return the sets of rows as an AsyncSeq accordingly to the command definition.
    let queryAsyncSeq connection read (commandDefinition: SqliteCommandDefinition) =
        SqlCommand.queryAsyncSeq connection dbValueToParameter read commandDefinition

    /// Return the sets of rows as a list accordingly to the command definition.
    let queryList connection read (commandDefinition: SqliteCommandDefinition) =
        SqlCommand.queryList connection dbValueToParameter read commandDefinition

    /// Return the first set of rows as a list accordingly to the command definition.
    let querySetList connection read (commandDefinition: SqliteCommandDefinition) =
        SqlCommand.querySetList connection dbValueToParameter read commandDefinition

    /// Return the 2 first sets of rows as a tuple of 2 lists accordingly to the command definition.
    let querySetList2 connection read1 read2 (commandDefinition: SqliteCommandDefinition) =
        SqlCommand.querySetList2 connection dbValueToParameter read1 read2 commandDefinition

    /// Return the 3 first sets of rows as a tuple of 3 lists accordingly to the command definition.
    let querySetList3 connection read1 read2 read3 (commandDefinition: SqliteCommandDefinition) =
        SqlCommand.querySetList3 connection dbValueToParameter read1 read2 read3 commandDefinition

    /// Execute the command accordingly to its definition and,
    /// - return the first cell value, if it is available and of the given type.
    /// - throw an exception, otherwise.
    let executeScalar<'Scalar> connection (commandDefinition: SqliteCommandDefinition) =
        SqlCommand.executeScalar<'Scalar, _, _, _>
            connection
            dbValueToParameter
            commandDefinition

    /// Execute the command accordingly to its definition and,
    /// - return Some, if the first cell is available and of the given type.
    /// - return None, if first cell is DbNull.
    /// - throw an exception, otherwise.
    let executeScalarOrNone<'Scalar> connection (commandDefinition: SqliteCommandDefinition) =
        SqlCommand.executeScalarOrNone<'Scalar, _, _, _>
            connection
            dbValueToParameter
            commandDefinition

    /// Execute the command accordingly to its definition and, return the number of rows affected.
    let executeNonQuery connection (commandDefinition: SqliteCommandDefinition) =
        SqlCommand.executeNonQuery connection dbValueToParameter commandDefinition
