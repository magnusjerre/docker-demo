using System.Net.Http.Headers;
using System.Text.Json;

namespace BulletinDotnetApi.Services;

public interface IJavaBackendBulletinBoardApiClient
{
    Task<IEnumerable<JavaBackendBulletinBoardMessageGetDto>> GetBulletinBoardMessages(CancellationToken cancellationToken);
    Task<JavaBackendBulletinBoardMessageGetDto?> PostBulletinBoardMessage(int posterId, string message, CancellationToken cancellationToken);
}

public class JavaBackendBulletinBoardApiClient : IJavaBackendBulletinBoardApiClient
{
    private readonly HttpClient _httpClient;

    public JavaBackendBulletinBoardApiClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<JavaBackendBulletinBoardMessageGetDto>> GetBulletinBoardMessages(CancellationToken cancellationToken)
    {
        var response = await _httpClient.GetAsync("bulletinboardmessages", cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<JavaBackendBulletinBoardMessageGetDto>>(cancellationToken: cancellationToken) ?? new List<JavaBackendBulletinBoardMessageGetDto>(); 
        }

        return Array.Empty<JavaBackendBulletinBoardMessageGetDto>();
    }

    public async Task<JavaBackendBulletinBoardMessageGetDto?> PostBulletinBoardMessage(int posterId, string message, CancellationToken cancellationToken)
    {
        var content = JsonSerializer.Serialize(new JavaBackendBulletinBoardMessagePostDto()
        {
            PosterId = posterId,
            Message = message
        }, options: new JsonSerializerOptions(JsonSerializerDefaults.Web));
        var response = await _httpClient.PostAsync("bulletinboardmessages",
            new StringContent(content, new MediaTypeHeaderValue("application/json")));
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<JavaBackendBulletinBoardMessageGetDto>(cancellationToken:cancellationToken);
        }

        return null;
    }
}