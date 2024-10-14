using Microsoft.EntityFrameworkCore;
using TaskManager.Context;
using TaskManager.Models;
using TaskManager.Services.Interfaces;

namespace TaskManager.Services.Implementations;

public class UserSubTaskService : IUserSubTaskService
{
    private readonly ApplicationDbContext _db;
    private readonly IMailService _mailService;

    public UserSubTaskService(ApplicationDbContext db, IMailService mailService)
    {
        _db = db;
        _mailService = mailService;
    }
    public async Task<bool> AddUsersToSubTask(long subTaskId, long userId)
    {
        try
        {
            var task = await _db.SubTasks.FindAsync(subTaskId);
            var user = await _db.Users.FindAsync(userId);

            if (task == null || user == null)
                return false;

            var existingUserTask = await _db.UserSubTasks
                .FirstOrDefaultAsync(ut => ut.SubTaskId == subTaskId && ut.UserId == userId);

            if (existingUserTask != null)
            {
                if (existingUserTask.IsDeleted)
                {
                    existingUserTask.IsDeleted = false;
                    existingUserTask.CreateAt = DateTime.Now;
                    _db.UserSubTasks.Update(existingUserTask);
                    await _db.SaveChangesAsync();
                    Console.WriteLine("User task restored.");
                    return true;
                }

                Console.WriteLine("This user is already added to the task.");
                return false;
            }

            var userTask = new UserTasks
            {
                TaskId = subTaskId,
                UserId = userId,
                CreateAt = DateTime.Now,
                IsDeleted = false
            };

            await _db.UserTasks.AddAsync(userTask);
            await _db.SaveChangesAsync();

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error adding user to task: {ex.Message}");
            return false;
        }
    }

    public async Task<ICollection<SubTasks>> GetSubTaskByUserId(long userId)
    {
        var subTasks = await _db.UserSubTasks
                 .Where(ut => ut.UserId == userId && !ut.IsDeleted)
                 .Include(ut => ut.SubTask)
                 .Select(ut => ut.SubTask)
                 .Distinct()
                 .ToListAsync();

        return subTasks;
    }

    public async Task<ICollection<Users>> GetUsersBySubTaskkId(long subTaskId)
    {
        var users = await _db.UserSubTasks
                .Where(ut => ut.SubTaskId == subTaskId && !ut.IsDeleted)
                .Select(ut => ut.User)
                .ToListAsync();

        return users;
    }

    public async Task<bool> RemoveUserFromSubTask(long subTaskId, long userId)
    {
        var userSubTask = await _db.UserSubTasks
              .FirstOrDefaultAsync(ut => ut.SubTaskId == subTaskId && ut.UserId == userId);

        if (userSubTask == null)
            return false;
        userSubTask.IsDeleted = true;
        userSubTask.DeletedAt = DateTime.Now;
        _db.UserSubTasks.Update(userSubTask);
        await _db.SaveChangesAsync();
        return true;
    }
}
