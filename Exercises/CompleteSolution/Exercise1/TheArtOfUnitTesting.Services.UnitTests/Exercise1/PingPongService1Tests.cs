namespace TheArtOfUnitTesting.Services.UnitTests.Exercise4
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;
 

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class PingPongService1Tests
    {
        [TestCase("Ping", "PingPong")]
        [TestCase("Pong", "PingPong")]
        [TestCase("PingPong", "PingPongWin")]
        [TestCase("AAAAA", "Fault")]
        public void PingPongService1_GetPingPongGame_When_getting_with_action_then_gets_action_and_win(
            string action,
            string actionResult)
        {
            // Arrange
            var service = new PingPongService1();

            // Act
            var result = service.GetPingPongGame(action);

            // Assert
            Assert.That(result, Is.EqualTo(actionResult));
        }

        [Test]
        public void PingPongService4_GetPingPongGame_When_action_is_null_then_throws_error()
        {
            // AAA
            Assert.Throws<ArgumentNullException>(() => new PingPongService1().GetPingPongGame(null));
        }
    }
}