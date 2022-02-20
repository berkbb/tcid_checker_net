// See https://aka.ms/new-console-template for more information
using TCIDCheckerLibrary;
using ColorLoggerLibrary;

TCIDChecker checker = new TCIDChecker();  // New ID checker.


// bool r1 =
checker.controlID("08392566548", true, true, LogLevel.info); // Control ID. -- true


// bool r6 =
checker.controlID("02345678982", false, true, LogLevel.verbose); // Control ID. -- false

// String? r2 =
checker.generateID(false, false, LogLevel.info); // Generates valid random TC ID. -- random int.

// String? r8 =
checker.generateID(false, true, LogLevel.info); // Returns a print ready TC ID. -- 02345678982.

// String? r7 =
checker.generateID(true, true, LogLevel.info); // Returns a print ready TC ID. -- 02345678982.

// String? r9 =
checker.generateID(
    true, false, LogLevel.info); // Returns a valid fake TC ID start with 0. -- random int.


// bool r3 =
await checker.validateIDAsync("11111111111", "ali", "veli", 1900,
    false, LogLevel.verbose); // Validate ID from WEB API. -- false


// bool r4 =
await checker.validateForeignIDAsync("11111111111", "jack", "delay", 1, 1, 1900,
    true, LogLevel.debug); // Validate foreign ID from WEB API. -- false

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
    true, LogLevel.info); // Validate Person and Card ID from WEB API. -- false

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