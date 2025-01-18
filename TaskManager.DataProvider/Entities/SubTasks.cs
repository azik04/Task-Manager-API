using TaskManager.DataProvider.Entities.BaseModel;
using TaskManager.DataProvider.Enums;

namespace TaskManager.DataProvider.Entities;

public class SubTasks:Base
{
    public string Name { get; set; }
    public Priority Priority { get; set; }
    public DateOnly DeadLine { get; set; }
    public long UserId { get; set; }
    public long TaskId { get; set; }
    public bool IsCompleted { get; set; }

    public Users User { get; set; }
    public Tasks Task { get; set; }
}
