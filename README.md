# Vp.FSharp.Sql.Sqlite

The library providing specific native DB types definition for SQLite and
relying on [`System.Data.SQLite`](https://system.data.sqlite.org) and
providing a DB-specific module `Sqlite`.

## Slagging Hype

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

## NuGet Package

 Name                  | Version  | Command |
---------------------- | -------- | ------- |
 `Vp.FSharp.Sql.Sqlite | [![NuGet Status](http://img.shields.io/nuget/v/Vp.FSharp.Sql.Sqlite.svg)](https://www.nuget.org/packages/Vp.FSharp.Sql.Sqlite) | `Install-Package Vp.FSharp.Sql.Sqlite`

# How to Contribute
Bug reports, feature requests, and pull requests are very welcome! Please read the [Contribution Guidelines](./CONTRIBUTION.md) to get started.

# Licensing
The project is licensed under MIT. For more information on the license see the [license file](./LICENSE).

#### Notes
It has been decided to not rely on [`Microsoft.Data.Sqlite`](https://docs.microsoft.com/en-us/dotnet/standard/data/sqlite/?tabs=netcore-cli)
[due to its lack of support for `TransactionScope`](https://github.com/dotnet/efcore/issues/13825).

