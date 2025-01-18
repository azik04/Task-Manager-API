using FluentValidation;
using TaskManager.Core.Dto.Themes;

namespace TaskManager.Core.Validation.Theme;

public class CreateThemeDtoValidation : AbstractValidator<CreateThemeDto>
{
    public CreateThemeDtoValidation()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Mövzu adı tələb olunur.");
    }
}
