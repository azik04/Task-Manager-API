using TaskManager.Models.Base;

namespace TaskManager.Models;

public class UserSubTask : BaseModel
{
    public long SubTaskId { get; set; }
    public long UserId { get; set; }

    public virtual SubTasks SubTask { get; set; }
    public virtual Users User { get; set; }
}
