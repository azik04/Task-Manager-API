using FluentValidation;
using TaskManager.Core.Dto.SubTask;

namespace TaskManager.Core.Validation.SubTask;

public class UpdateSubTaskDtoValidation : AbstractValidator<UpdateSubTaskDto>
{
    public UpdateSubTaskDtoValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Alt tapşırıq adı tələb olunur.");

        RuleFor(x => x.Priority)
            .IsInEnum()
            .WithMessage("Seçilmiş prioritet yanlışdır.");

        RuleFor(x => x.DeadLine)
            .Must(deadline => deadline > DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("Son tarix indiki vaxtdan sonra olmalıdır."); 
    }
}
