﻿using Business.Abstract;
using Business.Constants;
using Core.Utilities.Business;
using Core.Utilities.Helpers.FileHelper;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class CarImageManager : ICarImageService
    {
        private readonly ICarImageDal _carImageDal;
        private readonly IFileHelper _fileHelper;

        public CarImageManager(ICarImageDal carImageDal, IFileHelper fileHelper)
        {
            _carImageDal = carImageDal;
            _fileHelper = fileHelper;
        }

        public IResult Add(IFormFile file, CarImage carImage)
        {
            var result = BusinessRules.Run(CheckIfImageCountExceeded(carImage.CarId));

            if (result != null)
            {
                return result;
            }
            carImage.ImagePath = _fileHelper
                .Upload(file, CarImagesPathConstants.RootPath + CarImagesPathConstants.FilePath)
                .Replace("wwwroot/", "");

            _carImageDal.Add(carImage);

            return new SuccessResult();
        }

        public IResult Delete(CarImage carImage)
        {
            var imagePath = _carImageDal.Get(i => i.Id == carImage.Id).ImagePath;

            _fileHelper.Delete(CarImagesPathConstants.RootPath + imagePath);

            _carImageDal.Delete(carImage);

            return new SuccessResult();
        }

        public IDataResult<List<CarImage>> GetAll()
        {
            return new SuccessDataResult<List<CarImage>>(_carImageDal.GetAll());
        }

        public IDataResult<List<CarImage>> GetAllByCarId(int carId)
        {
            return new SuccessDataResult<List<CarImage>>(
                _carImageDal.GetAll(i => i.CarId == carId)
            );
        }

        public IDataResult<CarImage> GetById(int id)
        {
            return new SuccessDataResult<CarImage>(_carImageDal.Get(i => i.Id == id));
        }

        public IResult Update(IFormFile file, CarImage carImage)
        {
            var imagePath = _carImageDal.Get(i => i.Id == carImage.Id).ImagePath;

            _fileHelper.Update(
                file,
                imagePath,
                CarImagesPathConstants.RootPath + CarImagesPathConstants.FilePath
            );

            _carImageDal.Update(carImage);

            return new SuccessResult();
        }

        private IResult CheckIfImageCountExceeded(int carId)
        {
            var images = _carImageDal.GetAll(i => i.CarId == carId);

            if (images.Count >= 5)
            {
                return new ErrorResult();
            }

            return new SuccessResult();
        }
    }
}
