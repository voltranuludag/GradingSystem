using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class GroupValidator : AbstractValidator<Group>
    {
        public GroupValidator()
        {
            RuleFor(p => p.GroupName).NotEmpty();
            RuleFor(x => x.GroupName).MinimumLength(1);
        }
    }
}