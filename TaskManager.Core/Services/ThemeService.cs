using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Dto.Comment;
using TaskManager.Core.Dto.Themes;
using TaskManager.Core.Interfaces;
using TaskManager.DataProvider.Context;
using TaskManager.DataProvider.Entities;
using TeleSales.Core.Responses;

namespace TaskManager.Core.Services;

public class ThemeService : IThemeService
{
    private readonly ApplicationDbContext _db;
    public ThemeService(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BaseResponse<GetThemeDto>> CreateAsync(CreateThemeDto theme)
    {
        var data = new Themes
        {
            Name = theme.Name,
            CreatedBy =  theme.CreatedBy ,
            CreateAt = DateTime.Now,
        };

        await _db.Themes.AddAsync(data);
        await _db.SaveChangesAsync();

        var dto = new GetThemeDto
        {
            CreateAt = data.CreateAt,
            Id = data.Id,
            isDeleted = data.IsDeleted,
            CreatedBy = data.CreatedBy,
            Name = data.Name,
        };
        return new BaseResponse<GetThemeDto> ( dto );
    }

    public async Task<BaseResponse<ICollection<GetThemeDto>>> GetAllAsync(long userId)
    {
        var data = await _db.Themes.Where(x => !x.IsDeleted && x.CreatedBy == userId).ToListAsync();

        var dto =  data.Select(theme => new GetThemeDto
        {
            Id = theme.Id,
            Name = theme.Name,
            CreateAt = theme.CreateAt,
            isDeleted= theme.IsDeleted,
        }).ToList();

        return new BaseResponse<ICollection<GetThemeDto>>(dto);
    }



    public async Task<BaseResponse<GetThemeDto>> RemoveAsync(long id)
    {
        var data = await _db.Themes.SingleOrDefaultAsync(x=> x.Id == id);
        data.IsDeleted = true;

        _db.Themes.Update(data);
        await _db.SaveChangesAsync();

        var dto = new GetThemeDto
        {
            CreateAt = data.CreateAt,
            Id = data.Id,
            isDeleted = data.IsDeleted,
            Name = data.Name,
        };
        return new BaseResponse<GetThemeDto>(dto);
    }

    public async Task<BaseResponse<GetThemeDto>> UpdateAsync(long id, UpdateThemeDto theme)
    {
        var data = await _db.Themes.SingleOrDefaultAsync(x => x.Id == id);
        data.Name = theme.Name;

        _db.Themes.Update(data);
        await _db.SaveChangesAsync();

        var dto = new GetThemeDto
        {
            CreateAt = data.CreateAt,
            Id = data.Id,
            isDeleted = data.IsDeleted,
            Name = data.Name,
        };
        return new BaseResponse<GetThemeDto>(dto);
    }
}
