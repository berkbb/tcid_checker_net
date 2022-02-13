
# TC ID Checker for .NET. 

[![NuGet Version](https://img.shields.io/nuget/v/TCIDChecker_NET?&label=nuget&color=informational&logo=nuget)](https://www.nuget.org/packages/TCIDChecker_NET/) 
[![NuGet Downloads](https://img.shields.io/nuget/dt/TCIDChecker_NETcolor=brightgreen&?style=flat&logo=nuget)](https://www.nuget.org/packages/TCIDChecker_NET/)
[![License](https://img.shields.io/github/license/berkbb/tcid_checker_net?color=important)](https://www.nuget.org/packages/TCIDChecker_NET/)


   

 Determines that given TC ID is correct or wrong based on rules for .NET.

## Features

* Determines that given TC ID is correct or wrong based on rules.
* If correct, you can use online validation functions.
* Generates valid random TC ID.



## How to Work

* The ones digit of the sum of the first 10 digits gives the 11th digit.

* The ones digit of 7 times the sum of the 1st, 3rd, 5th, 7th and 9th digits plus 9 times the sum of the 2nd, 4th, 6th and 8th digits gives the 10th digit.

* The ones digit of 8 times the sum of the 1st, 3rd, 5th, 7th and 9th digits gives the 11th digit.

* A built-in control ID function in all validate functions with given credentials via Web API supplied by General Directorate of Population and Citizenship Affairs of the Republic of Turkey.
  
*  Generates valid random TC ID when you want.


## Usage
 

```c#
TCIDChecker checker = new TCIDChecker();  // New ID checker.


// bool r1 =
checker.controlID("08392566548", true, true); // Control ID. -- true


// bool r6 =
checker.controlID("02345678982", false, true); // Control ID. -- false

// String? r2 =
checker.generateID(false, false); // Generates valid random TC ID. -- random int.

// String? r8 =
checker.generateID(false, true); // Returns a print ready TC ID. -- 02345678982.

// String? r7 =
checker.generateID(true, true); // Returns a print ready TC ID. -- 02345678982.

// String? r9 =
checker.generateID(
    true, false); // Returns a valid fake TC ID start with 0. -- random int.


// bool r3 =
await checker.validateIDAsync("11111111111", "ali", "veli", 1900,
    false); // Validate ID from WEB API. -- false


// bool r4 =
await checker.validateForeignIDAsync("11111111111", "jack", "delay", 1, 1, 1900,
    true); // Validate foreign ID from WEB API. -- false

// bool r5 =
await checker.validatePersonAndCardAsync(
    "11111111111",
    "ali",
    "veli",
    false,
    1,
    false,
    1,
    false,
    1900,
    "a15",
    796544,
    "y02n45764",
    true); // Validate Person and Card ID from WEB API. -- false

//Print area.
// Console.WriteLine(r1);
// Console.WriteLine(r2);
// Console.WriteLine(r3);
// Console.WriteLine(r4);
// Console.WriteLine(r5);
// Console.WriteLine(r6);
// Console.WriteLine(r7);
// Console.WriteLine(r8);
// Console.WriteLine(r9);
```


