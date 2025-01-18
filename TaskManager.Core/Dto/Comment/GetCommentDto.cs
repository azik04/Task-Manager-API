namespace TaskManager.Core.Dto.Comment;

public class GetCommentDto
{
    public long Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public string Message { get; set; }
    public long UserId { get; set; }
    public long TaskId { get; set; }
}
