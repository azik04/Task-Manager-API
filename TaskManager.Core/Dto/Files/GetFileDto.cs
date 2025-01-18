namespace TaskManager.Core.Dto.Files;

public class GetFileDto
{
    public long Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public string FileName { get; set; }
    public long TaskId { get; set; }
}
