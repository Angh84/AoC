using AoC.Core.Interfaces;
using AoC.Core.Utils;

namespace AoC.Core.Services
{
    public class SolutionRunner
    {
        private readonly InputService _inputService;

        public SolutionRunner(InputService inputService)
        {
            _inputService = inputService;
        }

        public async Task<SolutionResult> RunSolution(ISolution solution, int year, int day, bool runTests = true)
        {
            var result = new SolutionResult { Year = year, Day = day };

            // Run tests if available and requested
            if (runTests && !string.IsNullOrEmpty(solution.TestInput))
            {
                var testsPass = RunTests(solution, result);
                if (!testsPass)
                {
                    result.Success = false;
                    return result;
                }
            }

            // Get input
            var input = await _inputService.GetInputForDay(year, day);

            // Run and time Part One
            var tracker = new PerformanceTracker();
            tracker.Start();
            result.PartOneAnswer = solution.SolvePartOne(input);
            tracker.Stop();
            result.PartOneTime = tracker.ElapsedMilliseconds;

            // Run and time Part Two
            tracker.Reset();
            tracker.Start();
            result.PartTwoAnswer = solution.SolvePartTwo(input);
            tracker.Stop();
            result.PartTwoTime = tracker.ElapsedMilliseconds;

            result.Success = true;
            return result;
        }

        private static bool RunTests(ISolution solution, SolutionResult result)
        {
            try
            {
                var testPartOne = solution.SolvePartOne(solution.TestInput);
                var partOneCorrect = testPartOne == solution.ExpectedOutputPartOne;
                
                result.TestsRun = true;
                result.TestPassedPartOne = partOneCorrect;

                // Only run part two test if part one passes and expected output exists
                if (partOneCorrect && !string.IsNullOrEmpty(solution.ExpectedOutputPartTwo))
                {
                    var testPartTwo = solution.SolvePartTwo(solution.TestInput);
                    result.TestPassedPartTwo = testPartTwo == solution.ExpectedOutputPartTwo;
                    return result is { TestPassedPartOne: true, TestPassedPartTwo: true };
                }

                return partOneCorrect;
            }
            catch (Exception ex)
            {
                result.TestsRun = true;
                result.TestError = ex.Message;
                return false;
            }
        }
    }

    public class SolutionResult
    {
        public int Year { get; set; }
        public int Day { get; set; }
        public bool Success { get; set; }
        
        public string PartOneAnswer { get; set; }
        public string PartTwoAnswer { get; set; }
        
        public double PartOneTime { get; set; }
        public double PartTwoTime { get; set; }
        
        public bool TestsRun { get; set; }
        public bool TestPassedPartOne { get; set; }
        public bool TestPassedPartTwo { get; set; }
        public string TestError { get; set; }
    }
}