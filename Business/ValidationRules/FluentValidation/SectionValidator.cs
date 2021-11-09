using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class SectionValidator:AbstractValidator<Section>
    {
        public SectionValidator()
        {
            RuleFor(p => p.SectionName).NotEmpty();
            RuleFor(x => x.SectionName).MinimumLength(2);
        }
    }
}
