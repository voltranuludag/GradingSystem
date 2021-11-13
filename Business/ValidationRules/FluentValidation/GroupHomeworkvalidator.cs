using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class GroupHomeworkvalidator : AbstractValidator<GroupHomework>
    {
        public GroupHomeworkvalidator()
        {
            RuleFor(p => p.GroupId).NotEmpty();
            RuleFor(p => p.HomeworkId).NotEmpty();
            RuleFor(p => p.UserId).NotEmpty();
        }
    }
}