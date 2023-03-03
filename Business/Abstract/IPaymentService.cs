using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Models;

namespace Business.Abstract
{
    public interface IPaymentService
    {
        IResult Pay(CreditCard creditCard, int amount);

        IResult Add(Payment payment);
    }
}
