using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class ListTasks : Command
    {
        public override int Do(Print print, Read read)
        {
            Console.WriteLine("List tasks");
            return 0;
        }
        public override int Undo(Print print, Read read)
        {
            Console.WriteLine("Can't undo list tasks!");
            return 0;
        }
    }
}
