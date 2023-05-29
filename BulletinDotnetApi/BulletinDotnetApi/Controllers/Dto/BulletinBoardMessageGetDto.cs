namespace BulletinDotnetApi.Controllers.Dto;

public class BulletinBoardMessageGetDto
{
    public int Id { get; set; }
    public int PosterId { get; set; }
    public string PosterName { get; set; }
    public string Message { get; set; }
}