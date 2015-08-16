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
            Driver.Run(print, "! ", read);
            Prompt.Pause();
        }
    }
}
