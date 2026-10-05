// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PingPongService3.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the PingPongService3 type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace TheArtOfUnitTesting.Services
{
  using System;
  using TheArtOfUnitTesting.Services;

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
        private readonly IWinRepository pingPongWinRepository; 

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="PingPongService3"/> class.
        /// </summary>
        public PingPongService3()
        {
            this.pingPongWinRepository = new PingPongWinRepository3();
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
                // Now the PingPongWinRepository have an interface and is exposing 
                // the contract that is used by this class.
                return action + this.pingPongWinRepository.GetWin();

                /* Problem: But the main problem is the same. We have a contract, but this class
                 * is coupled to a concrete implementation of the Win service.
                 * This is because this class, has the responsability to create a concrete
                 * instance of the win service and this is coupling the class to a concrete implementation.
                 */
            }

            return Fault;
        }
    }
}
