using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Business.Abstract
{
    public interface ICarImageService
    {
        IResult Add(IFormFile file, CarImage carImage);

        IResult AddMultiple(IFormFile[] files, CarImage carImage);

        IResult Delete(CarImage carImage);

        IResult Update(IFormFile file, CarImage carImage);

        IDataResult<List<CarImage>> GetAll();

        IDataResult<List<CarImage>> GetAllByCarId(int carId);

        IDataResult<CarImage> GetById(int id);
    }
}
