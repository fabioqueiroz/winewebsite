using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Text;

namespace Wine.Commons.Exceptions
{
    [Serializable]
    public class ItemNotFoundExceptions : Exception
    {

        public ItemNotFoundExceptions()
        {
            Trace.TraceInformation("The item has not been found");
        }

        public ItemNotFoundExceptions(string message) : base(message)
        {
            Trace.TraceInformation(message);
        }

        public ItemNotFoundExceptions(string message, Exception innerException) : base(message, innerException)
        {
            Trace.TraceError(message, innerException);
        }

        // Used to locate an error in the stream
        protected ItemNotFoundExceptions(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Trace.TraceInformation("Item not found", context);
        }

        //public ItemNotFoundException()
        //    : base() { }

        //public ItemNotFoundException(string message)
        //    : base(message) { }

        //public ItemNotFoundException(string format, params object[] args)
        //    : base(string.Format(format, args)) { }

        //public ItemNotFoundException(string message, Exception innerException)
        //    : base(message, innerException) { }

        //public ItemNotFoundException(string format, Exception innerException, params object[] args)
        //    : base(string.Format(format, args), innerException) { }

        //protected ItemNotFoundException(SerializationInfo info, StreamingContext context)
        //    : base(info, context) { }

    }
}
