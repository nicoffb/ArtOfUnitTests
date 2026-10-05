// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PingPongHelper1.cs" company="">
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
    public static class PingPongHelper1
    {
        /// <summary>
        /// Problem1: Not Testeable
        /// Problem2: Not Mockeable
        /// Problem3: Not Maintainable
        /// Problem4: What happens if we need parameters or dependencies in GetWin? 
        ///           More methods? More parameters? Until when?
        /// Problem5: What happens if we need some domain service in Helper to resolve GetWin? 
        ///           How can we pass this dependency to helper? As a parameter? == Problem4
        /// Problem6: Very very coupled to lot of classes and codes.
        /// Problem7: It is not extensible.
        /// </summary>
        /// <param name="filePath">
        /// The file path.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetWin(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// Obtiene el filepath desde configuración y devuelve el contenido del fichero.
        /// </summary>
        /// <returns>Contenido del fichero de "win".</returns>
        public static string GetWin()
        {
            var filePath = ConfigurationManager.AppSettings["WinFilePath"] ?? "win.txt";

            return File.ReadAllText(filePath);
        }
    }
}
