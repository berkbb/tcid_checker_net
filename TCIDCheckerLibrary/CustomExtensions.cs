namespace CustomExtensions;

/// <summary>
/// String extension class.
/// </summary>
public static class StringExtension
{
    /// <summary>
    /// Parse string to boolean.
    /// </summary>
    /// <param name="str">String value of boolean.</param>
    /// <returns>Boolean value.</returns>
    /// <exception cref="FormatException">If <paramref name="str"/> is not a boolean string.</exception>
    public static bool ParseBool(this string str)
    {
        ArgumentNullException.ThrowIfNull(str);

        if (bool.TryParse(str, out var value))
        {
            return value;
        }

        throw new FormatException($"'{str}' cannot be parsed to boolean.");
    }

    // Backward compatible alias for older consumers.
    public static bool parseBool(this string str) => ParseBool(str);
}
