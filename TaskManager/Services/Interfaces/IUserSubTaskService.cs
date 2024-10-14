using TaskManager.Models;

namespace TaskManager.Services.Interfaces;

public interface IUserSubTaskService
{
    Task<bool> AddUsersToSubTask(long subTaskId, long userId);
    Task<bool> RemoveUserFromSubTask(long subTaskId, long userId);
    Task<ICollection<Users>> GetUsersBySubTaskkId(long subTaskId);
    Task<ICollection<SubTasks>> GetSubTaskByUserId(long userId);
}
