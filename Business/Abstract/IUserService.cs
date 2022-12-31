using Core.Entities.Concrete;
using Core.Utilities.Results;
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

        IDataResult<List<OperationClaim>> GetClaims(User user);

        IDataResult<User> GetByMail(string email);
    }
}
