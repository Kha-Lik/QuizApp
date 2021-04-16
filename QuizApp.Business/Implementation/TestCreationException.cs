using System;
using System.Runtime.Serialization;

namespace QuizApp.Business.Implementation
{
    [Serializable]
    public class TestCreationException : Exception
    {
        private const string DefaultMessage = "Test creation exception has occured.";
        
        public TestCreationException() : base(DefaultMessage)
        {
        }

        public TestCreationException(string message) 
            : base(message)
        {
        }

        public TestCreationException(string message, Exception innerException) 
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected TestCreationException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}