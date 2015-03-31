using MSG.Console;
using System;

namespace Priorities
{
    public sealed class Program
    {
        /// <summary>
        ///   Program entry point.
        /// </summary>
        /// <param name="args">
        ///   None.
        /// </param>
        public static void Main(string[] args)
        {
            Print print = new Print();
            Read read = new Read();
            Driver driver = new Driver(print, read);
            driver.Run();
            Pause();
        }
        /// <summary>
        ///   Wait for key to keep the window open.
        /// </summary>
        public static void Pause()
        {
            Console.Write("Press any key to close window");
            Console.ReadKey(true);
        }
    }
}
