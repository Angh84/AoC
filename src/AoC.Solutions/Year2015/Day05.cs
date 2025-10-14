using AoC.Core.Attributes;
using AoC.Core.Interfaces;
using AoC.Solutions.Utils;

namespace AoC.Solutions.Year2015;

[Solution(2015, 5)]
public class Day05 : ISolution
{
    public string SolvePartOne(string input)
    {
        var rows = input.SplitLines();
        var niceStrings = rows.Count(IsNicePartOne);
        return niceStrings.ToString();
    }

    private static bool IsNicePartOne(string row)
    {
        HashSet<string> illegalCombos = ["ab", "cd", "pq", "xy"];
        var numberOfVowels = 0;
        var containsRepeatedChars = false;
        char? lastChar = null;
        foreach (var character in row)
        {
            if ("aeiou".Contains(character))
                numberOfVowels++;
            if (lastChar != null)
            {
                containsRepeatedChars |= character == lastChar;
                if (illegalCombos.Contains(lastChar.ToString() + character))
                    return false;
            }

            lastChar = character;
        }

        return numberOfVowels >= 3 && containsRepeatedChars;
    }

    public string SolvePartTwo(string input)
    {
        var rows = input.SplitLines();
        var niceStrings = rows.Count(IsNicePartTwo);
        return niceStrings.ToString();
    }

    private bool IsNicePartTwo(string row)
    {
        var hasRepeatingCharWithSpace = HasRepeatingCharWithSpace(row);
        var hasPairNotOverlapping = HasPairNotOverlapping(row);
        return hasRepeatingCharWithSpace && hasPairNotOverlapping;
    }

    private bool HasPairNotOverlapping(string row)
    {
        for (var i = 0; i < row.Length - 1; i++)
        {
            var currentPair = row.Substring(i, 2);
            var rest = row[(i + 2)..];
            if (rest.Contains(currentPair)) return true;
        }

        return false;
    }

    private bool HasRepeatingCharWithSpace(string row)
    {
        for (var i = 0; i < row.Length - 2; i++)
        {
            var firstChar = row[i];
            var secondChar = row[i + 2];
            if (firstChar == secondChar) return true;
        }

        return false;
    }

    public string TestInput { get; } = "ugknbfddgicrmopn\naaa\njchzalrnumimnmhp\nhaegwjzuvuyypxyu\ndvszwmarrgswjxmb";
    public string TestInputPartTwo => "qjhvhtzxzqqjkmpb\nxxyxx\nuurcxstgmygtbstg\nieodomkazucvgmuy";
    public string ExpectedOutputPartOne { get; } = "2";
    public string ExpectedOutputPartTwo { get; } = "2";
}