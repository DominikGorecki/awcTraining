using System;
using System.Collections.Generic;
using System.Text;

namespace AWC.TrainingEvents.Abstract.IModels
{
    public interface IResponse<T>
    {
        T Value { get; }
        bool IsSuccess { get; }
        bool IsError { get; } // Seems redundant but it will make for more readable code and very little extra implementaiton code.
        IEnumerable<string> ErrorSummary { get; }
    }
}
