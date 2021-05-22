using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace QuizApp.Business.Implementation.Exceptions
{
    [Serializable]
    public class LoginException : Exception
    {
        private const string DefaultMessage = "Login is failed.";

        public LoginException() : base(DefaultMessage)
        {
        }

        public LoginException(string message)
            : base(message)
        {
        }

        public LoginException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected LoginException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
