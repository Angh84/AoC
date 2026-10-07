using System.Diagnostics;
using AdventOfCode;
using AdventOfCode.Input;

var now = DateTime.UtcNow.AddHours(-5);
var fetch = args.FirstOrDefault() == "fetch";
var rest = fetch ? args[1..] : args;

if (rest.Length == 0 && now.Month != 12)
{
    Console.WriteLine("Usage: dotnet run -- [fetch] <day> [year]");
    return 1;
}

var day = rest.Length > 0 ? int.Parse(rest[0]) : now.Day;
var year = rest.Length > 1 ? int.Parse(rest[1]) : now.Month == 12 ? now.Year : now.Year - 1;

try
{
    await PuzzleProvider.Fetch(year, day, refreshText: fetch);
}
catch (Exception e) when (e is HttpRequestException or InvalidOperationException)
{
    Console.Error.WriteLine(e.Message);
    return 1;
}

if (fetch)
{
    Console.WriteLine($"Fetched {PuzzleProvider.Dir(year, day)}");
    return 0;
}

var type = Type.GetType($"AdventOfCode.Days.Y{year}.Day{day:00}");
if (type == null)
{
    Console.WriteLine($"No solution yet - create Days/Y{year}/Day{day:00}.cs");
    Console.WriteLine($"Puzzle text: {Path.Combine(PuzzleProvider.Dir(year, day), "puzzle.md")}");
    return 0;
}

var solution = (IDay)Activator.CreateInstance(type)!;
var input = PuzzleProvider.Input(year, day);
Run("Part 1", () => solution.Part1(input));
Run("Part 2", () => solution.Part2(input));
return 0;

static void Run(string name, Func<object> part)
{
    var sw = Stopwatch.StartNew();
    try
    {
        var answer = part();
        Console.WriteLine($"{name}: {answer} ({sw.Elapsed.TotalMilliseconds:F1} ms)");
    }
    catch (NotImplementedException)
    {
        Console.WriteLine($"{name}: not implemented");
    }
}
