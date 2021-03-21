# `Vp.FSharp.Sql.Sqlite`

The library providing specific native DB types definition for SQLite and
relying on [`System.Data.SQLite`](https://system.data.sqlite.org) and providing a DB-specific module `Sqlite`.

📝 Note: It has been decided to not rely on [`Microsoft.Data.Sqlite`](https://docs.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)
[due to its lack of support for `TransactionScope`](https://github.com/dotnet/efcore/issues/13825).

# ✨ Slagging Hype

We aim at following highly controversial practices to the best of our ability!

Status | Package                
------ | ----------------------
OK     | [![Conventional Commits](https://img.shields.io/badge/Conventional%20Commits-1.0.0-green.svg)](https://conventionalcommits.org)
OK     | [![semver](https://img.shields.io/badge/semver-2.0.0-green)](https://semver.org/spec/v2.0.0.html)
TBD    | [![keep a changelog](https://img.shields.io/badge/keep%20a%20changelog-1.0.0-red)](https://keepachangelog.com/en/1.0.0)
TBD    | [![Semantic Release](https://img.shields.io/badge/Semantic%20Release-17.1.1-red)](https://semantic-release.gitbook.io/semantic-release)

[Conventional Commits]: https://conventionalcommits.org
[semver]: https://img.shields.io/badge/semver-2.0.0-blue
[Semantic Release]: https://semantic-release.gitbook.io/semantic-release
[keep a changelog]: https://keepachangelog.com/en/1.0.0

# 📦 NuGet Package

 Name                  | Version  | Command |
---------------------- | -------- | ------- |
 `Vp.FSharp.Sql.Sqlite | [![NuGet Status](http://img.shields.io/nuget/v/Vp.FSharp.Sql.Sqlite.svg)](https://www.nuget.org/packages/Vp.FSharp.Sql.Sqlite) | `Install-Package Vp.FSharp.Sql.Sqlite`

# 📚 How to Use

## 🎓 Example 1

```fsharp
open Vp.FSharp.Sql.Sqlite


async {
    use connection = new SQLiteConnection("Data Source=:memory:")

    let readFirstSet _ (read: SqlRecordReader<_>) = read.Value<int64> 0
    let readSecondSet _ (read: SqlRecordReader<_>) = read.Value<double> 0
    let readThirdSet _ (read: SqlRecordReader<_>) = read.Value<string> 0

    let! sets = 
        SqliteCommand.text "SELECT @a; SELECT @b; SELECT @c;"
        |> SqliteCommand.parameters
            [ "a", SqliteDbValue.Integer 32L 
              "b", SqliteDbValue.Real 32.32
              "c", SqliteDbValue.Text "Meow" ]
        |> SqliteCommand.timeout (TimeSpan.FromMilliseconds 5.2)
        |> SqliteCommand.queryList3 connection readFirstSet readSecondSet readThirdSet 
    
    printfn "Sets = %A" sets
}
```

## 🎓 Example 2

```fsharp
open Vp.FSharp.Sql.Sqlite


async {
    use connection = new SQLiteConnection("Data Source=:memory:")

}

```

## 💿 Supported Database Values

Just a little FYI:

```fsharp
/// Native SQLite DB types.
/// See https://www.sqlite.org/datatype3.html
type SqliteDbValue =
    | Null
    | Integer of int64
    | Real of double
    | Text of string
    | Blob of byte array
```

## 🧱`SqliteCommand`

The main module is here to help you build and execute SQL(ite) commands (i.e. `SQLiteCommand` BTS).
    
### 🏗️ Command Construction

We are obviously going to talk about how to build the SQLite commands. 

📝 Note: the meaning of the word "update" below has to be put in a F# perspective, i.e. **immutable** update, as in the update returns a new updated and immutable instance.

#### `text`

Initialize a new command definition with the given text contained in the given string.

#### `textFromList`

Initialize a new command definition with the given text spanning over several strings (ie. list).

#### `noLogger`

Update the command definition so that when executing the command, it doesn't use any logger.
Be it the default one (Global, if any.) or a previously overriden one.

#### `overrideLogger`

Update the command definition so that when executing the command, it use the given overriding logger.
instead of the default one, aka the Global logger, if any.

#### `parameters`

Update the command definition with the given parameters.

#### `cancellationToken`

Update the command definition with the given cancellation token.

This comes in handy when you need to interop with more traditional, C#-async, cancellation style. 

#### `timeout`

Update the command definition with the given timeout.

#### `prepare`

Update the command definition and sets whether the command should be prepared or not.

As per [MS Docs](https://docs.microsoft.com/en-us/sql/ado/referento%20have%20the%20provider%20save%20a%20prepared%20(or%20compiled)%20version%20of%20the%20query%20specified%20in%20the%20CommandText%20property%20before%20a%20Command%20object's%20first%20execution.%20This%20may%20slow%20a%20command's%20first%20execution,%20but%20once%20the%20provider%20compiles%20a%20command,%20the%20provider%20will%20use%20the%20compiled%20version%20of%20the%20command%20for%20any%20subsequent%20executions,%20which%20will%20result%20in%20improved%20performance.e/ado-api/prepared-property-ado):

If supported,
> Use the `Prepared` property to have the provider save a prepared (or compiled) version 
> of the query specified in the CommandText property before a Command object's first 
> execution. This may slow a command's first execution, but once the provider compiles 
> a command, the provider will use the compiled version of the command for any subsequent
> executions, which will result in improved performance.

#### `transaction`

Update the command definition and sets whether the command should be wrapped in the given transaction.

### ⚙ Command Execution

We are obviously going to talk about how to build the SQLite commands.

#### `queryAsyncSeq`

Execute the command and return the sets of rows as an AsyncSeq accordingly to the command definition.

Example:

```fsharp
let example = 42
```

#### `queryList`

Execute the command and return the sets of rows as a list accordingly to the command definition.

Example:

```fsharp
let example = 42
```

#### `querySetList`

Execute the command and return the first set of rows as a list accordingly to the command definition.

Example:

```fsharp
let example = 42
```

#### `querySetList2`

Execute the command and return the 2 first sets of rows as a tuple of 2 lists accordingly to the command definition.

Example:

```fsharp
let example = 42
```

#### `querySetList3`

Execute the command and return the 3 first sets of rows as a tuple of 3 lists accordingly to the command definition.

Example:

```fsharp
let example = 42
```

#### `executeScalar<'Scalar>`

Execute the command accordingly to its definition and,
- return the first cell value, if it is available and of the given type.
- throw an exception, otherwise.

Example:

```fsharp
let example = 42
```

#### `executeScalarOrNone<'Scalar>`

Execute the command accordingly to its definition and,
- return `Some`, if the first cell is available and of the given type.
- return `None`, if first cell is DbNull.
- throw an exception, otherwise.

Example:

```fsharp
let example = 42
```

#### `executeNonQuery`

Execute the command accordingly to its definition and, return the number of rows affected.

Example:

```fsharp
let example = 42
```

### 🦮 Null Helpers

#### `ifNone`

Return SQLite DB Null value if the given option is `None`, otherwise the underlying wrapped in `Some`.

#### `ifError`

Return SQLite DB Null value if the given option is `Error`, otherwise the underlying wrapped in `Ok`.

### 🚄 Transaction Helpers

# ❤ How to Contribute

Bug reports, feature requests, and pull requests are very welcome! Please read the [Contribution Guidelines](./CONTRIBUTION.md) to get started.

# 📜 Licensing
The project is licensed under MIT. For more information on the license see the [license file](./LICENSE).
