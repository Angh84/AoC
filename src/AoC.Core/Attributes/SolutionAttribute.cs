namespace AoC.Core.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SolutionAttribute(int year, int day) : Attribute
{
    public int Year { get; } = year;
    public int Day { get; } = day;
}