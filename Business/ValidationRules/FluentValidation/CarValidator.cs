using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(car => car.Description).NotEmpty();
            RuleFor(car => car.Description).MinimumLength(2);

            RuleFor(car => car.ModelYear).NotEmpty();
            RuleFor(car => car.ModelYear).LessThanOrEqualTo(DateTime.Now.Year);

            RuleFor(car => car.DailyPrice).NotEmpty();
            RuleFor(car => car.DailyPrice).GreaterThan(0);
            RuleFor(car => car.DailyPrice).GreaterThan(10).When(car => car.BrandId == 1);

            RuleFor(car => car.BrandId).NotEmpty();

            RuleFor(car => car.ColorId).NotEmpty();

            RuleFor(car => car.Description)
                .Must(StartsWithA)
                .WithMessage("Car description has to start with 'A'.");
        }

        private bool StartsWithA(string arg)
        {
            return arg.StartsWith("A");
        }
    }
}
