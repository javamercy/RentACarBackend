using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfRentalDal : EfEntityRepositoryBase<Rental, RentACarContext>, IRentalDal
    {
        public List<RentalDetailDto> GetAllByDetails()
        {
            using (var context = new RentACarContext())
            {
                var result =
                    from rental in context.Rentals
                    join customer in context.Customers on rental.CustomerId equals customer.Id
                    join user in context.Users on customer.UserId equals user.Id
                    join car in context.Cars on rental.CarId equals car.Id
                    select new RentalDetailDto
                    {
                        RentalId = rental.Id,
                        CarId = car.Id,
                        CustomerId = customer.Id,
                        CarDescription = car.Description,
                        CustomerFullName = $"{user.FirstName} {user.LastName}",
                        RentDate = rental.RentDate,
                        ReturnDate = rental.ReturnDate
                    };

                return result.ToList();
            }
        }
    }
}
