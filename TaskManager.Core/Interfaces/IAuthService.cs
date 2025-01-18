using TaskManager.Core.Dto.AUTH;
using TeleSales.Core.Responses;

namespace TaskManager.Core.Interfaces;

public interface IAuthService
{
    Task<BaseResponse<string>> LogIn (AuthDto authDto);
}
