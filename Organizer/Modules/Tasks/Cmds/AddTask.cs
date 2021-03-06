﻿//
// Organizer/Modules/Tasks/Cmds/AddTask.cs
//

namespace Organizer.Modules.Tasks.Cmds
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
            this.task = new Task() { };
            tasks.Add(name, task);
            return TaskCmd.OK;
        }

        public override Result Undo()
        {
            tasks.Remove(task);
            return TaskCmd.OK;
        }
    }
}
