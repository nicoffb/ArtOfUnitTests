// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PingPongService5Tests.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the PingPongService5Tests type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TheArtOfUnitTesting.Services.UnitTests
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    using NUnit.Framework;

    using TheArtOfUnitTesting.Services;

    using PingPongService2 = Services.PingPongService2;

    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class ShapeTests
    {
        public void Shape_GetAreaFromSquare(
            string action,
            string actionResult)
        {
            // Arrange

            //// Act
            //var result = square.Area();

            //// Assert
            //Assert.IsNotNull(result);
            //Assert.IsTrue(result == 9);
        }

        [Test]
        public void PingPongService5_GetPingPongGame_When_action_is_null_then_throws_error()
        {
            // AAA
            Assert.Throws<ArgumentNullException>(() => new PingPongService2().GetPingPongGame(null));
        }
    }
}
