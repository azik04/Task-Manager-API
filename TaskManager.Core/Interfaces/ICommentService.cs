using TaskManager.Core.Dto.Comment;
using TeleSales.Core.Responses;

namespace TaskManager.Core.Interfaces;

public interface ICommentService
{
    Task<BaseResponse<GetCommentDto>> Create(CreateCommentDto comment);
    Task<BaseResponse<ICollection<GetCommentDto>>> GetByTask(long taskId);
    Task<BaseResponse<GetCommentDto>> Remove(long id);
    Task<BaseResponse<GetCommentDto>> Update(long id , UpdateCommentDto comment);
}

