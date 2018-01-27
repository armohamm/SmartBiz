using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz
{
    public class BizSecurityException : Exception
    {
        public BizSecurityException(string message) : base(message)
        {
            
        }
    }
}