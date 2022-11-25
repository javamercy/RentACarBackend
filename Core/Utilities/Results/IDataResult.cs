using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Utilities.Results
{
    public interface IDataResult<T>
    {
        public T Data { get; set; }

        public string Message { get; set; }

        public bool Success { get; set; }




    }
}
