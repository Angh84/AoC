using AoC.Core.Attributes;
using AoC.Core.Interfaces;
using AoC.Solutions.Utils;

namespace AoC.Solutions.Year2015;

[Solution(2015, 6)]
public class Day06 : ISolution
{
    public string SolvePartOne(string input)
    {
        Func<bool, LightAction, bool> actionToExecute = (current, action) => action switch
        {
            LightAction.Toggle => !current,
            LightAction.TurnOn => true,
            LightAction.TurnOff => false,
            _ => current
        };
        var lights = ResetLightMatrix<bool>();
        lights = input
            .SplitLines()
            .Select(ParseInstruction)
            .Aggregate(lights, (grid, instruction) =>
                ExecuteInstructionOnGrid(grid, instruction, actionToExecute));

        return lights.Values.Count(i => i).ToString();
    }

    public string SolvePartTwo(string input)
    {
        Func<long, LightAction, long> actionToExecute = (current, action) => action switch
        {
            LightAction.Toggle => current + 2,
            LightAction.TurnOn => current + 1,
            LightAction.TurnOff => Math.Max(current - 1, 0),
            _ => current
        };
        var lights = ResetLightMatrix<long>();
        var sum = input
            .SplitLines()
            .Select(ParseInstruction)
            .Aggregate(lights, (grid, instruction) => ExecuteInstructionOnGrid(grid, instruction, actionToExecute))
            .Sum(kvp => kvp.Value);

        return sum.ToString();
    }

    private static Dictionary<(int, int), TValue> ExecuteInstructionOnGrid<TValue>(
        Dictionary<(int, int), TValue> lights,
        LightInstruction instruction,
        Func<TValue, LightAction, TValue> applyAction)
    {
        for (var x = instruction.FromPosition.x; x <= instruction.ToPosition.x; x++)
        {
            for (var y = instruction.FromPosition.y; y <= instruction.ToPosition.y; y++)
            {
                lights[(x, y)] = applyAction(lights[(x, y)], instruction.Action);
            }
        }

        return lights;
    }

    private static Dictionary<(int, int), T> ResetLightMatrix<T>() where T : struct
    {
        var result = new Dictionary<(int, int), T>();
        for (var x = 0; x < 1000; x++)
        {
            for (var y = 0; y < 1000; y++)
            {
                result.TryAdd((x, y), default);
            }
        }

        return result;
    }

    private static LightInstruction ParseInstruction(string line)
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