// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PingPongWinRepository.cs" company="">
// </copyright>
// <summary>
//   The domain exercise helper.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace TheArtOfUnitTesting.Services
{
  using System.Configuration;
  using System.IO;

  /// <summary>
  /// The domain exercise helper.
  /// </summary>
  public class PingPongWinRepository3 : IWinRepository
    {
        /// <summary>
        /// The get win.
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public virtual string GetWin()
        {
            var filePath = ConfigurationManager.AppSettings["WinFilePath"] ?? "win.txt";
            return File.ReadAllText(filePath);
        }
    }
}
