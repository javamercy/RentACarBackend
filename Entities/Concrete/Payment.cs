using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Payment : IEntity
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string CardNumber { get; set; }

        public int ExpiringYear { get; set; }

        public int ExpiringMonth { get; set; }

        public int CardVerificationCode { get; set; }
    }
}
