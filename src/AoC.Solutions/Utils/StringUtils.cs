namespace AoC.Solutions.Utils;

public static class StringUtils
{
    public static string[] ToStringArray(this string str, char separator)
    {
        return str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
    }

    public static string[] SplitLines(this string str)
    {
        return str.ToStringArray('\n');
    }
}