using Core.Entities.Abstract;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    public class Payment : IEntity
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

    }
}
