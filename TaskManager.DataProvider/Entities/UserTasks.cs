using TaskManager.DataProvider.Entities.BaseModel;

namespace TaskManager.DataProvider.Entities;

public class UserTasks:Base
{
    public long TaskId { get; set; }
    public long UserId { get; set; }
    public bool isSeen { get; set; }
    public virtual Tasks Tasks { get; set; }
    public virtual Users Users { get; set; }
}
