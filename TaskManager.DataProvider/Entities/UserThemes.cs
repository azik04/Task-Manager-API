using TaskManager.DataProvider.Entities.BaseModel;

namespace TaskManager.DataProvider.Entities;

public class UserThemes:Base
{
    public long ThemeId { get; set; }
    public long UserId { get; set; }

    public virtual Themes Theme { get; set; }
    public virtual Users User { get; set; }
}
