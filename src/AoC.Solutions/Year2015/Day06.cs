using AoC.Core.Attributes;
using AoC.Core.Interfaces;
using AoC.Solutions.Utils;

namespace AoC.Solutions.Year2015;

[Solution(2015, 6)]
public class Day06 : ISolution
{
    public string SolvePartOne(string input)
    {
        var lights = ResetLightMatrix();
        lights = input
            .SplitLines()
            .Select(ParseInstruction)
            .Aggregate(lights, ExecuteInstruction);

        return lights.Values.Count(i => i).ToString();
    }

    private Dictionary<string, bool> ExecuteInstruction(Dictionary<string, bool> lights, LightInstruction instruction)
    {
        for (var x = instruction.FromPosition.x; x <= instruction.ToPosition.x; x++)
        {
            for (var y = instruction.FromPosition.y; y <= instruction.ToPosition.y; y++)
            {
                lights[x.ToString() + ',' + y] = instruction.Action switch
                {
                    LightAction.Toggle => !lights[x.ToString() + ',' + y],
                    LightAction.TurnOn => true,
                    LightAction.TurnOff => false,
                    _ => lights[x.ToString() + ',' + y]
                };
            }
        }

        return lights;
    }

    private Dictionary<string, bool> ResetLightMatrix()
    {
        var result = new Dictionary<string, bool>();
        for (var x = 0; x < 1000; x++)
        {
            for (var y = 0; y < 1000; y++)
            {
                var currentPosition = x.ToString() + ',' + y;
                result.TryAdd(currentPosition, false);
            }
        }

        return result;
    }

    private LightInstruction ParseInstruction(string line)
    {
        var parts = line.Split(' ');
        LightAction action;
        int startIdx;

        if (parts[0] == "toggle")
        {
            action = LightAction.Toggle;
            startIdx = 1;
        }
        else if (parts[1] == "on")
        {
            action = LightAction.TurnOn;
            startIdx = 2;
        }
        else
        {
            action = LightAction.TurnOff;
            startIdx = 2;
        }

        var fromCoords = parts[startIdx].Split(',');
        var toCoords = parts[startIdx + 2].Split(',');

        return new LightInstruction
        {
            Action = action,
            FromPosition = (int.Parse(fromCoords[0]), int.Parse(fromCoords[1])),
            ToPosition = (int.Parse(toCoords[0]), int.Parse(toCoords[1]))
        };
    }

    public string SolvePartTwo(string input)
    {
        return string.Empty;
    }

    public string TestInput { get; }
    public string TestInputPartTwo { get; }
    public string ExpectedOutputPartOne { get; }
    public string ExpectedOutputPartTwo { get; }
}

internal enum LightAction
{
    TurnOn,
    TurnOff,
    Toggle
}

internal class LightInstruction
{
    public LightAction Action { get; set; }
    public (int x, int y) FromPosition { get; set; }
    public (int x, int y) ToPosition { get; set; }
}