using TaskManager.Models;
using TaskManager.Response;
using TaskManager.ViewModels.UserTask;

namespace TaskManager.Services.Interfaces;

public interface IUserTaskService
{
    Task<IBaseResponse<GetUserTaskVM>> AddUsersToTask(CreateUserTaskVM vm);
    Task<IBaseResponse<GetUserTaskVM>> RemoveUserFromTask(long id);
    Task<IBaseResponse<ICollection<Users>>> GetUsersByTaskId(long taskId);
    Task<IBaseResponse<ICollection<Tasks>>> GetTasksByUserId(long userId);
}
