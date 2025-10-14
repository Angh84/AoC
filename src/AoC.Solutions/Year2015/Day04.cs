using System.Security.Cryptography;
using System.Text;
using AoC.Core.Attributes;
using AoC.Core.Interfaces;

namespace AoC.Solutions.Year2015;

[Solution(2015, 4)]
public class Day04 : ISolution
{
    public string SolvePartOne(string input)
    {
        long seed = 1;
        while (true)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input + seed);
            var hashBytes = MD5.HashData(inputBytes);
            var hash = Convert.ToHexString(hashBytes);
            if (hash.StartsWith("00000"))
                return seed.ToString();
            seed++;
        }
    }

    public string SolvePartTwo(string input)
    {
        long seed = 1;
        while (true)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input + seed);
            var hashBytes = MD5.HashData(inputBytes);
            var hash = Convert.ToHexString(hashBytes);
            if (hash.StartsWith("000000"))
                return seed.ToString();
            seed++;
        }
    }

    public string TestInput { get; } = "abcdef";
    public string TestInputPartTwo => TestInput;
    public string ExpectedOutputPartOne { get; } = "609043";
    public string ExpectedOutputPartTwo { get; } = "6742839";
}