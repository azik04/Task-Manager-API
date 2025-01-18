using FluentValidation;
using TaskManager.Core.Dto.Users;

namespace TaskManager.Core.Validation.User;

public class CreateUserDtoValidation : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad tələb olunur.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad tələb olunur.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-poçt ünvanı tələb olunur.");
            //.EmailAddress().WithMessage("Keçərli e-poçt ünvanı daxil edin.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifrə tələb olunur.")
            .MinimumLength(6).WithMessage("Şifrə ən azı 6 simvoldan ibarət olmalıdır.");
    }
}
