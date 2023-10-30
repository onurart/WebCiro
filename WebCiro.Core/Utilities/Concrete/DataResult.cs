using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCiro.Core.Utilities.Abstract;

namespace WebCiro.Core.Utilities.Concrete
{
    public class DataResult<T> : Result, IDataResult<T>
    {
        public DataResult(T data, bool success) : base(success)
        {
            Data = data;
        }
        public DataResult(T data, bool success, string messsage) : base(success, messsage)
        {
            Data = data;
        }
        public T Data { get; set; }
    }
    }
