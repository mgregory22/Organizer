//
// Organizer/Modules/Tasks/Conds/IsCurTaskDirNotRoot.cs
//

using MSG.Patterns;

namespace Organizer.Modules.Tasks.Conds
{
    public class IsCurTaskDirNotRoot : Cond
    {
        protected Tasks tasks;

        public IsCurTaskDirNotRoot(Tasks tasks)
        {
            this.tasks = tasks;
        }

        public override bool Test()
        {
            return tasks.HasParent();
        }
    }
}
