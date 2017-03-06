//
// Organizer/Modules/Tasks/Conds/IsTasksNonEmpty.cs
//

using MSG.Patterns;

namespace Organizer.Modules.Tasks.Conds
{
    public class IsTasksNonEmpty : Cond
    {
        protected Tasks tasks;

        public IsTasksNonEmpty(Tasks tasks)
        {
            this.tasks = tasks;
        }

        public override bool Test()
        {
            return tasks.Count > 0;
        }
    }
}
