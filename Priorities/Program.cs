using MSG.Console;
using MSG.IO;
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
            Read read = new Read(print);
            Driver driver = new Driver(print, read);
            Console.BufferWidth = Console.WindowWidth = 12; // Shrunk for easier testing of wrapping
            driver.Run();
            Pause();
        }

        /// <summary>
        ///   Wait for key to keep the window open.
        /// </summary>
        public static void Pause()
        {
            Console.Write("Press a key");
            Console.ReadKey(true);
        }
    }
}
