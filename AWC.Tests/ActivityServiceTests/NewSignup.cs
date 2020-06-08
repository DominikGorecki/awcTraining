using AWC.TrainingEvents.Abstract.IModels;
using AWC.TrainingEvents.Abstract.IRepositories;
using AWC.TrainingEvents.ActivityService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWC.Tests.ActivityServiceTests
{
    [TestClass]
    public class NewSignup
    {
        private readonly Mock<IActivityData> _mockActivityData;
        private readonly ActivityFactory _activityFactory;
        private readonly ActivitySignupFactory _activitySignupFactory;

        public NewSignup()
        {
            _activityFactory = new ActivityFactory();
            _activitySignupFactory = new ActivitySignupFactory(_activityFactory.NewValid());
            var successResponseMock = new Mock<IResponse<IActivitySignup>>();
            successResponseMock.Setup(r => r.Value).Returns(_activitySignupFactory.NewValid());
            successResponseMock.Setup(r => r.IsError).Returns(false);
            successResponseMock.Setup(r => r.IsSuccess).Returns(true);

            // Since we're testing signup we want the activity data to always return true
            // for most cases
            _mockActivityData = new Mock<IActivityData>();
            _mockActivityData.Setup(
                    ad => ad.UpsertSignup(It.IsAny<IActivitySignup>())
                ).Returns(
                    Task.FromResult(successResponseMock.Object));
        }

        [TestMethod]
        // This will cover the valid test case with all the existing props.
        // Negative test cases will test individual props.
        public async Task ValidCase()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);

            var activityMock = _activitySignupFactory.NewMock();
            activityMock.Setup(a => a.Id).Returns(Guid.Empty);
            var activitySignup = activityMock.Object; 

            // Act
            var newSignupResult = await activityService.NewSignup(activitySignup);

            // Assert -- in attribute
            Assert.IsTrue(newSignupResult.IsSuccess);
            Assert.IsFalse(newSignupResult.IsError);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        // New signups shuldn't have a guid
        public async Task Id_NonEmpty()
        {
            // Arrange
            var activitySignup = _activitySignupFactory.NewValid();
            // Let's make sure our test is setup properly and we have a GUi
            Assert.AreNotEqual(Guid.Empty, activitySignup.Id);
            var activityService = new ActivityService(_mockActivityData.Object);

            // Act
            await activityService.NewSignup(activitySignup); 

            // Assert -- in attribute
        }

        [TestMethod]
        public async Task FirstName_Null()
        {
            // Arrange
            var validActivityMock = _activitySignupFactory.NewMock();
            var activityService = new ActivityService(_mockActivityData.Object);

            validActivityMock.Setup(a => a.FirstName).Returns<string>(null);
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty);

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // We should only have one error -- invalid Name
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        public async Task LastName_Null()
        {
            // Arrange
            var validActivityMock = _activitySignupFactory.NewMock();
            var activityService = new ActivityService(_mockActivityData.Object);

            validActivityMock.Setup(a => a.LastName).Returns<string>(null);
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty);

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // We should only have one error -- invalid Name
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task FLName_Empty()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);
            var validActivityMock = _activitySignupFactory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup
            validActivityMock.Setup(a => a.FirstName).Returns("");
            validActivityMock.Setup(a => a.LastName).Returns("");

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Both First and Lastname are invalid--should have two errors
            Assert.AreEqual(2, signupResult.ErrorSummary.Count());
        }


        [TestMethod]
        // To save time let's do both F and L name together
        public async Task FLName_WhiteSpace()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);
            var validActivityMock = _activitySignupFactory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup
            validActivityMock.Setup(a => a.FirstName).Returns("   ");
            validActivityMock.Setup(a => a.LastName).Returns("    ");

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Both First and Lastname are invalid--should have two errors
            Assert.AreEqual(2, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task FLName_TooLong()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);
            var validActivityMock = _activitySignupFactory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup

            var tooLongString = new string('A', 51);

            validActivityMock.Setup(a => a.FirstName).Returns(tooLongString);
            validActivityMock.Setup(a => a.LastName).Returns(tooLongString);

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Both First and Lastname are invalid--should have two errors
            Assert.AreEqual(2, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task Email_Invalid()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);
            var validActivityMock = _activitySignupFactory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup

            validActivityMock.Setup(a => a.Email).Returns("NOTVALIDEMAIL");

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task Email_Null()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);
            var validActivityMock = _activitySignupFactory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup

            validActivityMock.Setup(a => a.Email).Returns<string>(null);

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task Email_Empty()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);
            var validActivityMock = _activitySignupFactory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup

            validActivityMock.Setup(a => a.Email).Returns("");

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task Email_WhiteSpace()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);
            var validActivityMock = _activitySignupFactory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup

            validActivityMock.Setup(a => a.Email).Returns("  ");

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task Start_BeforeToday()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);
            var validActivityMock = _activitySignupFactory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup

            validActivityMock.Setup(a => a.PrefferedStart).Returns(DateTime.MinValue);

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task TimeOfDay_Negative()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);
            var validActivityMock = _activitySignupFactory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup

            validActivityMock.Setup(a => a.TimeOfDayMinutes).Returns(-1);

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task TimeOfDay_TooLarge()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);
            var validActivityMock = _activitySignupFactory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup

            validActivityMock.Setup(a => a.TimeOfDayMinutes).Returns(24*60+1);

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task Experience_Negative()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);
            var validActivityMock = _activitySignupFactory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup

            validActivityMock.Setup(a => a.YearsExperience).Returns(-1);

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task Experience_Over100()
        {
            // Arrange
            var activityService = new ActivityService(_mockActivityData.Object);
            var validActivityMock = _activitySignupFactory.NewMock();
            validActivityMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup

            validActivityMock.Setup(a => a.YearsExperience).Returns(101);

            // Act
            var signupResult = await activityService.NewSignup(validActivityMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task Activity_NewNull()
        {
            // Arrange

            var activityMock = _activityFactory.NewMock();
            activityMock.Setup(a => a.Id).Returns(Guid.Empty);
            activityMock.Setup(a => a.Name).Returns<string>(null);

            var activitySignupService = new ActivityService(_mockActivityData.Object);
            var validActivitySignupMock = new ActivitySignupFactory(activityMock.Object).NewMock();
            validActivitySignupMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup


            // Act
            var signupResult = await activitySignupService.NewSignup(validActivitySignupMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task Activity_NewEmpty()
        {
            // Arrange

            var activityMock = _activityFactory.NewMock();
            activityMock.Setup(a => a.Id).Returns(Guid.Empty);
            activityMock.Setup(a => a.Name).Returns("");

            var activitySignupService = new ActivityService(_mockActivityData.Object);
            var validActivitySignupMock = new ActivitySignupFactory(activityMock.Object).NewMock();
            validActivitySignupMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup


            // Act
            var signupResult = await activitySignupService.NewSignup(validActivitySignupMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task Activity_NewWhiteSpace()
        {
            // Arrange

            var activityMock = _activityFactory.NewMock();
            activityMock.Setup(a => a.Id).Returns(Guid.Empty);
            activityMock.Setup(a => a.Name).Returns("  ");

            var activitySignupService = new ActivityService(_mockActivityData.Object);
            var validActivitySignupMock = new ActivitySignupFactory(activityMock.Object).NewMock();
            validActivitySignupMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup


            // Act
            var signupResult = await activitySignupService.NewSignup(validActivitySignupMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task Activity_NewTooLong()
        {
            // Arrange

            var activityMock = _activityFactory.NewMock();
            activityMock.Setup(a => a.Id).Returns(Guid.Empty);
            activityMock.Setup(a => a.Name).Returns(new string('a',21));

            var activitySignupService = new ActivityService(_mockActivityData.Object);
            var validActivitySignupMock = new ActivitySignupFactory(activityMock.Object).NewMock();
            validActivitySignupMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup


            // Act
            var signupResult = await activitySignupService.NewSignup(validActivitySignupMock.Object);

            // Assert
            Assert.IsFalse(signupResult.IsSuccess);
            Assert.IsTrue(signupResult.IsError);
            // Expecting only error from email;
            Assert.AreEqual(1, signupResult.ErrorSummary.Count());
        }

        [TestMethod]
        // To save time let's do both F and L name together
        public async Task Activity_NewValid()
        {
            // Arrange

            var activityMock = _activityFactory.NewMock();
            activityMock.Setup(a => a.Id).Returns(Guid.Empty);
            activityMock.Setup(a => a.Name).Returns("VALID NAME");

            var activitySignupService = new ActivityService(_mockActivityData.Object);
            var validActivitySignupMock = new ActivitySignupFactory(activityMock.Object).NewMock();
            validActivitySignupMock.Setup(a => a.Id).Returns(Guid.Empty); // required for new signup


            // Act
            var signupResult = await activitySignupService.NewSignup(validActivitySignupMock.Object);

            // Assert
            Assert.IsTrue(signupResult.IsSuccess);
            Assert.IsFalse(signupResult.IsError);
        }

    }
}
