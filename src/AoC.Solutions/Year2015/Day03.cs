using AoC.Core.Attributes;
using AoC.Core.Interfaces;

namespace AoC.Solutions.Year2015;

[Solution(2015, 3)]
public class Day03 : ISolution
{
    public string TestInput { get; } = "^>v<";
    public string ExpectedOutputPartOne { get; } = "4";
    public string ExpectedOutputPartTwo { get; } = "3";

    public string SolvePartOne(string input)
    {
        (int x, int y) currentLocation = (0, 0);
        HashSet<(int x, int y)> locations = [currentLocation];
        foreach (var currentInstruction in input)
        {
            currentLocation = Move(currentInstruction, currentLocation);

            locations.Add(currentLocation);
        }

        return locations.Count.ToString();
    }

    private static (int x, int y) Move(char currentInstruction, (int x, int y) currentLocation)
    {
        switch (currentInstruction)
        {
            case '^':
                currentLocation.y += 1;
                break;
            case 'v':
                currentLocation.y -= 1;
                break;
            case '>':
                currentLocation.x += 1;
                break;
            case '<':
                currentLocation.x -= 1;
                break;
        }

        return currentLocation;
    }

    public string SolvePartTwo(string input)
    {
        (int x, int y) santaLocation = (0, 0);
        (int x, int y) roboLocation = (0, 0);
        HashSet<(int x, int y)> locations = [santaLocation];
        var santaMove = true;
        foreach (var currentInstruction in input)
        {
            if (santaMove)
            {
                santaLocation = Move(currentInstruction, santaLocation);
                locations.Add(santaLocation);
            }
            else
            {
                roboLocation = Move(currentInstruction, roboLocation);
                locations.Add(roboLocation);
            }

            santaMove = !santaMove;
        }

        return locations.Count.ToString();
    }
}