//
// Organizer/Modules/Tasks/Conds/IsTasksMoreThanOne.cs
//

using MSG.Patterns;

namespace Organizer.Modules.Tasks.Conds
{
    public class IsTasksMoreThanOne : Cond
    {
        protected Tasks tasks;

        public IsTasksMoreThanOne(Tasks tasks)
        {
            this.tasks = tasks;
        }

        public override bool Test()
        {
            return tasks.Count > 1;
        }
    }
}
