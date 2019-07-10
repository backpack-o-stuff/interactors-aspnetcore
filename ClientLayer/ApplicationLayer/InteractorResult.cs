using System;
using System.Collections.Generic;

namespace BOS.ClientLayer.ApplicationLayer
{
    public class InteractorResult<T>
        where T : class
    {
        public InteractorResult()
        {
            Messages = new List<string>();
        }

        public bool Success { get; set; }
        public List<string> Messages { get; set; }
        public T Result { get; set; }
        public Exception Exception { get; set; }

        public static InteractorResult<T> ForSuccess(T result)
        {
            return new InteractorResult<T>
            {
                Success = true,
                Result = result
            };
        }
        
        public static InteractorResult<T> ForFailure(string message)
        {
            var messages = new List<string> { message };
            return ForFailure(messages);
        }
        
        public static InteractorResult<T> ForFailure(List<string> messages)
        {
            return new InteractorResult<T>
            {
                Success = false,
                Messages = messages
            };
        }
        
        public static InteractorResult<T> ForFailure(Exception exception)
        {
            var messages = new List<string> { exception.Message };
            return new InteractorResult<T>
            {
                Success = false,
                Messages = messages,
                Exception = exception
            };
        }
    }
}