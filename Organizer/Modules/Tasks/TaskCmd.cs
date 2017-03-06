//
// Organizer/Modules/Tasks/TaskCmd.cs
//

using MSG.Patterns;

namespace Organizer.Modules.Tasks
{
    public abstract class TaskCmd : Cmd
    {
        protected Tasks tasks;

        public TaskCmd(Tasks tasks)
        {
            this.tasks = tasks;
        }

    }
}
