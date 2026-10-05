// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PingPongService3Tests.cs" company="">
//   
// </copyright>
// <summary>
//   The unit test 1.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using NUnit.Framework;

namespace TheArtOfUnitTesting.Services.UnitTests.Exercise3.Solution
{
    /// <summary>
    /// The unit test 1.
    /// </summary>
    [TestFixture]
    public class PingPongService3Tests
    {
        #region Public Methods and Operators

        [TestCase("Ping", "PingPong")]
        [TestCase("Pong", "PingPong")]
        [TestCase("PingPong", "PingPongWin")] 
        [TestCase("AAAAA", "Fault")] 
        public void Exercise3_Solution_Get_ping_pong_game(string action, string actionResult)
        {
            // Arrange
            var winFileService = new WinFileServiceMock();
            var service = 
                new Services.Exercise3.Solution.DomainExerciseService3(winFileService);

            // Act
            var result = service.GetPingPongGame(action);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result == actionResult);
        }

        [TestCase("Ping", "PingPong")]
        [TestCase("Pong", "PingPong")]
        [TestCase("PingPong", "PingPongWin")]
        [TestCase("AAAAA", "Fault")]
        public void Exercise3_Solution_Get_ping_pong_game_with_Moq(string action, string actionResult)
        {
            // Arrange
            var winFileService = this.CreateFileServiceMock_With_Moq();
            var service =
                new Services.Exercise3.Solution.DomainExerciseService3(winFileService);

            // Act
            var result = service.GetPingPongGame(action);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result == actionResult);
        }

        [Test]
        public void Exercise3_Solution_Get_ping_pong_game_with_wrong_parameter()
        {
            // AAA
            Assert.Throws<ArgumentNullException>(
                () => new Services.Exercise3.Solution.DomainExerciseService3(new WinFileServiceMock()).GetPingPongGame(null));
        }

        #endregion

        #region Manual Mocking

        /// <summary>
        /// The win file service mock.
        /// </summary>
        internal class WinFileServiceMock : IWinFileService
        {
            /// <summary>
            /// The get win.
            /// </summary>
            /// <returns>
            /// The <see cref="string"/>.
            /// </returns>
            public string GetWin()
            {
                return "Win";
            }
        }

        #endregion

        #region Automated Mocking with Moq

        /// <summary>
        /// The create file service mock_ with_ mocking_ framework.
        /// </summary>
        /// <returns>
        /// The <see cref="IWinFileService"/>.
        /// </returns>
        private IWinFileService CreateFileServiceMock_With_Moq()
        {
            var mockFileService = new Mock<IWinFileService>();
            mockFileService.Setup(serv => serv.GetWin()).Returns("Win");
            return mockFileService.Object;
        }

        #endregion
    }
}