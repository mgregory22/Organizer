//
// Priorities/Program.cs
//

using MSG.Console;
using MSG.IO;

namespace Priorities
{
    public sealed class Program
    {
        /// <summary>
        ///   Program entry point.  This should be the only class that's not tested.
        /// </summary>
        /// <param name="args">
        ///   None.
        /// </param>
        public static void Main(string[] args)
        {
            Print print = new Print();
            Read read = new Read(print);
            string promptMsg = "! ";
            CharPrompt prompt = new CharPrompt(print, read, promptMsg);
            Driver.Run(print, read, prompt);
            prompt.Pause();
        }
    }
}
