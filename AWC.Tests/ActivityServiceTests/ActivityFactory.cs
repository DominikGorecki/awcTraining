using AWC.TrainingEvents.Abstract.IModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWC.Tests.ActivityServiceTests
{
    /// <summary>
    /// All valid property values are exposed through a readonly static field. Factory provides way to customize
    /// mock to test for negative or indvalid cases
    /// </summary>
    public class ActivityFactory
    {
        // We make these public so we can use them in our tests to verify... we also don't want a random GUID so we can test consistency
        public readonly static Guid TEST_GUID = new Guid("290a5b29-6ae7-4ec0-9e4f-0138fd2fec9e");
        public readonly static string NAME = "Name";

        public IActivity NewValid()
        {
            var validMock = NewMock();
            return validMock.Object;
        }

        public Mock<IActivity> NewMock()
        {
            var validMock = new Mock<IActivity>();
            validMock.Setup(a => a.Id).Returns(TEST_GUID);
            validMock.Setup(a => a.Name).Returns(NAME);
            return validMock;
        }
    }
}
