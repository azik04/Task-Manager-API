using TaskManager.Core.Dto.SubTask;
using TeleSales.Core.Responses;

namespace TaskManager.Core.Interfaces;

public interface ISubTaskService
{
    Task<BaseResponse<GetSubTaskDto>> Create(CreateSubTaskDto subTask);
    Task<BaseResponse<ICollection<GetSubTaskDto>>> GetByTaskDone(long taskId);
    Task<BaseResponse<ICollection<GetSubTaskDto>>> GetByTaskNotDone(long taskId);
    Task<BaseResponse<GetSubTaskDto>> GetById(long id);

    Task<BaseResponse<GetSubTaskDto>> Remove(long id);
    Task<BaseResponse<GetSubTaskDto>> Complete(long id);
    Task<BaseResponse<GetSubTaskDto>> Update(long taskId, UpdateSubTaskDto subTask);
}
