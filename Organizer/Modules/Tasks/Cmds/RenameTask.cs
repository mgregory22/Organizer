//
// Organizer/Modules/Tasks/Commands/RenameTask.cs
//

namespace Organizer.Modules.Tasks.Cmds
{
    public class RenameTask : TaskCmd
    {
        protected int index;
        protected string name;
        protected string savedName;

        public RenameTask(Tasks tasks, int index, string name)
            : base(tasks)
        {
            this.index = index;
            this.name = name;
        }

        public override Result Do()
        {
            savedName = tasks.GetNameAt(index);
            tasks.SetNameAt(index, name);
            return new Ok();
        }

        public override Result Undo()
        {
            tasks.SetNameAt(index, savedName);
            return new Ok();
        }
    }
}
