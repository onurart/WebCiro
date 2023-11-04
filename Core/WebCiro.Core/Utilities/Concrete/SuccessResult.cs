using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCiro.Core.Utilities.Concrete
{
    public class SuccessResult : Result
    {
        public SuccessResult() : base(true)
        {
        }
        public SuccessResult(string message) : base(true, message)
        {
        }
    }
}
