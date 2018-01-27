using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biz
{
    public class BizInformationException : Exception
    {
        public string Title = null;
        public BizInformationException(string title, string message)
            : base(message)
        {
            Title = title;
        }
    }
}