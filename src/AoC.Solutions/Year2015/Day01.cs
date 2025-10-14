using AoC.Core.Attributes;
using AoC.Core.Interfaces;

namespace AoC.Solutions.Year2015;

[Solution(2015,1)]
public class Day01: ISolution
{
    public string SolvePartOne(string input)
    {
        var currentFloor = input.Sum(c => c == '(' ? 1 : -1);
        return currentFloor.ToString();
    }

    public string SolvePartTwo(string input)
    {
        var currentFloor = 0;
        for (var i = 0; i<input.Length; i++)
        {
            var currentChar = input[i];
            currentFloor += currentChar == '(' ? 1 : -1;
            if (currentFloor == -1)
            {
                return (i + 1).ToString();
            }
        }

        return "No solution found";
    }

    public string TestInput => "())";
    public string TestInputPartTwo => TestInput;
    public string ExpectedOutputPartOne => "-1";
    public string ExpectedOutputPartTwo => "3";
}