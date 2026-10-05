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
  using System.Configuration;

  /// <summary>
  /// The ping pong service 4.
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
                var filePath = ConfigurationManager.AppSettings["WinFilePath"] ?? "win.txt";

                return action + PingPongHelper4.GetWin(filePath);
                /* Problem1: Not Testeable
                 * Problem2: Not Mockeable
                 * Problem3: Not Maintainable
                 * Problem4: What happens if we need parameters or dependencies in GetWin? 
                 *           More methods? More parameters? Until when?
                 * Problem5: What happens if we need some domain service in Helper to resolve GetWin? 
                 *           How can we pass this dependency to helper? As a parameter? == Problem4
                 * Problem6: Very very coupled to lot of classes and codes.
                 * Problem7: It is not extensible.
                 */
            }

            return Fault;
        }
    }
}
