using System;
using System.Collections.Generic;
using System.Text;

namespace AWC.TrainingEvents.Abstract.IModels
{
    /// <summary>
    /// Right now it's a simple model, but this will allow us to group people by activity
    /// and to also do activity search, text completion, etc. 
    /// </summary>
    public interface IActivity
    {
        Guid Id { get; }
        string Name { get; }
    }
}
