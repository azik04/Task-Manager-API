using FluentValidation;
using TaskManager.Core.Dto.Comment;

namespace TaskManager.Core.Validation.Comment;

public class UpdateCommentDtoValidation : AbstractValidator<UpdateCommentDto>
{
    public UpdateCommentDtoValidation()
    {
        RuleFor(x => x.Message)
            .NotEmpty()
            .WithMessage("Mesaj tələb olunur.");
    }
}
