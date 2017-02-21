//
// Priorities/Modules/Tasks/Conditions/IsTasksNonEmpty.cs
//

using MSG.Patterns;
using Priorities;

namespace Priorities.Modules.Tasks.Conditions
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
