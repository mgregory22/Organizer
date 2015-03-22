using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class MoveTask : Command
    {
        public override void Do()
        {
            Console.WriteLine("Move task");
        }
        public override void Undo()
        {
            Console.WriteLine("Undo move task");
        }
    }
}
