using FluentValidation;
using TaskManager.Core.Dto.Users;

namespace TaskManager.Core.Validation.User
{
    public class ChangePasswordDtoValidation : AbstractValidator<ChangePassword>
    {
        public ChangePasswordDtoValidation()
        {
            RuleFor(x => x.OldPassword)
                .NotEmpty().WithMessage("Əvvəlki şifrə tələb olunur.");

            RuleFor(x => x.NewPassword)
                .NotEmpty().WithMessage("Yeni şifrə tələb olunur.")
                .MinimumLength(6).WithMessage("Yeni şifrə ən azı 6 simvoldan ibarət olmalıdır.");

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty().WithMessage("Şifrəni təsdiq edin.")
                .Equal(x => x.NewPassword).WithMessage("Yeni şifrə ilə təsdiq edilmiş şifrə eyni olmalıdır.");
        }
    }
}
