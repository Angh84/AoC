namespace AoC.Core.Interfaces;

public interface ISolution
{
    string SolvePartOne(string input);
    string SolvePartTwo(string input);
    
    string TestInput { get; }
    string ExpectedOutputPartOne { get; }
    string ExpectedOutputPartTwo { get; }
}