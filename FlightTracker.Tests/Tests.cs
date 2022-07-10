using System.Windows.Input;
using FlightTracker.DataAccess;
using FlightTracker.Models;
using FlightTracker.Validation;
using FlightTracker.ViewModels;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Xamarin.Essentials;


namespace FlightTracker.Tests
{
    [TestClass]
    public class Tests
    {
        public ICommand SaveCommand { get; }
        private IParaglidingDbContext dbContext = new ParaglidingDbContext();

        [TestMethod]
        public void TestCommentValidation()
        {
            //Arrange
            ValidationService validation = new ValidationService();
            string teststring = "abc";
            string expectedAnswer = "";
            //Act
            var result = validation.IsCommentOutOfRange(teststring.Length);
            // Assert
            Assert.AreEqual(expectedAnswer, result);
        }

        [TestMethod]
        public async Task TestSaveNewUser()
        {
            //Arrange
            UserViewModel userViewmodel = new UserViewModel();

            //Act
            await dbContext.Pilots.AddAsync(new DataAccess.Entities.Pilot
            {
                FirstName = "TestFirstName",
                LastName = "TestLastName",
                Username = "TestUsername",
                Role = Role.User,
                Password = "Password",
            });
            await dbContext.SaveAsync();
            Thread.Sleep(150);

            // Assert
            var isUserNameInDB =
                (await dbContext.Pilots.FirstOrDefaultAsync(p => EF.Functions.Like(p.Username, $"TestUsername"))) != null;
            
            Assert.AreEqual("TestUsername", isUserNameInDB);//Not possible -> No local DB-Instance running
        }

        [TestMethod]
        public void GetNewSession()
        {
            //Arrange
            Preferences.Set(nameof(Session), new Session("Test", DateTime.UtcNow, "Test", Role.User).ToString());

            //Act
            var session = Preferences.Get(nameof(Session), null);

            // Assert
            Assert.AreEqual(Session.Get(), session); //Not possible due to different .net-Targets
        }


    }
}