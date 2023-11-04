﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCiro.Core.Utilities.Concrete
{
    public class ErrorDataResult<T> : DataResult<T>
    {
        public ErrorDataResult(T data) : base(data, false)
        {
        }
        public ErrorDataResult(T data, string messsage) : base(data, false, messsage)
        {
        }
        public ErrorDataResult(string messsage) : base(default, false, messsage)
        {
        }
    }
}
