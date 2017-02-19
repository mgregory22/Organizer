//
// Priorities/Features/Tasks/Conditions/IsTasksNonEmpty.cs
//

using MSG.Patterns;
using Priorities;

namespace Priorities.Features.Tasks.Conditions
{
    public class IsTasksNonEmpty : Condition
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
