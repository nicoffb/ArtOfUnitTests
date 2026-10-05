 --------------------------------------------------------------------------------------------------------------

namespace TheArtOfUnitTesting.Services
{
    using System.Configuration;
    using System.IO;
 
    public class PingPongWinRepository2 : IWinRepository
    { 
        public virtual string GetWin()
        {
            var filePath = ConfigurationManager.AppSettings["WinFilePath"] ?? "win.txt";
            return File.ReadAllText(filePath);
        }
    }
}
