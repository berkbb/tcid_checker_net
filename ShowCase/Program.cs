// See https://aka.ms/new-console-template for more information
using TCIDCheckerLibrary;

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