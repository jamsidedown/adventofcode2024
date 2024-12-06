# Advent of Code 2024

C# solutions for Advent of Code 2024

## Setup

As with last year's solutions the solutions are part of a larger program, with command line arguments to run individual days or all days.

I'm using Jetbrains Rider as my IDE.

### Software versions

```shell
$ dotnet --version
9.0.100
```

## Running unit tests

```shell
$ dotnet test
```

## Running solutions

> Input files need to be placed into an `input` directory
> Naming convention is `day01.txt`, `day02.txt`, etc.

```shell
# to restore dependencies
$ dotnet restore

# to build the solution without running it
$ dotnet build

# to run the most recent solution
$ dotnet run --project AdventOfCode2024

# to run solutions for specific days
$ dotnet run --project AdventOfCode2024 1 2 3

# to run all solutions
$ dotnet run --project AdventOfCode2024 --all

# to run in release mode (with optimisations)
$ dotnet run -c Release --project AdventOfCode2024

# to run with server GC (for days when I dump too much on the SOH)
$ DOTNET_gcServer=1 dotnet run -c Release --project AdventOfCode2024 6
```
