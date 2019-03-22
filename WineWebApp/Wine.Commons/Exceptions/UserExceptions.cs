using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Wine.Commons.Exceptions
{
    public class UserExceptions : Exception
    {
        public UserExceptions(string message) : base(message)
        {
            Trace.TraceError(message);
        }
    }
}
