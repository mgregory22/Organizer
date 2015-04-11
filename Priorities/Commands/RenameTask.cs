using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class RenameTask : Command
    {
        public override int Do(Print print, Read read)
        {
            Console.WriteLine("Rename task");
            return 0;
        }
        public override int Undo(Print print, Read read)
        {
            Console.WriteLine("Undo rename task");
            return 0;
        }
    }
}
