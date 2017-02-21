//
// Priorities/Modules/Tasks/Conditions/IsTasksMoreThanOne.cs
//

using MSG.Patterns;

namespace Priorities.Modules.Tasks.Conditions
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
