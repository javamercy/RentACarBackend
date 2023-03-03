using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Models;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Business.Concrete
{
    public class PaymentManager : IPaymentService
    {
        private readonly IPaymentDal _paymentDal;

        public PaymentManager(IPaymentDal paymentDal)
        {
            _paymentDal = paymentDal;
        }

        [ValidationAspect(typeof(PaymentValidator))]
        public IResult Add(Payment payment)
        {
            this._paymentDal.Add(payment);
            return new SuccessResult();
        }



        public IResult Pay(CreditCard creditCard, int amount)
        {
            // Assuming that the required micro service for carrying out this method was implemented.
            return new SuccessResult();
        }
    }
}
