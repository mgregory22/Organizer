using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class DeleteTask : Command
    {
        public override int Do(Print print, Read read)
        {
            Console.WriteLine("Delete task");
            return 0;
        }
        public override int Undo(Print print, Read read)
        {
            Console.WriteLine("Undo delete task");
            return 0;
        }
    }
}
