namespace CustomExtensions
{


    /// <summary>
    /// String extension class.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Parse boolean to string.
        /// </summary>
        /// <param name="str">String value of boolean.</param>
        /// <returns>boolean.</returns>
        /// <exception cref="FormatException">If str is not a bool string.</exception>
        public static bool parseBool(this string str)
        {
            try
            {

                if (str.ToLower() == "true" || str.ToLower() == "false")
                {
                    return (str.ToLower() == "true" ? true : false);
                }
                else
                {
                    throw new FormatException();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"{str} can not be parsed to boolean -{e.Message}");
                return false;
            }
        }
    }
}