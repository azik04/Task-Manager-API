using Microsoft.EntityFrameworkCore;
using TaskManager.Core.Dto.Comment;
using TaskManager.Core.Interfaces;
using TaskManager.DataProvider.Context;
using TaskManager.DataProvider.Entities;
using TeleSales.Core.Responses;

namespace TaskManager.Core.Services;

public class CommentService : ICommentService
{
    private readonly ApplicationDbContext _db;
    public CommentService (ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<BaseResponse<GetCommentDto>> Create(CreateCommentDto comment)
    {
        var data = new Comments
        {
            Message = comment.Message,
            TaskId = comment.TaskId,
            UserId = comment.UserId,
            CreateAt = DateTime.Now,
        };

        await _db.Comments.AddAsync(data);
        await _db.SaveChangesAsync();

        var dto = new GetCommentDto
        {
            Id = data.Id,
            Message = data.Message,
            TaskId = data.TaskId,
            UserId = data.UserId,
            IsDeleted = data.IsDeleted,
            CreateAt = data.CreateAt,
        };

        return new BaseResponse<GetCommentDto>(dto);
    }

    public async Task<BaseResponse<ICollection<GetCommentDto>>> GetByTask(long taskId)
    {
        var data = await _db.Comments.Where(x => x.TaskId == taskId && !x.IsDeleted).ToListAsync();

        var dto = data.Select(comment => new GetCommentDto
        {
            Id = comment.Id,
            Message = comment.Message,
            TaskId = comment.TaskId,
            UserId = comment.UserId,
            IsDeleted = comment.IsDeleted,
            CreateAt = comment.CreateAt,
        }).ToList();

        return new BaseResponse<ICollection<GetCommentDto>>(dto);
    }

    public async Task<BaseResponse<GetCommentDto>> Remove(long id)
    {
        var data = await _db.Comments.SingleOrDefaultAsync(x => x.Id == id);
        data.IsDeleted = true;

        _db.Update(data);
        await _db.SaveChangesAsync();

        var dto = new GetCommentDto
        {
            Id = data.Id,
            Message = data.Message,
            TaskId = data.TaskId,
            UserId = data.UserId,
            IsDeleted = data.IsDeleted,
            CreateAt = data.CreateAt,
        };

        return new BaseResponse<GetCommentDto>(dto);
    }

    public async Task<BaseResponse<GetCommentDto>> Update(long id, UpdateCommentDto comment)
    {
        var data = await _db.Comments.SingleOrDefaultAsync(x => x.Id == id);
        data.Message = comment.Message;
        
        _db.Comments.Update(data);
        await _db.SaveChangesAsync();

        var dto = new GetCommentDto
        {
            Id = data.Id,
            Message = data.Message,
            TaskId = data.TaskId,
            UserId = data.UserId,
            IsDeleted = data.IsDeleted,
            CreateAt = data.CreateAt,
        };

        return new BaseResponse<GetCommentDto>(dto);
    }
}
