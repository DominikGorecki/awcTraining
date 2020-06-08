using System;
using System.Collections.Generic;
using System.Text;

namespace AWC.TrainingEvents.Abstract.IModels
{
    /// <summary>
    /// Shared implementation of IResponse. If response business logic is required, we can always
    /// define our own IResponse in the module we need it. 
    /// </summary>
    /// <typeparam name="T">Any reference type</typeparam>
    public class Response<T> : IResponse<T>
        where T: class 
        // We want to limit it to reference classes because 
        // 1) Response<T> doesn't support string type and 
        // 2) encourage better response types in implementation details
    {
        protected Response() { }

        public Response(T val)
            => Value = val;

        public Response(string errorMessage)
            => new List<string> { errorMessage };

        public Response(IEnumerable<string> errorSummary)
            => ErrorSummary = errorSummary;

        public T Value { get; private set; } 

        public bool IsSuccess { get => ErrorSummary is null; }

        public bool IsError => !IsSuccess;

        public IEnumerable<string> ErrorSummary { get; private set; }
    }
}
