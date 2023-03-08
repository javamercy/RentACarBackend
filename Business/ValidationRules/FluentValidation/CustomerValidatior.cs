using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    internal class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.UserId).NotEmpty();

            RuleFor(customer => customer.CompanyName).MinimumLength(3);
            RuleFor(customer => customer.CompanyName).MaximumLength(20);
            RuleFor(customer => customer.CompanyName).NotEmpty();

        }

    }
}
