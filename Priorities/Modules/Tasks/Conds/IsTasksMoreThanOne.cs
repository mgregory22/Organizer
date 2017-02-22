//
// Priorities/Modules/Tasks/Conds/IsTasksMoreThanOne.cs
//

using MSG.Patterns;

namespace Priorities.Modules.Tasks.Conds
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
