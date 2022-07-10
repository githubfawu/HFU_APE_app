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
            CryptoService crypto = new CryptoService();
            //Act
            var result = crypto.Encrypt("admin");
            string expectedAnswer = "adminencrypted"; //Not possible to expect the encrypted value
            // Assert
            Assert.AreEqual(expectedAnswer, result);
        }

        [TestMethod]
        public void DecryptUserName()
        {
            //ICrypto crypto = DependencyService.Get<ICrypto>();
            CryptoService crypto = new CryptoService();
            //Act
            var result = crypto.Decrypt("pfVYHp4FvUMtx/WKnZ6Uc2o+L71jX295Gx4tYThSBA==");
            string expectedAnswer = "admin";
            // Assert
            Assert.AreEqual(expectedAnswer, result);
        }
    }
}