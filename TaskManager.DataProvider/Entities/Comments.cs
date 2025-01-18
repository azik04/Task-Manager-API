
using TaskManager.DataProvider.Entities.BaseModel;

namespace TaskManager.DataProvider.Entities;

public class Comments:Base
{
    public string Message { get; set; }
    public long UserId { get; set; }
    public long TaskId { get; set; }
    public virtual Users User { get; set; }
    public virtual Tasks Tasks { get; set; }
}
