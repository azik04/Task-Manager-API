using FluentValidation;
using TaskManager.Core.Dto.Comment;

namespace TaskManager.Core.Validation.Comment;

public class CreateCommentDtoValidation : AbstractValidator<CreateCommentDto>
{
    public CreateCommentDtoValidation()
    {
        RuleFor(x => x.Message)
            .NotEmpty()
            .WithMessage("Mesaj tələb olunur."); 

        RuleFor(x => x.UserId)
            .GreaterThan(0)
            .WithMessage("İstifadəçi ID düzgün olmalıdır."); 

        RuleFor(x => x.TaskId)
            .GreaterThan(0)
            .WithMessage("Task ID düzgün olmalıdır."); 
    }
}
