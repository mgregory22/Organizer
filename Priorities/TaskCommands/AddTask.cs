//
// Priorities/TaskCommands/AddTask.cs
//

using System;
using Priorities.Types;

namespace Priorities.TaskCommands
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
            this.task = new Types.Task(name);
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
