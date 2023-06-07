using System.Net.Http.Headers;
using System.Text.Json;

namespace BulletinDotnetApi.Services;

public interface IJavaBackendApiClient
{
    Task<IEnumerable<JavaBackendMessageGetDto>> GetMessages(CancellationToken cancellationToken);
    Task<JavaBackendMessageGetDto?> PostMessage(string posterId, string message, CancellationToken cancellationToken);
}

public class JavaBackendApiClient : IJavaBackendApiClient
{
    private readonly HttpClient _httpClient;
    private readonly IAzureFunctionApiClient _azureFunctionApiClient;

    public JavaBackendApiClient(HttpClient httpClient, IAzureFunctionApiClient azureFunctionApiClient)
    {
        _httpClient = httpClient;
        _azureFunctionApiClient = azureFunctionApiClient;
    }

    public async Task<IEnumerable<JavaBackendMessageGetDto>> GetMessages(CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync("bulletinboardmessages", cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<JavaBackendMessageGetDto>>(cancellationToken: cancellationToken) ?? new List<JavaBackendMessageGetDto>(); 
        }

        return Array.Empty<JavaBackendMessageGetDto>();
    }

    public async Task<JavaBackendMessageGetDto?> PostMessage(string posterId, string message, CancellationToken cancellationToken)
    {
        var cleanedMessage = await _azureFunctionApiClient.GetCleanedMessage(message, cancellationToken);
        var content = JsonSerializer.Serialize(new JavaBackendMessagePostDto
        {
            PosterId = posterId,
            Message = cleanedMessage
        }, options: new JsonSerializerOptions(JsonSerializerDefaults.Web));
        var response = await _httpClient.PostAsync("bulletinboardmessages",
            new StringContent(content, new MediaTypeHeaderValue("application/json")), cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<JavaBackendMessageGetDto>(cancellationToken:cancellationToken);
        }

        return null;
    }
}