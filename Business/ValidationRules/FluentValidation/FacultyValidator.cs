using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation
{
    public class FacultyValidator:AbstractValidator<Faculty>
    {
        public FacultyValidator()
        {
            RuleFor(p => p.FacultyName).NotEmpty();
            RuleFor(x => x.FacultyName).MinimumLength(2);
        }
    }
}
