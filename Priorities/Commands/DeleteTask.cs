using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class DeleteTask : Command
    {
        public override int Do()
        {
            Console.WriteLine("Delete task");
            return 0;
        }
        public override int Undo()
        {
            Console.WriteLine("Undo delete task");
            return 0;
        }
    }
}
