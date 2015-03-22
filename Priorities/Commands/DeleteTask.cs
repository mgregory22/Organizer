using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class DeleteTask : Command
    {
        public override void Do()
        {
            Console.WriteLine("Delete task");
        }
        public override void Undo()
        {
            Console.WriteLine("Undo delete task");
        }
    }
}
