using FlightTracker.Converters;
using FlightTracker.Validation;

namespace FlightTracker.Tests
{
    [TestClass]
    public class LogicTests
    {
        [TestMethod]
        public void TestCommentValidation()
        {
            //Arrange
            FlightValidation validation = new FlightValidation();
            string teststring = "abc";
            string expectedAnswer = "";
            //Act
            var result = validation.IsCommentOutOfRange(teststring.Length);
            // Assert
            Assert.AreEqual(expectedAnswer, result);
        }
        
        [TestMethod]
        public void tbd()
        {
          
        }
    }
}