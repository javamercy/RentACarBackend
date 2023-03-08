using Business.Abstract;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private readonly ICustomerDal _customerDal;

        public CustomerManager(ICustomerDal customerDal)
        {
            _customerDal = customerDal;
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Add(Customer customer)
        {
            var result = BusinessRules.Run(CheckIfCustomerAlreadyExists(customer.UserId));

            if (result != null)
            {
                return result;
            }
            _customerDal.Add(customer);
            return new SuccessResult();
        }

        public IResult Delete(Customer customer)
        {
            _customerDal.Delete(customer);
            return new SuccessResult();
        }

        public IDataResult<List<Customer>> GetAll()
        {
            return new SuccessDataResult<List<Customer>>(_customerDal.GetAll());
        }

        public IDataResult<Customer> GetById(int id)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.Id == id));
        }

        public IDataResult<Customer> GetByUserId(int userId)
        {
            return new SuccessDataResult<Customer>(_customerDal.Get(c => c.UserId == userId));
        }

        [ValidationAspect(typeof(CustomerValidator))]
        public IResult Update(Customer customer)
        {
            var result = BusinessRules.Run(CheckIfCustomerAlreadyExists(customer.UserId));

            if (result != null)
            {
                return result;
            }

            _customerDal.Update(customer);
            return new SuccessResult();
        }

        private IResult CheckIfCustomerAlreadyExists(int userId)
        {
            var customerToCheck = _customerDal.Get(customer => customer.UserId == userId);

            if (customerToCheck != null)
            {
                return new ErrorResult(BusinessMessages.CustomerAlreadyExists);
            }

            return new SuccessResult();
        }
    }
}
