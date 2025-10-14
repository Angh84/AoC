using AoC.Core.Attributes;
using AoC.Core.Interfaces;
using AoC.Solutions.Utils;

namespace AoC.Solutions.Year2015;

[Solution(2015,2)]
public class Day02 : ISolution
{
    public string TestInput { get; }
    public string TestInputPartTwo => TestInput;
    public string ExpectedOutputPartOne { get; }
    public string ExpectedOutputPartTwo { get; }
    
    public string SolvePartOne(string input)
    {
        var presents = input.SplitLines();
        var requiredPaper = presents.Sum(CalculateArea);
        return requiredPaper.ToString(); //1586300
    }
    private static List<long> ParsePresent(string present) => present.ToStringArray('x')
        .Select(long.Parse)
        .ToList();
    private static long CalculateArea(string present)
    {
        var dimensions = ParsePresent(present);
        var a1 = dimensions[0] * dimensions[1];
        var a2 = dimensions[0] * dimensions[2];
        var a3 = dimensions[1] * dimensions[2];
        var a4 = Math.Min(Math.Min(a1,a2),a3);
        return 2 * (a1 + a2 + a3) + a4;
    }

    private static long CalculateBow(string present)
    {
        var dimensions = ParsePresent(present);
        return dimensions[0] * dimensions[1] * dimensions[2];
    }
    
    private static long CalculatePerimeter(string present)
    {
        var dimensions = ParsePresent(present)
            .Order()
            .ToList();
        var smallestPerimeter = 2 * (dimensions[0] + dimensions[1]);
        return smallestPerimeter;
    }

    public string SolvePartTwo(string input)
    {
        var presents = input.SplitLines();
        var requiredRibbon = presents.Sum(p => CalculateBow(p) + CalculatePerimeter(p));
        return requiredRibbon.ToString();
    }

}