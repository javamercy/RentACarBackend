﻿using Business.Abstract;
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

        [ValidationAspect(typeof(CarValidator))]
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

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(BusinessMessages.CarDeleted);
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(), BusinessMessages.CarsListed);
        }

        public IDataResult<List<Car>> GetAllByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.BrandId == id));
        }

        public IDataResult<List<Car>> GetAllByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetAll(c => c.ColorId == id));
        }

        public IDataResult<List<CarDetailDto>> GetAllByDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetAllByDetails());
        }

        public IDataResult<Car> GetById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }

        //[ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(BusinessMessages.CarUpdated);
        }

        private IResult CheckIfCarCountOfBrandExceeded(int brandId)
        {
            var cars = GetAllByBrandId(brandId).Data;

            if (cars.Count >= 5)
            {
                return new ErrorResult(BusinessMessages.CarCountOfBrandError);
            }

            return new SuccessResult();
        }

        private IResult CheckIfCarDescriptionAlreadyExists(string carDescription)
        {
            var cars = GetAll().Data;
            var result = cars.Any(c => c.Description == carDescription);

            if (result)
            {
                return new ErrorResult(BusinessMessages.CarDescriptionAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckIfBrandLimitExceeded()
        {
            var brandCount = _brandService.GetAll().Data.Count;

            if (brandCount >= 15)
            {
                return new ErrorResult(BusinessMessages.BrandLimitExceeded);
            }

            return new SuccessResult();
        }
    }
}
