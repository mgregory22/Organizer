using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class ListTasks : Command
    {
        public override void Do()
        {
            Console.WriteLine("List tasks");
        }
        public override void Undo()
        {
            Console.WriteLine("Can't undo list tasks!");
        }
    }
}
