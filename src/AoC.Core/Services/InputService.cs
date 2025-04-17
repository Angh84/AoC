namespace AoC.Core.Services;

public class InputService
{
    private readonly string _fileBasePath;
    private readonly string _sessionToken;
    private readonly string _urlTemplate;
    private readonly HttpClient _httpClient;

    public InputService(string fileBasePath, string sessionToken, string urlTemplate)
    {
        _fileBasePath = fileBasePath;
        _sessionToken = sessionToken;
        _urlTemplate = urlTemplate;
        _httpClient = new HttpClient();
    }

    public async Task<string> GetInputForDay(int year, int day)
    {
        var inputPath = Path.Combine(_fileBasePath, year.ToString(), $"day{day:D2}.txt");

        // Check if the input file exists locally
        if (File.Exists(inputPath))
        {
            return await File.ReadAllTextAsync(inputPath);
        }

        // Download input if not available
        var input = await DownloadInputForDay(year, day);

        // Ensure directory exists
        Directory.CreateDirectory(Path.GetDirectoryName(inputPath) ?? throw new InvalidOperationException());

        // Save input for future use
        await File.WriteAllTextAsync(inputPath, input);

        return input;
    }

    private async Task<string> DownloadInputForDay(int year, int day)
    {
        var url = string.Format(_urlTemplate, year, day);

        _httpClient.DefaultRequestHeaders.Clear();
        _httpClient.DefaultRequestHeaders.Add("Cookie", $"session={_sessionToken}");

        var response = await _httpClient.GetAsync(url);
        response.EnsureSuccessStatusCode();

        return await response.Content.ReadAsStringAsync();
    }
}