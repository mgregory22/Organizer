//
// Priorities/Modules/Tasks/Commands/EditSubtasks.cs
//

using MSG.Types.Dir;

namespace Priorities.Modules.Tasks.Commands
{
    public class EditSubtasks : TaskCommand
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
