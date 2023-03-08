using Business.Abstract;
using Business.Constants;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private readonly IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Add(Rental rental)
        {
            var result = BusinessRules.Run(CheckIfCarDelievered(rental.CarId));

            if (result != null)
            {
                return result;
            }

            _rentalDal.Add(rental);
            return new SuccessResult();
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(BusinessMessages.RentalDeleted);
        }

        public IResult Deliver(Rental rental)
        {
            var result = BusinessRules.Run(CheckIfCarCanBeDelievered(rental.CarId));

            if (result != null)
            {
                return result;
            }

            rental.ReturnDate = DateTime.Now;

            this.Update(rental);

            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(
                _rentalDal.GetAll(),
                BusinessMessages.AllRentalsListed
            );
        }

        public IDataResult<List<Rental>> GetAllByCarId(int carId)
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll(r => r.CarId == carId));
        }

        public IDataResult<List<Rental>> GetAllByCustomerId(int customerId)
        {
            return new SuccessDataResult<List<Rental>>(
                _rentalDal.GetAll(r => r.CustomerId == customerId)
            );
        }

        public IDataResult<List<RentalDetailDto>> GetAllByDetails()
        {
            return new SuccessDataResult<List<RentalDetailDto>>(_rentalDal.GetAllByDetails());
        }

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id));
        }

        [ValidationAspect(typeof(RentalValidator))]
        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);

            return new SuccessResult();
        }

        // true, if the car was delivered - false, if the car has not been delivered yet
        private IResult CheckIfCarDelievered(int carId)
        {
            var carsToCheck = _rentalDal.GetAll(r => r.CarId == carId);

            var result = carsToCheck.Any(
                c => c.ReturnDate.ToLocalTime().CompareTo(DateTime.Now) > 0
            );

            if (carsToCheck.Count > 0 && result)
            {
                return new ErrorResult(BusinessMessages.CarNotDelivered);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCarCanBeDelievered(int carId)
        {
            var carsToCheck = _rentalDal.GetAll(r => r.CarId == carId);

            if (carsToCheck.Count > 0)
            {
                var result = carsToCheck.Any(
                    c => DateTime.Compare(c.ReturnDate.ToLocalTime(), DateTime.Now) < 0
                );

                if (result)
                {
                    return new ErrorResult(BusinessMessages.CarAlreadyDelivered);
                }
                else
                {
                    return new SuccessResult(BusinessMessages.CarDeliveredSuccessfully);
                }
            }

            return new ErrorResult(BusinessMessages.CarWasNotRented);
        }
    }
}
