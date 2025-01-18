namespace TaskManager.Core.Dto.Comment;

public class CreateCommentDto
{
    public string Message { get; set; }
    public long UserId { get; set; }
    public long TaskId { get; set; }
}
