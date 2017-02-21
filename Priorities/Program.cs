//
// Priorities/Program.cs
//

using MSG.Console;
using MSG.IO;
using System.Linq;

namespace Priorities
{
    public sealed class Program
    {
        /// <summary>
        /// Program entry point.  This should be the only class that's not tested.
        /// </summary>
        /// <param name="args">
        /// None.
        /// </param>
        public static void Main(string[] args)
        {
            Io io = new Io(new Print(), new Read());
            CharPrompt prompt = new CharPrompt();
            Driver.Do(io, prompt);
            if (args.Any(a => a.Equals("-p"))) {
                prompt.Pause(io);
            }
        }
    }
}
