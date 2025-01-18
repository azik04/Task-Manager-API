using TaskManager.Core.Dto.Themes;
using TeleSales.Core.Responses;

namespace TaskManager.Core.Interfaces;

public interface IThemeService
{
    Task<BaseResponse<GetThemeDto>> CreateAsync(CreateThemeDto theme);
    Task<BaseResponse<ICollection<GetThemeDto>>> GetAllAsync(long userId);
    Task<BaseResponse<GetThemeDto>> RemoveAsync(long id);
    Task<BaseResponse<GetThemeDto>> UpdateAsync(long id , UpdateThemeDto theme);


}
