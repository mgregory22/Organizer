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
            string name = this.task.Name;
            if (name == null)
                throw new InvalidOperationException("Adding a task must be done before it can be redone");
            if (this.tasks.TaskExists(name))
                throw new InvalidOperationException("Adding a task cannot be redone before it is undone");
            this.tasks.Add(name);
        }

        public override void Undo()
        {
            string name = this.task.Name;
            if (name == null)
                throw new InvalidOperationException("Adding a task must be done before it can be undone");
            if (!this.tasks.TaskExists(name))
                throw new InvalidOperationException("Adding a task cannot be undone twice");
            this.tasks.Remove(name);
        }
    }
}
