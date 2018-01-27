using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz
{
    public class BizBadRequestException : Exception
    {
        public BizBadRequestException()
            : base("We are sorry, the page you have requested cannot be found. The URL may be misspelled or the page you're looking for is no longer available.")
        {

        }
        public BizBadRequestException(string message)
            : base(message)
        {
            
        }
    }
}