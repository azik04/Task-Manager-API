using TaskManager.DataProvider.Entities.BaseModel;

namespace TaskManager.DataProvider.Entities;

public class Files:Base
{
    public string FileName { get; set; }
    public long TaskId { get; set; }
    public virtual Tasks Tasks { get; set; }
}
