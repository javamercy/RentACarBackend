using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IUserService
    {
        IResult Add(User user);

        IResult Delete(User user);

        IResult Update(User user);

        IDataResult<List<User>> GetAll();

        IDataResult<User> GetById(int id);

        IDataResult<List<User>> GetAllByFirstName(string firstName);

        IDataResult<List<User>> GetAllByLastName(string lastName);

        IDataResult<User> GetByEmail(string email);
    }
}
