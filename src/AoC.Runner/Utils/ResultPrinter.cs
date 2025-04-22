using AoC.Core.Services;

namespace AoC.Runner.Utils;

public static class ResultPrinter
{
    public static void PrintResult(SolutionResult result)
    {
        if (!result.Success)
        {
            Console.WriteLine("Solution failed to run correctly.");
            if (result.TestsRun && !string.IsNullOrEmpty(result.TestError))
            {
                Console.WriteLine($"Test Error: {result.TestError}");
            }
            return;
        }

        if (result.TestsRun)
        {
            Console.WriteLine("Tests:");
            Console.WriteLine($"  Part 1: {(result.TestPassedPartOne ? "PASSED" : "FAILED")}");
            Console.WriteLine($"  Part 2: {(result.TestPassedPartTwo ? "PASSED" : "FAILED")}");
            Console.WriteLine();
        }

        Console.WriteLine("Results:");
        Console.WriteLine($"  Part 1: {result.PartOneAnswer} ({result.PartOneTime:F2}ms)");
        Console.WriteLine($"  Part 2: {result.PartTwoAnswer} ({result.PartTwoTime:F2}ms)");
        Console.WriteLine($"  Total Time: {result.PartOneTime + result.PartTwoTime:F2}ms");
    }

}