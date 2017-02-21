//
// Priorities/Modules/Tasks/Cmds/InsertTask.cs
//

namespace Priorities.Modules.Tasks.Cmds
{
    public class InsertTask : TaskCmd
    {
        string name;
        int index;
        Task task;

        public InsertTask(Tasks tasks, string name, int index) : base(tasks)
        {
            this.name = name;
            this.index = index;
            this.task = new Task(name);
        }

        public override Result Do()
        {
            tasks.Insert(index, task);
            return new Ok();
        }

        public override Result Undo()
        {
            tasks.Remove(task);
            return new Ok();
        }
    }
}
