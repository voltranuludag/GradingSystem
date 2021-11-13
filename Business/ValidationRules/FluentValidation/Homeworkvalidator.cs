using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class Homeworkvalidator : AbstractValidator<Homework>
    {
        public Homeworkvalidator()
        {
            RuleFor(p => p.HomeworkName).NotEmpty();
            RuleFor(c => c.HomeworkName).MinimumLength(2);
        }
    }
}