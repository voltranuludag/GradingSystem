using Core.Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class OperationClaimValidation : AbstractValidator<OperationClaim>
    {
        public OperationClaimValidation()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(x => x.Name).MinimumLength(2);
        }
    }
}