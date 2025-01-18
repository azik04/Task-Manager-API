using TaskManager.DataProvider.Entities.BaseModel;

namespace TaskManager.DataProvider.Entities;

public class Themes:Base
{
    public string Name { get; set; }
    public long CreatedBy { get; set; }
    
    public virtual Users Users { get; set; }
    public virtual ICollection<Tasks> Tasks { get; set; }
    public virtual ICollection<UserThemes> UserThemes { get; set; }
}
