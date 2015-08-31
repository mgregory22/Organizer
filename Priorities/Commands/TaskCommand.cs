//
// Priorities/Commands/TaskCommand.cs
//

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

        override public void Do()
        {
            Console.WriteLine("Do Task Command");
        }

        override public void Redo()
        {
            Console.WriteLine("Redo Task Command");
        }

        override public void Undo()
        {
            Console.WriteLine("Undo Task Command");
        }
    }
}
