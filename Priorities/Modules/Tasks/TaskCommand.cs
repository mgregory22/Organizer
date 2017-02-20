//
// Priorities/Modules/Tasks/TaskCommand.cs
//

using MSG.Patterns;

namespace Priorities.Modules.Tasks
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
