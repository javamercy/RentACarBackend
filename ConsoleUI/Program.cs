using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            BrandManager brandManager = new BrandManager(new EfBrandDal());

            ColorManager colorManager = new ColorManager(new EfColorDal());

            CarManager carManager = new CarManager(new EfCarDal());

            //brandManager.Add(new Brand { Name = "Porsche" });
            //brandManager.Add(new Brand { Name = "BMW" });
            //brandManager.Add(new Brand { Name = "Mercedes" });

            //colorManager.Add(new Color { Name = "blue" });
            //colorManager.Add(new Color { Name = "black" });
            //colorManager.Add(new Color { Name = "red" });

            //carManager.Add(new Car { BrandId = 8, ColorId = 1, DailyPrice = 257.99, Description = "BMW i8", ModelYear = 2015 });


            var result = carManager.GetAllCarsByDetails();

            foreach (var carDetail in result.Data)
            {
                Console.WriteLine("CarId: " + carDetail.CarId);
                Console.WriteLine("CarDescription: " + carDetail.CarDescription);
                Console.WriteLine("BrandName: " + carDetail.BrandName);
                Console.WriteLine("ColorName: " + carDetail.ColorName);
            }
        }
    }
}
