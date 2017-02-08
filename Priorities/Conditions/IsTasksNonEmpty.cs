using MSG.Patterns;
using Priorities.Types;

namespace Priorities.Conditions
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
