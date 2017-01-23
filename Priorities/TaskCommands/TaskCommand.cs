//
// Priorities/TaskCommands/TaskCommand.cs
//

using MSG.Patterns;
using System;
using Priorities.Types;

namespace Priorities.TaskCommands
{
    public abstract class TaskCommand : Command
    {
        protected Tasks tasks;

        public TaskCommand(Tasks tasks)
        {
            this.tasks = tasks;
        }

    }
}
