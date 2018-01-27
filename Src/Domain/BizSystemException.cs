using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz
{
    public class BizSystemException : Exception
    {
        public BizSystemException(string message)
            : base(message)
        {
            
        }
    }
}