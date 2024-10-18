using FluentValidation;
using TaskManager.ViewModels.UserTheme;

namespace TaskManager.Validation.UserTheme;

public class CreateUserThemeVMValidation : AbstractValidator<CreateUserThemeVM>
{
    public CreateUserThemeVMValidation()
    {
        RuleFor(x => x.ThemeId)
            .NotEmpty().WithMessage("Task ID is required.")
            .GreaterThan(0).WithMessage("Task ID must be greater than 0.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.")
            .GreaterThan(0).WithMessage("User ID must be greater than 0.");
}
}
