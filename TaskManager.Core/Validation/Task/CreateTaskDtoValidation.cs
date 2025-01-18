using FluentValidation;
using TaskManager.Core.Dto.Tasks;
using TaskManager.DataProvider.Enums;

namespace TaskManager.Core.Validation.Task
{
    public class CreateTaskDtoValidation : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidation()
        {
            RuleFor(x => x.TaskName)
                .NotEmpty()
                .WithMessage("Tapşırıq adı tələb olunur.");

            RuleFor(x => x.TaskDescription)
                .NotEmpty()
                .WithMessage("Tapşırıq təsviri tələb olunur.");

            RuleFor(x => x.Status)
                .IsInEnum()
                .WithMessage("Seçilmiş status yanlışdır.");

            RuleFor(x => x.Priority)
                .IsInEnum()
                .WithMessage("Seçilmiş prioritet yanlışdır."); 

            //RuleFor(x => x.DeadLine)
            //    .Must(deadline => deadline > DateOnly.FromDateTime(DateTime.Now))
            //    .WithMessage("Son tarix indiki vaxtdan sonra olmalıdır.");

            RuleFor(x => x.ThemeId)
                .GreaterThan(0)
                .WithMessage("Mövzu ID düzgün olmalıdır."); 

        }
    }
}
