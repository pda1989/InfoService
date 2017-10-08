//using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfoService.Implementations;
using InfoService.Models;
using Moq;
using NUnit.Framework;

namespace InfoService.Tests
{
    [TestFixture]
    public class WebServerTests
    {
        [Test]
        [Ignore("Developer test")]
        public void PerformeCommand()
        {
            // Arrange
            var converter = new JsonSerialyzer();
            var handler = new MessageHandler(converter);
            handler.AddCommand(new GetInfoCommand(null, new PCInfo(), converter));

            var mock = new Mock<WebServer>(handler, null, null);
            mock.Setup(server => server.RequestMessage())
                .Returns("{\"Command\":\"GetInfo\"}");
            mock.Setup(server => server.SendResult(It.IsAny<string>()));

            // Act
            mock.Object.PerformCommand();

            // Assert
            Assert.IsTrue(true);
        }
    }
}