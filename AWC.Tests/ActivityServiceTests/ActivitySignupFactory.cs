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
    public class ActivitySignupFactory
    {
        // We make these public so that we can use them in our tests
        public readonly static Guid TEST_GUID = new Guid("cdea9737-78bd-416c-b980-f80486cbc5b6");
        public readonly static string FIRST_NAME = "FirstName";
        public readonly static string LAST_NAME = "FirstName";
        public readonly static int TIME_OF_DAY = 12 * 60; // 12 noon
        public readonly static DateTime START_DATE = DateTime.Now.AddDays(7); // start in 7 days from today
        public readonly static string EMAIL = "test@test.test";
        public readonly static int YEARS_EXPERIENCE = 3;
        public readonly static string COMMENTS = "comment";
        private readonly IActivity _activity;

        public ActivitySignupFactory(IActivity activity)
        {
            _activity = activity;
        }


        public IActivitySignup NewValid()
        {
            var validMock = NewMock();

            return validMock.Object;
        }

        // Returns a valid mock that we can customize in tests
        public Mock<IActivitySignup> NewMock()
        {
            var validMock = new Mock<IActivitySignup>();
            validMock.Setup(s => s.Id).Returns(TEST_GUID);
            validMock.Setup(s => s.FirstName).Returns(FIRST_NAME);
            validMock.Setup(s => s.LastName).Returns(LAST_NAME);
            validMock.Setup(s => s.TimeOfDayMinutes).Returns(TIME_OF_DAY);
            validMock.Setup(s => s.PrefferedStart).Returns(START_DATE);
            validMock.Setup(s => s.Email).Returns(EMAIL);
            validMock.Setup(s => s.YearsExperience).Returns(YEARS_EXPERIENCE);
            validMock.Setup(s => s.Comments).Returns(COMMENTS);
            validMock.Setup(s => s.Activity).Returns(_activity);
            return validMock;
        } 

        public IActivity NullProp(
            bool FirstNameNull,
            bool LastNameNull,
            bool EmailNull,
            bool CommentNull,
            bool ActivityNull)
        {
            throw new NotImplementedException();

        }



        /*
        public IActivitySignup ValidCustomName(string firstName, string lastName, bool hasGuid = false)
        {
            
        }
        */


    }
}
