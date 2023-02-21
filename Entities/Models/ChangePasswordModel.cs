using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class ChangePasswordModel : IModel
    {
        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string Email { get; set; }
    }
}
