using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace AWC.Tests.ActivityServiceTests
{
    /// <summary>
    /// We should test our factory implementation to ensure our unit tests are valid
    /// </summary>

    [TestClass]
    public class FactoryTests
    {
        [TestMethod]
        public void ActivityFactory_Valid()
        {
            // Arrange
            var factory = new ActivityFactory();
            // Act
            var validActivity = factory.NewValid();
            // Assert
            Assert.AreEqual(ActivityFactory.NAME, validActivity.Name);
            Assert.AreEqual(ActivityFactory.TEST_GUID, validActivity.Id);
        }

        [TestMethod]
        public void ActivityFactory_CustomName()
        {
            // Arrange
            var factory = new ActivityFactory();
            // Act
            var validActivityMock = factory.NewMock();
            validActivityMock.Setup(a => a.Name).Returns("CustomName");
            var validActivity = validActivityMock.Object;

            // Assert
            Assert.AreEqual("CustomName", validActivity.Name);
            Assert.AreEqual(ActivityFactory.TEST_GUID, validActivity.Id);
        }

        [TestMethod]
        public void ActivityFactory_EmptyId()
        {
            // Arrange
            var factory = new ActivityFactory();
            // Act
            var validActivityMock = factory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty);
            var validActivity = validActivityMock.Object;

            // Assert
            Assert.AreEqual(ActivityFactory.NAME, validActivity.Name);
            Assert.AreEqual(Guid.Empty, validActivity.Id);
        }

        [TestMethod]
        public void ActivityFactory_CustomId()
        {
            // Arrange
            var factory = new ActivityFactory();
            var customId = Guid.NewGuid();
            // Act
            var validActivityMock = factory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(customId);
            var validActivity = validActivityMock.Object;

            // Assert
            Assert.AreEqual(ActivityFactory.NAME, validActivity.Name);
            Assert.AreEqual(customId, validActivity.Id);
        }


        [TestMethod]
        public void ActivitySignupFactory_NewValid()
        {
            // Arrange
            var validActivity = new ActivityFactory().NewValid();
            var signupFactory = new ActivitySignupFactory(validActivity);

            // Act
            var validSignup = signupFactory.NewValid();

            // Assert
            Assert.AreEqual(ActivitySignupFactory.COMMENTS, validSignup.Comments);
            Assert.AreEqual(ActivitySignupFactory.EMAIL, validSignup.Email);
            Assert.AreEqual(ActivitySignupFactory.FIRST_NAME, validSignup.FirstName);
            Assert.AreEqual(ActivitySignupFactory.LAST_NAME, validSignup.LastName);
            Assert.AreEqual(ActivitySignupFactory.START_DATE, validSignup.PrefferedStart);
            Assert.AreEqual(ActivitySignupFactory.TEST_GUID, validSignup.Id);
            Assert.AreEqual(ActivitySignupFactory.TIME_OF_DAY, validSignup.TimeOfDayMinutes);
            Assert.AreEqual(ActivitySignupFactory.YEARS_EXPERIENCE, validSignup.YearsExperience);
        }

        [TestMethod]
        public void ActivitySignupFactory_CustomId()
        {
            // Arrange
            var validActivity = new ActivityFactory().NewValid();
            var signupFactory = new ActivitySignupFactory(validActivity);
            var signupMock = signupFactory.NewMock();
            var customId = Guid.NewGuid();

            // Act
            signupMock.Setup(a => a.Id).Returns(customId);

            var customSignup = signupMock.Object; 

            // Assert
            Assert.AreEqual(customId, customSignup.Id);

            Assert.AreEqual(ActivitySignupFactory.COMMENTS, customSignup.Comments);
            Assert.AreEqual(ActivitySignupFactory.EMAIL, customSignup.Email);
            Assert.AreEqual(ActivitySignupFactory.FIRST_NAME, customSignup.FirstName);
            Assert.AreEqual(ActivitySignupFactory.LAST_NAME, customSignup.LastName);
            Assert.AreEqual(ActivitySignupFactory.START_DATE, customSignup.PrefferedStart);
            Assert.AreEqual(ActivitySignupFactory.TIME_OF_DAY, customSignup.TimeOfDayMinutes);
            Assert.AreEqual(ActivitySignupFactory.YEARS_EXPERIENCE, customSignup.YearsExperience);
        }

        [TestMethod]
        // Checking one other prop.. no need to check everything because our factory is pretty simple
        public void ActivitySignupFactory_CustomEmail()
        {
            // Arrange
            var validActivity = new ActivityFactory().NewValid();
            var signupFactory = new ActivitySignupFactory(validActivity);
            var signupMock = signupFactory.NewMock();

            // Act
            signupMock.Setup(a => a.Email).Returns("CUSTOM_EMAIL");
            var customSignup = signupMock.Object; 

            // Assert
            Assert.AreEqual("CUSTOM_EMAIL", customSignup.Email);

            Assert.AreEqual(ActivitySignupFactory.TEST_GUID, customSignup.Id);
            Assert.AreEqual(ActivitySignupFactory.COMMENTS, customSignup.Comments);
            Assert.AreEqual(ActivitySignupFactory.FIRST_NAME, customSignup.FirstName);
            Assert.AreEqual(ActivitySignupFactory.LAST_NAME, customSignup.LastName);
            Assert.AreEqual(ActivitySignupFactory.START_DATE, customSignup.PrefferedStart);
            Assert.AreEqual(ActivitySignupFactory.TIME_OF_DAY, customSignup.TimeOfDayMinutes);
            Assert.AreEqual(ActivitySignupFactory.YEARS_EXPERIENCE, customSignup.YearsExperience);
        }

    }
}
