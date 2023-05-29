using System.Text;

namespace BulletinDotnetApi.Services;

public interface IAzureFunctionApiClient
{
    public Task<string> GetCleanedMessage(string message, CancellationToken cancellationToken);
}

public class AzureFunctionApiClient : IAzureFunctionApiClient
{
    private readonly HttpClient _httpClient;
    private readonly string _path;

    public AzureFunctionApiClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _path = $"api/{configuration.GetValue<string>("AzureFunctionName")}";
    }

    public async Task<string> GetCleanedMessage(string message, CancellationToken cancellationToken)
    {
        var response = await _httpClient.PostAsync(_path, new StringContent(message, Encoding.UTF8, "text/plain"),
            cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync(cancellationToken);
        }

        return message;
    }
}