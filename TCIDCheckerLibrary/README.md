# TC ID Checker for .NET

[![NuGet Version](https://img.shields.io/nuget/v/TCIDChecker_NET?&label=nuget&color=informational&logo=nuget)](https://www.nuget.org/packages/TCIDChecker_NET/)
[![NuGet Downloads](https://img.shields.io/nuget/dt/TCIDChecker_NET?color=brightgreen&logo=nuget)](https://www.nuget.org/packages/TCIDChecker_NET/)
[![License](https://img.shields.io/github/license/berkbb/tcid_checker_net?color=important)](https://www.nuget.org/packages/TCIDChecker_NET/)

Determines whether a given Turkish ID number is valid according to official checksum rules.

## Target Framework

- .NET 10 (`net10.0`)

## Features

- Determines that given TC ID is correct or wrong based on rules.
- If correct, you can use online validation functions.
- Generates valid random TC ID.

## How It Works

- The ones digit of the sum of the first 10 digits gives the 11th digit.
- The ones digit of 7 times the sum of the 1st, 3rd, 5th, 7th and 9th digits plus 9 times the sum of the 2nd, 4th, 6th and 8th digits gives the 10th digit.
- The ones digit of 8 times the sum of the 1st, 3rd, 5th, 7th and 9th digits gives the 11th digit.

## Usage

```csharp
using TCIDCheckerLibrary;
using ColorLoggerLibrary;

var checker = new TCIDChecker();

checker.controlID("08392566548", true, true, LogLevel.info);
checker.controlID("02345678982", false, true, LogLevel.verbose);
checker.generateID(false, false, LogLevel.info);
checker.generateID(false, true, LogLevel.info);
checker.generateID(true, true, LogLevel.info);
checker.generateID(true, false, LogLevel.info);

await checker.validateIDAsync("11111111111", "ali", "veli", 1900, false, LogLevel.verbose);
await checker.validateForeignIDAsync("11111111111", "jack", "delay", 1, 1, 1900, true, LogLevel.debug);
await checker.validatePersonAndCardAsync("11111111111", "ali", "veli", false, 1, false, 1, false, 1900, "a15", 796544, "y02n45764", true, LogLevel.info);
```
