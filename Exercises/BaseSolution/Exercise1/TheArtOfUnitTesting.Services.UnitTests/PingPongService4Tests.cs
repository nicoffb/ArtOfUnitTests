namespace TheArtOfUnitTesting.Services.UnitTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using TheArtOfUnitTesting.Services;

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class PingPongService4Tests
    {
        [TestCase("Ping", "PingPong")]
        [TestCase("Pong", "PingPong")]
        [TestCase("PingPong", "PingPongWin")]
        [TestCase("AAAAA", "Fault")]
        public void PingPongService4_GetPingPongGame_When_getting_with_action_then_gets_action_and_win(
            string action,
            string actionResult)
        {
            // Arrange
            var service = new PingPongService4();

            // Act
            var result = service.GetPingPongGame(action);

            // Assert
            Assert.That(result, Is.EqualTo(actionResult));
        }

        [Test]
        public void PingPongService4_GetPingPongGame_When_action_is_null_then_throws_error()
        {
            // AAA
            Assert.Throws<ArgumentNullException>(() => new PingPongService4().GetPingPongGame(null));
        }
    }
}
