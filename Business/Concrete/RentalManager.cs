using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
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

        public IResult Add(Rental rental)
        {
            if (!CheckIfCarDelievered(rental))
            {
                throw new Exception("Car has already been rented!");
            }

            _rentalDal.Add(rental);
            return new SuccessResult();
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult();
        }

        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
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

        public IDataResult<Rental> GetById(int id)
        {
            return new SuccessDataResult<Rental>(_rentalDal.Get(r => r.Id == id));
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult();
        }

        private bool CheckIfCarDelievered(Rental rental)
        {
            var rentsToCheck = _rentalDal.GetAll(r => r.CarId == rental.CarId);

            if (rentsToCheck.Count > 0)
            {
                return !rentsToCheck.Any(r => r.ReturnDate.Equals("0001 - 01 - 01T00: 00:00"));
            }

            return true;
        }
    }
}
