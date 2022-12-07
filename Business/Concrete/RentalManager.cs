using Business.Abstract;
using Business.Constants;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
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
            if (!CheckIfCarDelievered(rental))
            {
                return new ErrorResult(BusinessMessages.CarAlreadyRented);
            }

            rental.RentDate = rental.RentDate.ToLocalTime();

            _rentalDal.Add(rental);
            return new SuccessResult();
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult(BusinessMessages.RentalDeleted);
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

        public IResult Update(Rental rental)
        {
            rental.ReturnDate = rental.ReturnDate.ToLocalTime();
            _rentalDal.Update(rental);

            return new SuccessResult();
        }

        // true, if the car was delivered - false, if the car has not been delivered yet
        private bool CheckIfCarDelievered(Rental rental)
        {
            var rentsToCheck = _rentalDal.GetAll(r => r.CarId == rental.CarId);

            if (rentsToCheck.Count > 0)
            {
                return !rentsToCheck.Any(
                    r => r.ReturnDate.ToLocalTime().Equals(SqlServerConstants.DateNull)
                );
            }

            return true;
        }
    }
}
