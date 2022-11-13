using DataAccess.abstracts;
using Entities.concretes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.concretes.inMemory
{
    public class InMemoryBrandDal : IBrandDal
    {
        private List<Brand> _brands;

        public InMemoryBrandDal()
        {
            _brands = new List<Brand> {

                new Brand{ Id = 1, Name = "BMW"},
                new Brand{ Id = 2, Name = "Mercedes"},
                new Brand{ Id = 3, Name = "Audi"},
                new Brand{ Id = 4, Name = "Renault"}
            };                          
        }
        public void Add(Brand brand)
        {
            _brands.Add(brand);
        }

        public void Delete(Brand brand)
        {
            _brands.Remove(_brands.Find(b => b.Id == brand.Id));

        }

        public List<Brand> GetAll()
        {
            return _brands;
        }

        public void Update(Brand brand)
        {
            var brandToUpdate = _brands.Find(b => b.Id == brand.Id);

            brandToUpdate.Name = brand.Name;
        }
    }
}
