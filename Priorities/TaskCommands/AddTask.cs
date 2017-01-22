//
// Priorities/TaskCommands/AddTask.cs
//

using MSG.Console;
using MSG.IO;
using System;
using Priorities.Types;

namespace Priorities.TaskCommands
{
    public class AddTask : TaskCommand
    {
        protected Task task;
        Print print;
        Read read;
        protected string lastPrompt;

        public string LastPrompt
        {
            get { return this.lastPrompt; }
        }

        public AddTask(Print print, Read read, Tasks tasks)
            : base(tasks)
        {
            this.print = print;
            this.read = read;
        }

        public override void Do()
        {
            Editor editor = new Editor(print, read);
            string name = editor.StringPrompt();
            if (name == null || name == "")
            {
                print.StringNL("Add cancelled");
                return;
            }
            this.task = new Types.Task(name);
            Redo();
            this.lastPrompt = editor.LastPrompt;
        }

        public override void Redo()
        {
            if (this.task == null || this.task.Name == null)
                throw new InvalidOperationException("Nothing to redo");
            if (this.tasks.TaskExists(this.task.Name))
                throw new InvalidOperationException("Already redone");
            this.tasks.Add(this.task.Name);
        }

        public override void Undo()
        {
            if (this.task == null || this.task.Name == null)
                throw new InvalidOperationException("Nothing to undo");
            if (!this.tasks.TaskExists(this.task.Name))
                throw new InvalidOperationException("Already undone");
            this.tasks.Remove(this.task.Name);
        }
    }
}
