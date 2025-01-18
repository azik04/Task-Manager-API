using TaskManager.Core.Dto.Tasks;
using TaskManager.Core.Dto.UserTheme;
using TeleSales.Core.Responses;

namespace TaskManager.Core.Interfaces;

public interface ITaskService
{
    Task<BaseResponse<GetTaskDto>> Create(CreateTaskDto task);
    Task<BaseResponse<ICollection<GetTaskDto>>> GetAllDone(long themeId);
    Task<BaseResponse<ICollection<GetTaskDto>>> GetAllNotDone(long themeId);
    Task<BaseResponse<GetTaskDto>> GetById(long id);
    Task<BaseResponse<GetTaskDto>> Remove(long id);
    Task<BaseResponse<GetTaskDto>> Update(long id, UpdateTaskDto task);
    Task<BaseResponse<GetTaskDto>> Complite(long id);
}
