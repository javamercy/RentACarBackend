using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants.Messages;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        private readonly ICarDal _carDal;
        private readonly IBrandService _brandService;

        public CarManager(ICarDal carDal, IBrandService brandService)
        {
            _carDal = carDal;
            _brandService = brandService;
        }

        [SecuredOperation("admin,car.add")]
        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Add(Car car)
        {
            IResult result = BusinessRules.Run(
                CheckIfCarCountOfBrandExceeded(car.BrandId),
                CheckIfCarDescriptionAlreadyExists(car.Description),
                CheckIfBrandLimitExceeded()
            );

            if (result != null)
            {
                return result;
            }

            _carDal.Add(car);
            return new SuccessResult(BusinessMessages.CarAdded);
        }

        [ValidationAspect(typeof(CarValidator))]
        [CacheRemoveAspect("ICarService.Get")]
        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(BusinessMessages.CarDeleted);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), BusinessMessages.CarsListed);
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAllByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id));
        }

        [CacheAspect]
        public IDataResult<List<Car>> GetAllByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id));
        }

        public IDataResult<List<CarDetailDto>> GetAllByDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllByDetails());
        }

        [CacheAspect]
        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }

        [CacheRemoveAspect("ICarService.Get")]
        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(BusinessMessages.CarUpdated);
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Car car)
        {
            Add(car);
            Update(car);

            return new SuccessResult();
        }

        private IResult CheckIfCarCountOfBrandExceeded(int brandId)
        {
            var cars = _carDal.GetAll(c => c.BrandId == brandId);

            if (cars.Count >= 5)
            {
                return new ErrorResult(BusinessMessages.CarCountOfBrandError);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCarDescriptionAlreadyExists(string carDescription)
        {
            var cars = _carDal.GetAll();
            var result = cars.Any(c => c.Description == carDescription);

            if (result)
            {
                return new ErrorResult(BusinessMessages.CarDescriptionAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfBrandLimitExceeded()
        {
            var brands = _brandService.GetAll().Data;

            if (brands.Count >= 15)
            {
                return new ErrorResult(BusinessMessages.BrandLimitExceeded);
            }

            return new SuccessResult();
        }
    }
}
