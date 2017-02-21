//
// Priorities/Modules/Tasks/Cmds/AddTask.cs
//

namespace Priorities.Modules.Tasks.Cmds
{
    public class AddTask : TaskCmd
    {
        string name;
        Task task;

        public AddTask(Tasks tasks, string name)
            : base(tasks)
        {
            this.name = name;
            this.task = null;
        }

        public override Result Do()
        {
            this.task = new Task(name);
            tasks.Add(task);
            return new Ok();
        }

        public override Result Undo()
        {
            tasks.Remove(task);
            return new Ok();
        }
    }
}
