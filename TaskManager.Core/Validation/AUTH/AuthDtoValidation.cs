using FluentValidation;
using TaskManager.Core.Dto.AUTH;

namespace TaskManager.Core.Validation.AUTH;

public class AuthDtoValidation : AbstractValidator<AuthDto>
{
    public AuthDtoValidation()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-poçt ünvanı tələb olunur.")
            .EmailAddress().WithMessage("Düzgün e-poçt ünvanı daxil edin.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə tələb olunur.")
            .MinimumLength(6).WithMessage("Şifrə ən azı 6 simvoldan ibarət olmalıdır.");

        RuleFor(x => x.RememberMe)
            .NotNull().WithMessage("Xatırlatma seçimi düzgün olmalıdır.");
    }
}
