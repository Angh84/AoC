using AoC.Core.Services;
using AoC.Runner.Utils;

namespace AoC.Runner.Commands;

public class RunBenchmarkCommand
{
    private readonly SolutionRunner _runner;
    private readonly int _year;

    public RunBenchmarkCommand(SolutionRunner runner, int year)
    {
        _runner = runner;
        _year = year;
    }
    public async Task Execute()
    {
        var solutions = SolutionFinder.GetAllSolutions()
            .Where(s => s.Year == _year)
            .OrderBy(s => s.Day)
            .ToList();

        if (solutions.Count == 0)
        {
            Console.WriteLine($"No solutions found for year {_year}");
            return;
        }

        Console.WriteLine($"Running benchmark for Year {_year}");
        Console.WriteLine("Day | Part 1 | Part 2 | Total");
        Console.WriteLine("----|----------|----------|----------");

        foreach (var solution in solutions)
        {
            var result = await _runner.RunSolution(solution.Solution!, _year, solution.Day, false);
            Console.WriteLine($"{solution.Day,3:D2} | {result.PartOneTime,6:F2}ms | {result.PartTwoTime,6:F2}ms | {result.PartOneTime + result.PartTwoTime,6:F2}ms");
        }
    }

}