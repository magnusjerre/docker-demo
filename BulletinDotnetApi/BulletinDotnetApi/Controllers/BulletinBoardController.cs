using System.IdentityModel.Tokens.Jwt;
using BulletinDotnetApi.Controllers.Dto;
using BulletinDotnetApi.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BulletinDotnetApi.Controllers;

[ApiController]
[Route("bulletinboard")]
public class BulletinBoardController : ControllerBase
{
    private readonly IJavaBackendApiClient _apiClient;

    public BulletinBoardController(IJavaBackendApiClient apiClient)
    {
        _apiClient = apiClient;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IEnumerable<MessageGetDto>> Get(CancellationToken cancellationToken)
    {
        return (await _apiClient.GetMessages(cancellationToken)).Select(m => new MessageGetDto
        {
            Id = m.Id,
            PosterId = m.PosterId,
            Message = m.Message
        });
    }

    [HttpPost]
    [Authorize]
    public async Task<MessageGetDto?> Post([FromForm(Name = "message")] string message, CancellationToken cancellationToken)
    {
        var token = await HttpContext.GetTokenAsync("access_token");
        var subject = new JwtSecurityTokenHandler().ReadJwtToken(token).Subject;
        var response = await _apiClient.PostMessage(subject ?? "default", message, cancellationToken);
        return response == null
            ? null
            : new MessageGetDto
            {
                Id = response.Id,
                PosterId = response.PosterId,
                Message = response.Message
            };
    }
}