using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class Options : Command
    {
        public override int Do(Print print, Read read)
        {
            Console.WriteLine("Go to options menu");
            return 0;
        }
        public override int Undo(Print print, Read read)
        {
            Console.WriteLine("Can't undo go to options menu!");
            return 0;
        }
    }
}
