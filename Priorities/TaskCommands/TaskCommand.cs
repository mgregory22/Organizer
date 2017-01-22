//
// Priorities/TaskCommands/TaskCommand.cs
//

using MSG.Patterns;
using System;
using Priorities.Types;

namespace Priorities.TaskCommands
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
