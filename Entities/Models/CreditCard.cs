using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class CreditCard : IModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CardNumber { get; set; }

        public int ExpiringYear { get; set; }

        public int ExpiringMonth { get; set; }

        public string CardVerificationCode { get; set; }
    }
}
