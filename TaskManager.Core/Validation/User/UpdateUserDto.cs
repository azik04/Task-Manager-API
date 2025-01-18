using FluentValidation;
using TaskManager.Core.Dto.Users;

namespace TaskManager.Core.Validation.User;

public class UpdateUserDtoValidation : AbstractValidator<UpdateUserDto>
{
    public UpdateUserDtoValidation()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad tələb olunur.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad tələb olunur.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-poçt ünvanı tələb olunur.");
            //.EmailAddress().WithMessage("Keçərli e-poçt ünvanı daxil edin.");
    }
}
