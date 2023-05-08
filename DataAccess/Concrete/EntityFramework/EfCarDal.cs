using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetAllByDetails(
            Expression<Func<CarDetailDto, bool>> filter = null
        )
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result =
                    from car in context.Cars
                    join color in context.Colors on car.ColorId equals color.Id
                    join brand in context.Brands on car.BrandId equals brand.Id
                    join model in context.Models on car.ModelId equals model.Id
                    select new CarDetailDto
                    {
                        CarId = car.Id,
                        BrandId = brand.Id,
                        BrandName = brand.Name,
                        ColorId = color.Id,
                        ColorName = color.Name,
                        ModelId = model.Id,
                        ModelName = model.Name,
                        ModelYear = car.ModelYear,
                        CarDescription = car.Description,
                        DailyPrice = car.DailyPrice,
                        ImagePaths = new List<string>(
                            from carImage in context.CarImages
                            where carImage.CarId == car.Id
                            select carImage.ImagePath
                        ).ToList()
                    };

                return filter == null ? result.ToList() : result.Where(filter).ToList();
            }
        }
    }
}
