using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class MoveTask : Command
    {
        public override int Do(Print print, Read read)
        {
            Console.WriteLine("Move task");
            return 0;
        }
        public override int Undo(Print print, Read read)
        {
            Console.WriteLine("Undo move task");
            return 0;
        }
    }
}
