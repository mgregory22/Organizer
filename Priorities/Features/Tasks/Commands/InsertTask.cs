//
// Priorities/Features/Tasks/Commands/InsertTask.cs
//

namespace Priorities.Features.Tasks.Commands
{
    public class InsertTask : TaskCommand
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
            tasks.Insert(task, index);
            return new Ok();
        }

        public override Result Undo()
        {
            tasks.Remove(task);
            return new Ok();
        }
    }
}
