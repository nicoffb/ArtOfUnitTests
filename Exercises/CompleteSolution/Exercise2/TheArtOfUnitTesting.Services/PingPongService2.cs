// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PingPongService2.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the PingPongService2 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TheArtOfUnitTesting.Services
{
  using System;

  /// <summary>
  /// The ping pong service 5.
  /// </summary>
  public class PingPongService2 
    {
        #region Constants & fields

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
        private readonly PingPongWinRepository2 pingPongWinRepository; 
        
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="PingPongService2"/> class.
        /// </summary>
        public PingPongService2()
        {
            this.pingPongWinRepository = new PingPongWinRepository2();
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
        /// Arguments Cannot be null.
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

                /* Problem: Although we are following Open-Close principle he have de problem
                 *          that this class is coupled to a concrete implementation of the class.
                 */
            }

            return Fault;
        }
    }
}
