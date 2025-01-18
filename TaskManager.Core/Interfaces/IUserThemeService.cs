using TaskManager.Core.Dto.UserTheme;
using TeleSales.Core.Responses;

namespace TaskManager.Core.Interfaces;

public interface IUserThemeService
{
    Task<BaseResponse<GetUserThemeDto>> CreateAsync(CreateUserThemeDto dto);
    Task<BaseResponse<ICollection<GetUserThemeDto>>> GetUsersAsync(long themeId);
    Task<BaseResponse<ICollection<GetUserThemeDto>>> GetThemeAsync (long userId);
    Task<BaseResponse<GetUserThemeDto>> RemoveAsync(long id);
}
