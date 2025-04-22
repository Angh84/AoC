using AoC.Core.Services;
using AoC.Runner.Utils;

namespace AoC.Runner.Commands;

public class RunSpecificCommand
{
    private readonly SolutionRunner _runner;
    private readonly int _year;
    private readonly int _day;

    public RunSpecificCommand(SolutionRunner runner, int year, int day)
    {
        _runner = runner;
        _year = year;
        _day = day;
    }
    public async Task Execute()
    {
        var solution = SolutionFinder.GetAllSolutions()
            .FirstOrDefault(s => s.Year == _year && s.Day == _day);

        if (solution.Solution != null)
        {
            Console.WriteLine($"Running solution for Year {_year}, Day {_day}");
            var result = await _runner.RunSolution(solution.Solution, _year, _day);
            ResultPrinter.PrintResult(result);
        }
        else
        {
            Console.WriteLine($"No solution found for Year {_year}, Day {_day}");
        }
    }

}