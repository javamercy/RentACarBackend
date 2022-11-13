using Entities.abstracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.concretes
{
    public class Brand: IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }
}
