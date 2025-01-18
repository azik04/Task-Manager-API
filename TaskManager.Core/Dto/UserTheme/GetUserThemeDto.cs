using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Core.Dto.UserTheme;

public class GetUserThemeDto
{
    public long Id { get; set; }
    public bool isDeleted { get; set; }
    public DateTime CreateAt { get; set; }
    public long ThemeId { get; set; }
    public string ThemeName { get; set; }
    public string CreatedBy { get; set; }

    public long UserId { get; set; }
    public string UserName { get; set; } 
}
