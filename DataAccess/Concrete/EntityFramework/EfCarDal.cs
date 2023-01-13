using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCarDal : EfEntityRepositoryBase<Car, RentACarContext>, ICarDal
    {
        public List<CarDetailDto> GetAllByDetails()
        {
            using (RentACarContext context = new RentACarContext())
            {
                var result =
                    from car in context.Cars
                    join color in context.Colors on car.ColorId equals color.Id
                    join brand in context.Brands on car.BrandId equals brand.Id
                    select new CarDetailDto
                    {
                        CarId = car.Id,
                        BrandId = brand.Id,
                        ColorId = color.Id,
                        BrandName = brand.Name,
                        ColorName = color.Name,
                        ModelYear = car.ModelYear,
                        CarDescription = car.Description,
                        DailyPrice = car.DailyPrice,
                    };

                return result.ToList();
            }
        }
    }
}
