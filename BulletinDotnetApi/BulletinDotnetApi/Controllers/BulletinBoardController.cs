using BulletinDotnetApi.Controllers.Dto;
using BulletinDotnetApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulletinDotnetApi.Controllers;

[ApiController]
[Route("bulletinboard")]
public class BulletinBoardController : ControllerBase
{
    private readonly IJavaBackendBulletinBoardApiClient _apiClient;

    public BulletinBoardController(IJavaBackendBulletinBoardApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IEnumerable<BulletinBoardMessageGetDto>> Get(CancellationToken cancellationToken)
    {
        return (await _apiClient.GetBulletinBoardMessages(cancellationToken)).Select(m => new BulletinBoardMessageGetDto()
        {
            Id = m.Id,
            PosterId = m.PosterId,
            PosterName = "TempName",
            Message = m.Message
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<BulletinBoardMessageGetDto?> Post([FromForm(Name = "message")] string message, CancellationToken cancellationToken)
    {
        var response = await _apiClient.PostBulletinBoardMessage(1, message, cancellationToken);
        return response == null
            ? null
            : new BulletinBoardMessageGetDto()
            {
                Id = response.Id,
                PosterId = response.PosterId,
                PosterName = "TempName",
                Message = response.Message
            };
    }

    [HttpGet("yo")]
    public async Task<string> GetString()
    {
        return "yo yo";
    }
}