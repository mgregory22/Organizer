using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    public class TaskCommand : Command
    {
        protected Print print;
        protected Read read;
        protected Tasks tasks;
        public TaskCommand(Print print, Read read, Tasks tasks)
        {
            this.tasks = tasks;
            this.print = print;
            this.read = read;
        }
        public override int Do()
        {
            Console.WriteLine("Do Task Command");
            return 0;
        }
        public override int Redo()
        {
            Console.WriteLine("Redo Task Command");
            return 0;
        }
        public override int Undo()
        {
            Console.WriteLine("Undo Task Command");
            return 0;
        }
    }
}
