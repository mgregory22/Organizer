//
// Priorities/Features/Tasks/Conditions/IsTasksMoreThanOne.cs
//

using MSG.Patterns;

namespace Priorities.Features.Tasks.Conditions
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
