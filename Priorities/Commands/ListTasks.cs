using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class ListTasks : Command
    {
        public override int Do()
        {
            Console.WriteLine("List tasks");
            return 0;
        }
        public override int Undo()
        {
            Console.WriteLine("Can't undo list tasks!");
            return 0;
        }
    }
}
