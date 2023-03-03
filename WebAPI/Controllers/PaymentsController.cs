using Business.Abstract;
using Entities.Concrete;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }



        [HttpPost("pay")]
        public IActionResult Pay([FromBody] CreditCard creditCard, [FromQuery] int amount)
        {
            var result = _paymentService.Pay(creditCard, amount);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(Payment payment)
        {
            var result = _paymentService.Add(payment);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


    }
}
