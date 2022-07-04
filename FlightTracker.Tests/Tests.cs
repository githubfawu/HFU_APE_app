using FlightTracker.Converters;
using FlightTracker.DataAccess;
using FlightTracker.DataAccess.Entities;
using FlightTracker.Validation;
using FlightTracker.ViewModels;
using Xamarin.Forms;

namespace FlightTracker.Tests
{
    [TestClass]
    public class LogicTests
    {
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
        public void EncryptUserName()
        {
            //ICrypto crypto = DependencyService.Get<ICrypto>();
            Crypto crypto = new Crypto();
            //Act
            var result = crypto.Encrypt("Hallo Velo");
            string expectedAnswer = "x1Rdms3cVsOeiSn0448FXAKf1kS5s/odFyQactZX5TPzOMvwWa97/EsOvE+saH8="; //Not possible to expect the encrypted value
            // Assert
            Assert.AreEqual(expectedAnswer, result);
        }

        [TestMethod]
        public void DecryptUserName()
        {
            //ICrypto crypto = DependencyService.Get<ICrypto>();
            Crypto crypto = new Crypto();
            //Act
            var result = crypto.Decrypt("iIg0Gfd8IWZoY/U1a7Lj2m61YBo1YbKybCFd9WKQSXXB+osgszWbqguqynNnqNY=");
            string expectedAnswer = "Hallo Velo";
            // Assert
            Assert.AreEqual(expectedAnswer, result);
        }
    }
}