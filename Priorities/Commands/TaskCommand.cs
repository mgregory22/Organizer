using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    public class TaskCommand : Command
    {
        protected Tasks tasks;

        public TaskCommand(Tasks tasks)
        {
            this.tasks = tasks;
        }

        public override void Do()
        {
            Console.WriteLine("Do Task Command");
        }

        public override void Redo()
        {
            Console.WriteLine("Redo Task Command");
        }

        public override void Undo()
        {
            Console.WriteLine("Undo Task Command");
        }
    }
}
