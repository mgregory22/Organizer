using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class AddTask : Command
    {
        public override void Do()
        {
            Console.WriteLine("Add Task");
        }
        public override void Undo()
        {
            Console.WriteLine("Undo add task");
        }
    }
}
