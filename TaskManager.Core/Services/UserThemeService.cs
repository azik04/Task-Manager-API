using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Dto.UserTheme;
using TaskManager.Core.Interfaces;
using TaskManager.DataProvider.Context;
using TaskManager.DataProvider.Entities;
using TeleSales.Core.Responses;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskManager.Core.Services;

public class UserThemeService : IUserThemeService
{
    private readonly ApplicationDbContext _db;
    public UserThemeService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BaseResponse<GetUserThemeDto>> CreateAsync(CreateUserThemeDto dto)
    {
        var data = new UserThemes
        {
            ThemeId = dto.ThemeId,
            UserId = dto.UserId,
            CreateAt = DateTime.Now,
        };
        
        await _db.UserThemes.AddAsync(data);
        await _db.SaveChangesAsync();

        var theme = await _db.Themes.FindAsync(dto.ThemeId);
        var user = await _db.Users.FindAsync(dto.UserId);

        var ndto = new GetUserThemeDto
        {
            Id = data.Id,
            UserId = data.UserId,
            UserName = user.FullName,
            ThemeId = data.ThemeId,
            ThemeName = theme.Name,
            isDeleted = data.IsDeleted,
            CreateAt = data.CreateAt,
        };

        return new BaseResponse<GetUserThemeDto>(ndto);
    }

    public async Task<BaseResponse<ICollection<GetUserThemeDto>>> GetThemeAsync(long userId)
    {
        var datas = await _db.UserThemes.Where(x => x.UserId == userId && !x.IsDeleted).ToListAsync();
        var dtos = new List<GetUserThemeDto>();

        foreach (var item in datas)
        {
            var theme = await _db.Themes.FindAsync(item.ThemeId);
            var user = await _db.Users.FindAsync(item.UserId);
            var crtBy = await _db.Users.FindAsync(theme.CreatedBy);

            var ndto = new GetUserThemeDto
            {
                Id = item.Id,
                UserId = item.UserId,
                UserName = user.FullName,
                ThemeId = item.ThemeId,
                ThemeName = theme.Name,
                isDeleted = item.IsDeleted,
                CreateAt = item.CreateAt,
                CreatedBy = crtBy?.FullName,
            };
            dtos.Add(ndto);
        }
        return new BaseResponse<ICollection<GetUserThemeDto>>(dtos);
    }

    public async Task<BaseResponse<ICollection<GetUserThemeDto>>> GetUsersAsync(long themeId)
    {
        var datas = await _db.UserThemes.Where(x => x.ThemeId == themeId && !x.IsDeleted).ToListAsync();
        var dtos = new List<GetUserThemeDto>();

        foreach (var item in datas)
        {
            var theme = await _db.Themes.FindAsync(item.ThemeId);
            var user = await _db.Users.FindAsync(item.UserId);

            var ndto = new GetUserThemeDto
            {
                Id = item.Id,
                UserId = item.UserId,
                UserName = user.FullName,
                ThemeId = item.ThemeId,
                ThemeName = theme.Name,
                isDeleted = item.IsDeleted,
                CreateAt = item.CreateAt,
            };
            dtos.Add(ndto);
        }
        return new BaseResponse<ICollection<GetUserThemeDto>>(dtos);
    }

    public async Task<BaseResponse<GetUserThemeDto>> RemoveAsync(long id)
    {
        var data = await _db.UserThemes.SingleOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        data.IsDeleted = true;

        var theme = await _db.Themes.FindAsync(data.ThemeId);
        var user = await _db.Users.FindAsync(data.UserId);

        var ndto = new GetUserThemeDto
        {
            Id = data.Id,
            UserId = data.UserId,
            UserName = user.FullName,
            ThemeId = data.ThemeId,
            ThemeName = theme.Name,
            isDeleted = data.IsDeleted,
            CreateAt = data.CreateAt,
        };

        return new BaseResponse<GetUserThemeDto>(ndto);
    }
}
