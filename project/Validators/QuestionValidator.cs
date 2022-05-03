namespace backend_dev_project.Validators;
public class QuestionValidator : AbstractValidator<Question>
{
    public QuestionValidator() {
        RuleFor(q => q.question).NotEmpty().WithMessage("Name can't be empty").MaximumLength(90).WithMessage("Question can't be longer than 90 characters");
    }
}