
namespace backend_dev_project.Validators;
public class RecepeInputValidator : AbstractValidator<AddRecepeInput>
{
    public RecepeInputValidator() {
        RuleFor(r => r.Name).NotEmpty().WithMessage("Name can't be empty").MinimumLength(4).WithMessage("Name should be at least 4 characters").MaximumLength(30).WithMessage("Name can't be longer than 30 characters");
    }
}
