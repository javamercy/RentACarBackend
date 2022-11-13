using Entities.concretes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.abstracts
{
    public interface IBrandDal
    {
        void Add(Brand brand);

        void Update(Brand brand);

        void Delete(Brand brand);

        List<Brand> GetAll();
    }
}
