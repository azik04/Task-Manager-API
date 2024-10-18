using Microsoft.EntityFrameworkCore;
using TaskManager.Context;
using TaskManager.Models;
using TaskManager.Response;
using TaskManager.Services.Interfaces;
using TaskManager.ViewModels.UserTask;

namespace TaskManager.Services.Implementations;

public class UserTaskService : IUserTaskService
{
    private readonly ApplicationDbContext _db;
    private readonly IMailService _mailService;

    public UserTaskService(ApplicationDbContext db, IMailService mailService)
    {
        _db = db;
        _mailService = mailService;
    }
    public async Task<IBaseResponse<GetUserTaskVM>> AddUsersToTask(CreateUserTaskVM vm)
    {
        try
        {
            var task = await _db.Themes.FirstOrDefaultAsync(x => x.Id == vm.TaskId);
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == vm.UserId);

            if (task == null || user == null)
                return new BaseResponse<GetUserTaskVM>()
                {
                    Description = task == null ? "Theme not found." : "User not found.",
                    StatusCode = Enum.StatusCode.NotFound
                };

            var existingUserTask = await _db.UserTasks
                .FirstOrDefaultAsync(ut => ut.TaskId == vm.TaskId && ut.UserId == vm.UserId);

            if (existingUserTask != null)
                return new BaseResponse<GetUserTaskVM>()
                {
                    Description = "User already assigned to the task.",
                    StatusCode = Enum.StatusCode.NotFound
                };

            var newUserTask = new UserTasks()
            {
                TaskId = vm.TaskId,
                UserId = vm.UserId,
                CreateAt = DateTime.Now
            };

            await _db.UserTasks.AddAsync(newUserTask);
            await _db.SaveChangesAsync();

            var result = new GetUserTaskVM()
            {
                UserId = newUserTask.UserId,
                TaskId = newUserTask.TaskId,
            };

            return new BaseResponse<GetUserTaskVM>()
            {
                Data = result,
                Description = "User successfully assigned to the theme.",
                StatusCode = Enum.StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<GetUserTaskVM>()
            {
                Description = $"Error: {ex.Message}",
                StatusCode = Enum.StatusCode.Error
            };
        }
    }



    public async Task<IBaseResponse<GetUserTaskVM>> RemoveUserFromTask(long id)
    {
        try
        {
            var userTheme = await _db.UserTasks
                .FirstOrDefaultAsync(ut => ut.Id == id && !ut.IsDeleted);

            if (userTheme == null)
            {
                return new BaseResponse<GetUserTaskVM>()
                {
                    Description = "UserTheme not found.",
                    StatusCode = Enum.StatusCode.NotFound
                };
            }
            userTheme.IsDeleted = true;
            _db.UserTasks.Update(userTheme);
            await _db.SaveChangesAsync();

            var result = new GetUserTaskVM()
            {
                UserId = userTheme.UserId,
                TaskId = userTheme.TaskId
            };

            return new BaseResponse<GetUserTaskVM>()
            {
                Data = result,
                Description = "User successfully removed from the task.",
                StatusCode = Enum.StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<GetUserTaskVM>()
            {
                Description = $"Error: {ex.Message}",
                StatusCode = Enum.StatusCode.Error
            };
        }
    }


    public async Task<IBaseResponse<ICollection<Users>>> GetUsersByTaskId(long taskId)
    {
        try
        {
            var users = await _db.UserTasks
                .Where(ut => ut.TaskId == taskId && !ut.IsDeleted) 
                .Include(ut => ut.User)
                .Select(ut => ut.User)
                .Distinct()
                .ToListAsync();

            if (!users.Any())
            {
                return new BaseResponse<ICollection<Users>>()
                {
                    Description = "No users found for the task.",
                    StatusCode = Enum.StatusCode.NotFound
                };
            }

            return new BaseResponse<ICollection<Users>>()
            {
                Data = users,
                Description = "Users retrieved successfully.",
                StatusCode = Enum.StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ICollection<Users>>()
            {
                Description = $"Error: {ex.Message}",
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public async Task<IBaseResponse<ICollection<Tasks>>> GetTasksByUserId(long userId)
    {
        try
        {
            var tasks = await _db.UserTasks
                .Where(ut => ut.UserId == userId && !ut.IsDeleted)
                .Include(ut => ut.Task)
                .Select(ut => ut.Task)
                .Distinct()
                .ToListAsync();

            if (!tasks.Any())
            {
                return new BaseResponse<ICollection<Tasks>>()
                {
                    Description = "No tasks found for the user.",
                    StatusCode = Enum.StatusCode.NotFound
                };
            }

            return new BaseResponse<ICollection<Tasks>>()
            {
                Data = tasks,
                Description = "Tasks retrieved successfully.",
                StatusCode = Enum.StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ICollection<Tasks>>()
            {
                Description = $"Error: {ex.Message}",
                StatusCode = Enum.StatusCode.Error
            };
        }
    }
}
