using Business.concretes;
using DataAccess.concretes.inMemory;
using Entities.concretes;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            BrandManager brandManager = new BrandManager(new InMemoryBrandDal());

            brandManager.Add(new Brand { Id = 5, Name = "Porsche" });

            brandManager.Delete(new Brand { Id = 1 ,Name="BMW"});

            brandManager.Update(new Brand { Id = 2, Name = "Updated" });

            foreach (var brand in brandManager.GetAll())
            {
                Console.WriteLine("{0} - {1}", brand.Id, brand.Name);
            }


        }
    }
}
