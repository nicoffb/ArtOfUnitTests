// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PingPongService7.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the PingPongService7 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TheArtOfUnitTesting.Services
{
  using System;

  /// <summary>
  /// The ping pong service 7.
  /// </summary>
  public class PingPongService4
    {
        #region Fields & Constants
        
        /// <summary>
        ///     The fault.
        /// </summary>
        private const string Fault = "Fault";

        /// <summary>
        ///     The ping.
        /// </summary>
        private const string Ping = "Ping";

        /// <summary>
        ///     The ping pong.
        /// </summary>
        private const string PingPong = "PingPong";

        /// <summary>
        ///     The pong.
        /// </summary>
        private const string Pong = "Pong";

        /// <summary>
        /// The sin service.
        /// </summary>
        private readonly IWinRepository pingPongWinRepository;

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="PingPongService4"/> class.
        /// </summary>
        /// <param name="pingPongWinRepository">
        /// The win service.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Arguments cannot be null.
        /// </exception>
        public PingPongService4(IWinRepository pingPongWinRepository)
        {
            if (pingPongWinRepository == null)
            {
                throw new ArgumentNullException(nameof(pingPongWinRepository));
            }

            this.pingPongWinRepository = pingPongWinRepository;
        }

        /// <summary>
        ///     The get ping pong game.
        /// </summary>
        /// <param name="action">
        ///     The action.
        /// </param>
        /// <returns>
        ///     The <see cref="string" />.
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
                // Now de pingPongWinRepository is a dependency injected through de construcor
                return action + this.pingPongWinRepository.GetWin();

                /* NOW:
                 *  - Inside IWinRepository we can pass any implementation of that interface
                 *  - Any business change afecting IWinRepository in its implementation does not affects to the Domain Service
                 *  - We, finally can simulate the behaviour of IWinRepository to not get data from the disk
                 */
            }

            return Fault;
        }
    }
}