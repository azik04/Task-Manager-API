using FluentValidation;
using TaskManager.ViewModels.UserTask;

namespace TaskManager.Validation.UserTask;

public class CreateUserTaskVMValidation : AbstractValidator<CreateUserTaskVM>
{
    public CreateUserTaskVMValidation() 
    {
        RuleFor(x => x.TaskId)
            .NotEmpty().WithMessage("Task ID is required.")
            .GreaterThan(0).WithMessage("Task ID must be greater than 0.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.")
            .GreaterThan(0).WithMessage("User ID must be greater than 0.");
    }
}
