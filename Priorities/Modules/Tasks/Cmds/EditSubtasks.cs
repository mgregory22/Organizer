//
// Priorities/Modules/Tasks/Cmds/EditSubtasks.cs
//

using MSG.Types.Dir;

namespace Priorities.Modules.Tasks.Cmds
{
    public class EditSubtasks : TaskCmd
    {
        protected int index;
        protected string name;
        protected Dir<Task> savedSubtasks;

        public EditSubtasks(Tasks tasks, int index, string name)
            : base(tasks)
        {
            this.index = index;
            this.name = name;
        }

        public override Result Do()
        {
            tasks[index].Name = name;
            return new Ok();
        }

        public override Result Undo()
        {
            savedSubtasks = tasks.GetSubdir(index);
            tasks.SetSubdir(index, null);
            return new Ok();
        }
    }
}
