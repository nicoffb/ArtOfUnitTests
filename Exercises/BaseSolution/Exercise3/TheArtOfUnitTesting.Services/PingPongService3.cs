// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PingPongService6.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the PingPongService6 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TheArtOfUnitTesting.Services
{
  using System;

  /// <summary>
  /// The ping pong service 6.
  /// </summary>
  public class PingPongService3
    {
        #region Constants & Fields

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
        private readonly IPingPongWinRepository3 pingPongWinRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="PingPongService3"/> class.
        /// </summary>
        public PingPongService3()
            : this(new PingPongWinRepository3())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PingPongService3"/> class with an injected repository.
        /// </summary>
        /// <param name="pingPongWinRepository">Repositorio que provee el valor de GetWin.</param>
        public PingPongService3(IPingPongWinRepository3 pingPongWinRepository)
        {
            this.pingPongWinRepository = pingPongWinRepository;
        }

        #endregion

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
                // With a class this has an advantage that we can extend class with more functionality
                // We are following Open-Close principle
                return action + this.pingPongWinRepository.GetWin();

                /* Problem: Although we are following Open-Close principle 
                 *          this class is coupled to a concrete implementation of the class.
                 */
            }

            return Fault;
        }
    }
}
