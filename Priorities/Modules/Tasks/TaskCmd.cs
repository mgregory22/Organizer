//
// Priorities/Modules/Tasks/TaskCommand.cs
//

using MSG.Patterns;

namespace Priorities.Modules.Tasks
{
    public abstract class TaskCmd : UnCmd
    {
        protected Tasks tasks;

        public TaskCmd(Tasks tasks)
        {
            this.tasks = tasks;
        }

    }
}
