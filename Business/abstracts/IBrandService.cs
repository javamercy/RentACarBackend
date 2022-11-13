using Entities.concretes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.abstracts
{
   public interface IBrandService
    {
        void Add(Brand brand);

        void Delete(Brand brand);

        void Update(Brand brand);

        List<Brand> GetAll();

    }
}
