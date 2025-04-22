using AoC.Runner.Configuration;
using AoC.Core.Services;
using AoC.Runner.Commands;

// Initialize services
var appSettings = AppSettings.Load();
var inputService = new InputService(appSettings.InputsBasePath, appSettings.SessionToken,
    appSettings.InputUrlTemplate);
var solutionRunner = new SolutionRunner(inputService);

if (args.Length == 0 || args[0].Equals("latest", StringComparison.CurrentCultureIgnoreCase))
{
    var latestCommand = new RunLatestCommand(solutionRunner);
    await latestCommand.Execute();
}
else switch (args.Length)
{
    case >= 3 when args[0].Equals("run", StringComparison.CurrentCultureIgnoreCase):
    {
        var year = int.Parse(args[1]);
        var day = int.Parse(args[2]);
        var specificCommand = new RunSpecificCommand(solutionRunner, year, day);
        await specificCommand.Execute();
        break;
    }
    case >= 2 when args[0].Equals("benchmark", StringComparison.CurrentCultureIgnoreCase):
    {
        var year = args.Length >= 2 ? int.Parse(args[1]) : DateTime.Now.Year;
        var benchmarkCommand = new RunBenchmarkCommand(solutionRunner, year);
        await benchmarkCommand.Execute();
        break;
    }
    default:
        ShowHelp();
        break;
}

return;

static void ShowHelp()
{
    Console.WriteLine("Advent of Code Runner");
    Console.WriteLine("Usage:");
    Console.WriteLine("  latest              - Run the latest solution");
    Console.WriteLine("  run <year> <day>    - Run a specific solution");
    Console.WriteLine("  benchmark [year]    - Run benchmark for a year (defaults to current year)");
}