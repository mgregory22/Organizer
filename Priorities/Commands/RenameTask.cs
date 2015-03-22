using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class RenameTask : Command
    {
        public override void Do()
        {
            Console.WriteLine("Rename task");
        }
        public override void Undo()
        {
            Console.WriteLine("Undo rename task");
        }
    }
}
