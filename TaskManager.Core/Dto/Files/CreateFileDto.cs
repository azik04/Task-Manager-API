using Microsoft.AspNetCore.Http;

namespace TaskManager.Core.Dto.Files;

public class CreateFileDto
{
    public IFormFile File { get; set; }
    public long TaskId { get; set; }
}
