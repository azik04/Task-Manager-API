using System.ComponentModel.DataAnnotations;

namespace TaskManager.Models;


public class UserThemes 
{
    [Key]
    public long Id { get; set; }
    public DateTime CreateAt { get; set; }
    public bool IsDeleted { get; set; }
    public long ThemeId { get; set; }
    public long UserId { get; set; }
    public long CreatedByUserId { get; set; }

    public virtual Themes Theme { get; set; }
    public virtual Users User { get; set; }
}

