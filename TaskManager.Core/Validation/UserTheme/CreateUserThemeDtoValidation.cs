using FluentValidation;
using TaskManager.Core.Dto.UserTheme;

namespace TaskManager.Core.Validation.UserTheme;

public class CreateUserThemeDtoValidation : AbstractValidator<CreateUserThemeDto>
{
    public CreateUserThemeDtoValidation()
    {
        RuleFor(x => x.ThemeId)
            .GreaterThan(0).WithMessage("Tema ID düzgün olmalıdır.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("İstifadəçi ID düzgün olmalıdır.");
    }
}
