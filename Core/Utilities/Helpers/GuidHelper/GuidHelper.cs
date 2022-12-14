using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Helpers.GuidHelper
{
    public class GuidHelper
    {
        public static string GetNewGuid()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
