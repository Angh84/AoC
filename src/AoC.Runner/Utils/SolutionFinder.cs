using System.Reflection;
using AoC.Core.Attributes;
using AoC.Core.Interfaces;

namespace AoC.Runner.Utils;

public static class SolutionFinder
{
    public static IEnumerable<(int Year, int Day, ISolution? Solution)> GetAllSolutions()
    {
        // Force-load the AoC.Solutions assembly
        const string assemblyName = "AoC.Solutions";
        if (AppDomain.CurrentDomain.GetAssemblies().All(a => a.GetName().Name != assemblyName))
            Assembly.Load(assemblyName);

        var solutionType = typeof(ISolution);   
        var res =  AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => solutionType.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .Select(t => {
                var instance = Activator.CreateInstance(t) as ISolution;
                var attr = t.GetCustomAttribute<SolutionAttribute>();
                return (Year: attr?.Year ?? 0, Day: attr?.Day ?? 0, Solution: instance);
            })
            .Where(s => s.Year > 0 && s.Day > 0);
        return res;
    }
}