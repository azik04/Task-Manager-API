using FluentValidation;
using TaskManager.Core.Dto.Themes;

namespace TaskManager.Core.Validation.Theme;

public class UpdateThemeDtoValidation : AbstractValidator<UpdateThemeDto>
{
    public UpdateThemeDtoValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Mövzu adı tələb olunur.");
    }
}
