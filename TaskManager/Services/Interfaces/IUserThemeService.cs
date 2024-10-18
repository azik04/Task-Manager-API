using TaskManager.Models;
using TaskManager.Response;
using TaskManager.ViewModels.UserTheme;

namespace TaskManager.Services.Interfaces;

public interface IUserThemeService
{
    Task<IBaseResponse<GetUserThemeVM>> AddUsersToTheme(CreateUserThemeVM vm);
    Task<IBaseResponse<GetUserThemeVM>> RemoveUserFromTheme(long id);
    Task<IBaseResponse<ICollection<Users>>> GetUsersByThemeId(long taskId);
    Task<IBaseResponse<ICollection<Themes>>> GetThemesByUserId(long userId);
}
