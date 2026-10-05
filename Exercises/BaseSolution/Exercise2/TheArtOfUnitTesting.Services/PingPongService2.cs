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
        private readonly IWinRepository winRepository;

        public PingPongService2(IWinRepository winRepository)
        {
            this.winRepository = winRepository ?? throw new ArgumentNullException(nameof(winRepository));
        }
      
        private const string Fault = "Fault";

        
        private const string Ping = "Ping";

        
        private const string PingPong = "PingPong";

        
        private const string Pong = "Pong";

      
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
                return action + this.winRepository.GetWin();

               
            }

            return Fault;
        }
    }
}
