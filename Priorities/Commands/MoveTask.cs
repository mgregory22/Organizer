using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class MoveTask : Command
    {
        public override int Do()
        {
            Console.WriteLine("Move task");
            return 0;
        }
        public override int Undo()
        {
            Console.WriteLine("Undo move task");
            return 0;
        }
    }
}
