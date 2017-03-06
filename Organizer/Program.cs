//
// Organizer/Program.cs
//

using System;
using System.Linq;
using MSG.Console;
using MSG.IO;
using MSG.Types.Dir;
using Organizer.Modules.Tasks;

namespace Organizer
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
            // Io, Print and Read represent the parts of the console i/o.
            Io io = new Io(new Print(), new Read());

            CharPrompt prompt = new CharPrompt();

            MemDir<Task> tasks = new MemDir<Task>();

            //Database db = new Database("Test.org", Database.OpenStyle.RecreateExistent);
            //DbDir<Task> tasks = new DbDir<Task>(db, "Tasks");

            Driver.Run(io, prompt, tasks);

            if (args.Any(a => a.Equals("-p"))) {
                prompt.Pause(io);
            }
        }
    }
}
