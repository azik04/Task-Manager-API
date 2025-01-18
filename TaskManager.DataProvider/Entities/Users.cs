using TaskManager.DataProvider.Entities.BaseModel;
using TaskManager.DataProvider.Enums;

namespace TaskManager.DataProvider.Entities;

public class Users:Base
{
    public string FullName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public Role Role { get; set; }

    public virtual ICollection<Tasks> Tasks { get; set; }
    public virtual ICollection<UserThemes> UserThemes { get; set; }
    public virtual ICollection<Comments> Comment { get; set; }
    public virtual ICollection<Themes> Themes { get; set; }
    public virtual ICollection<SubTasks> CoTasks { get; set; }
    public virtual ICollection<UserTasks> UserTasks { get; set; }  

}
