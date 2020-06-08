using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWC.TrainingEventsWeb.Models
{
    public class ActivityVM
    {
        /// <summary>
        /// No need to use a mapper for this obj b/c it's so simple--easier to just
        /// do it in the ctor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        public ActivityVM(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
    }
}
