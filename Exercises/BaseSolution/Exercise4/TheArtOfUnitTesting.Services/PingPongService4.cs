// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PingPongService4.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the PingPongService4 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TheArtOfUnitTesting.Services
{
    using System;

    /// <summary>
    /// The ping pong service 4.
    /// Demonstrates the Dependency Inversion Principle (DIP).
    /// </summary>
    public class PingPongService4
    {
        /// <summary>
        /// The fault.
        /// </summary>
        private const string Fault = "Fault";

        /// <summary>
        /// The ping.
        /// </summary>
        private const string Ping = "Ping";

        /// <summary>
        /// The ping pong.
        /// </summary>
        private const string PingPong = "PingPong";

        /// <summary>
        /// The pong.
        /// </summary>
        private const string Pong = "Pong";

        /// <summary>
        /// The win repository dependency.
        /// </summary>
        private readonly IWinRepository _winRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PingPongService4"/> class.
        /// </summary>
        /// <param name="winRepository">
        /// The win repository abstraction.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown when winRepository is null.
        /// </exception>
        public PingPongService4(IWinRepository winRepository)
        {
            _winRepository = winRepository ?? throw new ArgumentNullException(nameof(winRepository));
        }

        /// <summary>
        /// The get ping pong game.
        /// </summary>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Arguments cannot be null.
        /// </exception>
        public string GetPingPongGame(string action)
        {
            if (string.IsNullOrEmpty(action))
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (action == Ping)
            {
                return action + Pong;
            }

            if (action == Pong)
            {
                return Ping + action;
            }

            if (action == PingPong)
            {
                return action + _winRepository.GetWin();
            }

            return Fault;
        }
    }
}
