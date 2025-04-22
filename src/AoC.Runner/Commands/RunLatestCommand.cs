using AoC.Core.Services;
using AoC.Runner.Utils;

namespace AoC.Runner.Commands;

public class RunLatestCommand
{
    private readonly SolutionRunner _runner;

    public RunLatestCommand(SolutionRunner runner)
    {
        _runner = runner;
    }
    
    public async Task Execute()
    {
        // Find the latest solution by year and day
        var latestSolution = SolutionFinder.GetAllSolutions()
            .OrderByDescending(s => s.Year)
            .ThenByDescending(s => s.Day)
            .FirstOrDefault();

        if (latestSolution.Year != 0 && latestSolution.Day != 0 && latestSolution.Solution != null)
        {
            Console.WriteLine($"Running latest solution: Year {latestSolution.Year}, Day {latestSolution.Day}");
            var result = await _runner.RunSolution(latestSolution.Solution, latestSolution.Year, latestSolution.Day);
            ResultPrinter.PrintResult(result);
        }
        else
        {
            Console.WriteLine("No solutions found.");
        }
    }


}