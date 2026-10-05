// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PingPongService2Tests.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the PingPongService2Tests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TheArtOfUnitTesting.Services.UnitTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using TheArtOfUnitTesting.Services;

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class PingPongService2Tests
    {
        private class FakeWinRepository : IWinRepository
        {
            public string GetWin() => "Win" == null ? string.Empty : "Win";
        }

        [TestCase("Ping", "PingPong")]
        [TestCase("Pong", "PingPong")]
        [TestCase("PingPong", "PingPongWin")]
        [TestCase("AAAAA", "Fault")]
        public void PingPongService5_GetPingPongGame_When_getting_with_action_then_gets_action_and_win(
            string action,
            string actionResult)
        {
            // Arrange  
            var service = new PingPongService2(new FakeWinRepository());

            // Act  
            var result = service.GetPingPongGame(action);

            // Assert  
            Assert.That(result, Is.EqualTo(actionResult));
        }

        [Test]
        public void PingPongService5_GetPingPongGame_When_action_is_null_then_throws_error()
        {
            // AAA  
            Assert.Throws<ArgumentNullException>(() => new PingPongService2(new FakeWinRepository()).GetPingPongGame(null));
        }
    }
}
