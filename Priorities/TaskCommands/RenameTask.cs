﻿//
// Priorities/TaskCommands/RenameTask.cs
//

using System;
using MSG.IO;
using Priorities.Types;

namespace Priorities.TaskCommands
{
    public class RenameTask : TaskCommand
    {
        protected int position;
        protected string name;
        protected string savedName;

        public RenameTask(Tasks tasks, int position, string name)
            : base(tasks)
        {
            this.position = position;
            this.name = name;
        }

        public override Result Do()
        {
            savedName = tasks[position].Name;
            tasks[position].Name = name;
            return new Ok();
        }

        public override Result Undo()
        {
            tasks[position].Name = savedName;
            return new Ok();
        }
    }
}
