using Microsoft.EntityFrameworkCore;
using TaskManager.Context;
using TaskManager.Models;
using TaskManager.Response;
using TaskManager.Services.Interfaces;
using TaskManager.ViewModels.UserTheme;

namespace TaskManager.Services.Implementations;

public class UserThemeService : IUserThemeService
{
    private readonly ApplicationDbContext _db;
    public UserThemeService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IBaseResponse<GetUserThemeVM>> AddUsersToTheme(CreateUserThemeVM vm)
    {
        try
        {
            var theme = await _db.Themes.FindAsync(vm.ThemeId);
            var user = await _db.Users.FindAsync(vm.UserId);

            if (theme == null || user == null)
                return new BaseResponse<GetUserThemeVM>
                {
                    Description = theme == null ? "Theme not found." : "User not found.",
                    StatusCode = Enum.StatusCode.NotFound
                };

            var existingUserTheme = await _db.UserThemes
                .AnyAsync(ut => ut.ThemeId == vm.ThemeId && ut.UserId == vm.UserId && !ut.IsDeleted);

            if (existingUserTheme)
                return new BaseResponse<GetUserThemeVM>
                {
                    Description = "User already assigned to the theme.",
                    StatusCode = Enum.StatusCode.NotFound // Use Conflict for duplicate entries
                };

            var newUserTheme = new UserThemes
            {
                ThemeId = vm.ThemeId,
                UserId = vm.UserId,
                CreatedByUserId = vm.CreatedByUserId,
                CreateAt = DateTime.Now // Use UTC for consistency
            };

            await _db.UserThemes.AddAsync(newUserTheme);
            await _db.SaveChangesAsync();

            var result = new GetUserThemeVM
            {
                UserId = newUserTheme.UserId,
                ThemeId = newUserTheme.ThemeId,
                Id = newUserTheme.Id,
                CreatedByUserId = vm.CreatedByUserId,
            };

            return new BaseResponse<GetUserThemeVM>
            {
                Data = result,
                Description = "User successfully added to the theme.",
                StatusCode = Enum.StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<GetUserThemeVM>
            {
                Description = $"Error: {ex.Message}",
                StatusCode = Enum.StatusCode.Error
            };
        }
    }



    public async Task<IBaseResponse<ICollection<Themes>>> GetThemesByUserId(long userId)
    {
        try
        {
            var themes = await _db.UserThemes
                .Where(ut => ut.UserId == userId && !ut.IsDeleted)
                .Include(ut => ut.Theme)
                .Select(ut => ut.Theme)
                .Distinct()
                .ToListAsync();

            if (!themes.Any())
            {
                return new BaseResponse<ICollection<Themes>>()
                {
                    Description = "No themes found for the user.",
                    StatusCode = Enum.StatusCode.NotFound
                };
            }

            return new BaseResponse<ICollection<Themes>>()
            {
                Data = themes,
                Description = "Themes retrieved successfully.",
                StatusCode = Enum.StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<ICollection<Themes>>()
            {
                Description = $"Error: {ex.Message}",
                StatusCode = Enum.StatusCode.Error
            };
        }
    }

    public async Task<IBaseResponse<ICollection<Users>>> GetUsersByThemeId(long themeId)
    {
        try
        {
            var users = await _db.UserThemes
                .Where(ut => ut.ThemeId == themeId && !ut.IsDeleted)
                .Include(ut => ut.User)
                .Select(ut => ut.User)
                .Distinct()
                .ToListAsync();

            if (users == null || !users.Any())
            {
                return new BaseResponse<ICollection<Users>>()
                {
                    Description = "No users found for the theme.",
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

    public async Task<IBaseResponse<GetUserThemeVM>> RemoveUserFromTheme(long themeId, long userId)
    {
        try
        {
            var userTheme = await _db.UserThemes
                .FirstOrDefaultAsync(ut => ut.UserId == userId && ut.ThemeId == themeId  && !ut.IsDeleted);

            if (userTheme == null)
            {
                return new BaseResponse<GetUserThemeVM>()
                {
                    Description = "UserTheme not found.",
                    StatusCode = Enum.StatusCode.NotFound
                };
            }
            userTheme.IsDeleted = true;
            _db.UserThemes.Update(userTheme);
            await _db.SaveChangesAsync();

            var result = new GetUserThemeVM()
            {
                UserId = userTheme.UserId,
                ThemeId = userTheme.ThemeId,
                CreatedByUserId = userTheme.CreatedByUserId,

            };

            return new BaseResponse<GetUserThemeVM>()
            {
                Data = result,
                Description = "User successfully removed from the task.",
                StatusCode = Enum.StatusCode.OK
            };
        }
        catch (Exception ex)
        {
            return new BaseResponse<GetUserThemeVM>()
            {
                Description = $"Error: {ex.Message}",
                StatusCode = Enum.StatusCode.Error
            };
        }
    }
}
