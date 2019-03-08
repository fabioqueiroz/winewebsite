using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;
using Wine.Commons.Business.Models;

namespace Wine.Commons.Validation
{
   
    public class FluentFiguresValidator : AbstractValidator<WineModel>
    {
        public FluentFiguresValidator()
        {
            RuleFor(x => x.Price).GreaterThan(0.0M);

            RuleFor(x => x.Name).NotNull();
        }
    }
}
