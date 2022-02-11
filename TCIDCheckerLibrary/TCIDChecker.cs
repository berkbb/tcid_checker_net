using System.Text;

namespace TCIDCheckerLibrary;
public class TCIDChecker
{

    /// <summary>
    ///  Checks that Turkish ID number is correct or not.
    /// </summary>
    /// <param name="id">is TC ID</param>
    /// <param name="skipRealCitizen">is a key that controls not to create a real citizen ID. If it is true, ID will start 0, it is not correct on real life.</param>
    /// <param name="printLog"> enables / dsiables console log. If true, log will printed.</param>
    /// <returns>boolean.</returns>
    public bool controlID(string id, bool skipRealCitizen, bool printLog)
    {
        try
        {
            bool c1 = false;
            bool c2 = false;
            bool c3 = false;

            if (id.Length == 11 && skipRealCitizen == true
                ? true
                : id[0] != '0') // If length is 11 and first number is not equal 0.
            {
                int first10Sum = 0; // First 10 digit sum.

                for (int i = 0; i < 10; i++)
                {
                    first10Sum = first10Sum + int.Parse(id[i].ToString());
                }

                int sum1 = int.Parse(id[0].ToString()) +
                    int.Parse(id[2].ToString()) +
                    int.Parse(id[4].ToString()) +
                    int.Parse(id[6].ToString()) +
                    int.Parse(
                        id[8].ToString()); // Sum of numbers at 1., 3., 5., 7. and 9. positions.

                int multiply1 = sum1 * 7; // Multiply sum1 with 7.

                int sum2 = int.Parse(id[1].ToString()) +
                    int.Parse(id[3].ToString()) +
                    int.Parse(id[5].ToString()) +
                    int.Parse(id[7].ToString()); // Sum of numbers at 2., 4., 6. and 8. positions.

                int multiply2 = sum2 * 9; // Multiply sum2 with 9.

                int operation1 = first10Sum % 10; // mod10 of first10sum.

                int operation2 = (multiply1 + multiply2) %
                    10; // mod10 of Multiply multiply1 and multiply1.

                int operation3 = (sum1 * 8) % 10; // mod10 Multiply sum1 and 8.

                if (operation1 ==
                    int.Parse(id[10].ToString())) //If operation1 is equal to 11th digit of the ID.

                {
                    c1 = true;
                }

                if (operation2 ==
                    int.Parse(id[9].ToString())) //If operation2  is equal to 10th digit of the ID.

                {
                    c2 = true;
                }

                if (operation3 ==
                    int.Parse(
                        id[10].ToString())) //If operation3 mod 10 is equal to 11th digit of the ID.

                {
                    c3 = true;
                }
            }

            bool isValid = c1 & c2 & c3;

            if (printLog == true)
            {
                Console.WriteLine($"TC ID: {id} is {(isValid == true ? "correct" : "wrong")}");
            }
            return isValid;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while checking Turkish ID - {e.Message}");
            return false;
        }
    }

    /// <summary>
    /// Generates valid random TC ID.
    /// </summary>
    /// <param name="skipRealCitizen"> is a key that controls not to create a real citizen ID. If it is true, ID will start 0, it is not correct on real life.</param>
    /// <param name="computeFake">is a key that controls compute Fake TC ID or returns a Console.WriteLine ready value.</param>
    /// <returns>string?.</returns>
    public string? generateID(bool skipRealCitizen, bool computeFake)
    {
        try
        {
            var controlKey = false; // Control key for do -while.
            const int size = 11; // Size is final.
            Random random = new Random(); // Random Generator.
            var generatedNum = ""; // Generated number.

            if (skipRealCitizen == true &&
                computeFake == false) // Fake ID starts with 0.
            {
                Console.WriteLine(
                    "Generating fake TC ID starts with '0', so it is hard to find any match. So, the process takes a little longer. Please wait.");
                do
                {
                    var tcList = Enumerable.Repeat(0, size).ToList<int>();
                    // Fill the list with 0.

                    // Creator logic

                    for (var i = 1; i < size - 2; i++)
                    {
                        tcList[i] = random.Next(0, 10); // Generate number for index 2-11.
                    }

                    var c1o1 = tcList[1] + tcList[3] + tcList[5] + tcList[7];
                    var c1o2 =
                        (tcList[0] + tcList[2] + tcList[4] + tcList[6] + tcList[8]) * 7 -
                            c1o1;

                    tcList[9] = c1o2 % 10;

                    var c2o1 = (tcList[0] +
                        tcList[1] +
                        tcList[2] +
                        tcList[3] +
                        tcList[4] +
                        tcList[5] +
                        tcList[6] +
                        tcList[7] +
                        tcList[8] +
                        tcList[9]);

                    tcList[10] = c2o1 % 10;

                    //
                    var buffer = new StringBuilder(); // Stringbuilder for add int to a string.
                    tcList.ForEach(t => buffer.Append(t)); // Write to builder


                    var isValidInt = Int64.TryParse(buffer.ToString(), out long z);
                    var value = buffer.ToString().PadLeft(11, '0'); // Add 0 to left.

                    if (isValidInt == true &&
                        controlID(value, true, false) ==
                            true) // Not null means valid integer && ID is correct.
                    {
                        generatedNum = value; // Set number.
                        controlKey = true; // Stop loop.
                    }
                } while (controlKey == false);

                Console.WriteLine($"Generated valid fake TC ID is: {generatedNum}");
            }
            else if ((skipRealCitizen == false && computeFake == true) ||
              (skipRealCitizen == true && computeFake == true)) // Print Ready Fake ID
            {
                generatedNum = "02345678982";
                Console.WriteLine($"Print ready valid fake TC ID is: {generatedNum}");

            }
            else
            {
                do
                {
                    var tcList = Enumerable.Repeat(0, size).ToList<int>();
                    // Fill the list with 0.

                    // Creator logic

                    tcList[0] = random.Next(1, 10); // from 1 upto 9 included. First num must not be zero.

                    for (var i = 1; i < size; i++)
                    {
                        tcList[i] = random.Next(0, 10); // Generate number for index 2-9.
                    }

                    //
                    var buffer = new StringBuilder(); // Stringbuilder for add int to a string.
                    tcList.ForEach(t => buffer.Append(t)); // Write to builder


                    var isValidInt = Int64.TryParse(buffer.ToString(), out long u);
                    var value = buffer.ToString();

                    if (isValidInt == true &&
                    controlID(value, skipRealCitizen,
                            false) == true) // Not null means valid integer && ID is correct.
                    {
                        generatedNum = value; // Set number.
                        controlKey = true; // Stop loop.
                    }

                } while (controlKey == false);
                Console.WriteLine($"Generated valid random TC ID is: {generatedNum!}");
            }

            return generatedNum;
        }
        catch (Exception e)
        {
            Console.WriteLine($"An error occurred while generating valid Turkish ID - {e.Message}");
            return null;
        }
    }

}

