using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz
{
    public class BizValidationException : Exception
    {
        public BizValidationException(string message)
            : base(message)
        {
            
        }
    }
}