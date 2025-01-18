using TaskManager.Core.Dto.Users;
using TaskManager.DataProvider.Enums;
using TeleSales.Core.Responses;

namespace TaskManager.Core.Interfaces;

public interface IUserService
{
    Task<BaseResponse<GetUserDto>> Create(CreateUserDto user);
    Task<BaseResponse<ICollection<GetUserDto>>> GetAdmin();
    Task<BaseResponse<ICollection<GetUserDto>>> GetUser();
    Task<BaseResponse<GetUserDto>> Remove(long id);
    Task<BaseResponse<GetUserDto>> ChangeRole(long id, Role role);
    Task<BaseResponse<GetUserDto>> Update(long id , UpdateUserDto user);
}
