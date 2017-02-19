//
// Priorities/Features/Tasks/TaskCommand.cs
//

using MSG.Patterns;

namespace Priorities.Features.Tasks
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
