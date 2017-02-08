using MSG.Patterns;
using Priorities.Types;

namespace Priorities.Conditions
{
    public class IsTasksMoreThanOne : Condition
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
