//
// Priorities/Features/Tasks/Commands/AddTask.cs
//

namespace Priorities.Features.Tasks.Commands
{
    public class AddTask : TaskCommand
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
