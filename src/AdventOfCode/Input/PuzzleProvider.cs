using System.Net;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using ReverseMarkdown;

namespace AdventOfCode.Input;

public static partial class PuzzleProvider
{
    private const string UserAgent = "github.com/Angh84/AoC by andreashansson84@gmail.com";

    public static string Dir(int year, int day) => Path.Combine(FindRoot(), "puzzles", $"{year}", $"{day:00}");

    public static string Input(int year, int day) => Read(Path.Combine(Dir(year, day), "input.txt"));

    public static string Example(int year, int day, int n = 1) =>
        Read(Path.Combine(Dir(year, day), n == 1 ? "example.txt" : $"example{n}.txt"));


    public static async Task Fetch(int year, int day, bool refreshText = false)
    {
        var dir = Dir(year, day);
        Directory.CreateDirectory(dir);
        var session = new ConfigurationBuilder().AddUserSecrets(typeof(PuzzleProvider).Assembly).Build()["AocSession"];

        using var http = new HttpClient();
        http.BaseAddress = new("https://adventofcode.com/");
        http.DefaultRequestHeaders.TryAddWithoutValidation("User-Agent", UserAgent);
        if (session != null)
            http.DefaultRequestHeaders.Add("Cookie", $"session={session}");

        var textPath = Path.Combine(dir, "puzzle.md");
        if (refreshText || !File.Exists(textPath))
        {
            var html = await http.GetStringAsync($"{year}/day/{day}");
            var articles = ArticleRegex().Matches(html).Select(m => m.Value).ToList();
            if (articles.Count == 0)
                throw new InvalidOperationException($"No puzzle text found for {year} day {day}");

            var converter = new Converter(new() { GithubFlavored = true, Tags = { Unknown = Config.UnknownTagsOption.Bypass } });
            await File.WriteAllTextAsync(textPath, string.Join("\n\n", articles.Select(converter.Convert)));

            var examplePath = Path.Combine(dir, "example.txt");
            var example = CodeBlockRegex().Match(articles[0]);
            if (!File.Exists(examplePath) && example.Success)
                await File.WriteAllTextAsync(examplePath, WebUtility.HtmlDecode(TagRegex().Replace(example.Groups[1].Value, "")));
        }

        var inputPath = Path.Combine(dir, "input.txt");
        if (!File.Exists(inputPath))
        {
            if (session == null)
                throw new InvalidOperationException("Missing session cookie. Run: dotnet user-secrets set AocSession <cookie> --project src/AdventOfCode");
            await File.WriteAllTextAsync(inputPath, await http.GetStringAsync($"{year}/day/{day}/input"));
        }
    }

    private static string Read(string path) => File.ReadAllText(path).TrimEnd('\n');

    private static string FindRoot()
    {
        var dir = new DirectoryInfo(AppContext.BaseDirectory);
        while (dir != null && !File.Exists(Path.Combine(dir.FullName, "AdventOfCode.slnx")))
            dir = dir.Parent;
        return dir?.FullName ?? throw new InvalidOperationException($"AdventOfCode.slnx not found above {AppContext.BaseDirectory}");
    }

    [GeneratedRegex("""<article class="day-desc">.*?</article>""", RegexOptions.Singleline)]
    private static partial Regex ArticleRegex();

    [GeneratedRegex("<pre><code>(.*?)</code></pre>", RegexOptions.Singleline)]
    private static partial Regex CodeBlockRegex();

    [GeneratedRegex("<.*?>")]
    private static partial Regex TagRegex();
}
