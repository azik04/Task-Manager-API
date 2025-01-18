namespace TaskManager.Core.Dto.SubTask;

public class GetSubTaskDto
{
    public long Id { get; set; }
    public bool IsDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public string Name { get; set; }
    public string Priority { get; set; }
    public DateOnly DeadLine { get; set; }
    public long UserId { get; set; }
    public long TaskId { get; set; }
    public bool IsCompleted { get; set; }
    public string UserName { get; set; }
}
