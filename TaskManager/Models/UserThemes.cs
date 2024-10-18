using TaskManager.Models.Base;

namespace TaskManager.Models;

public class UserThemes : BaseModel
{
    public long ThemeId { get; set; }

    public long UserId { get; set; }
    public virtual Themes Theme { get; set; }
    public virtual Users User { get; set; }
}
