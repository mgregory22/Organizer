using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class RenameTask : Command
    {
        public override int Do()
        {
            Console.WriteLine("Rename task");
            return 0;
        }
        public override int Undo()
        {
            Console.WriteLine("Undo rename task");
            return 0;
        }
    }
}
