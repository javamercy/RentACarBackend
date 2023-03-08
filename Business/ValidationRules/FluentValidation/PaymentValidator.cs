using Entities.Concrete;
using Entities.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator()
        {
            RuleFor(payment => payment.Amount).NotEmpty();
            RuleFor(payment => payment.Amount).GreaterThan(0);

            RuleFor(payment => payment.Date).NotEmpty();

            RuleFor(payment => payment.CustomerId).NotEmpty();

        }
    }
}
