using TaskManager.DataProvider.Enums;

namespace TaskManager.Core.Dto.SubTask;

public class CreateSubTaskDto
{
    public string Name { get; set; }
    public Priority Priority { get; set; }
    public DateOnly DeadLine { get; set; }
    public long UserId { get; set; }
    public long TaskId { get; set; }
}
