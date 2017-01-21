//
// Priorities/Commands/AddTask.cs
//

using MSG.Console;
using MSG.IO;
using System;
using Priorities.Types;

namespace Priorities.Commands
{
    public class AddTask : TaskCommand
    {
        protected string taskName;
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
            taskName = editor.StringPrompt();
            if (taskName == "")
            {
                print.StringNL("Add cancelled");
                return;
            }
            Redo();
            this.lastPrompt = editor.LastPrompt;
        }

        public override void Redo()
        {
            if (taskName == null)
                throw new InvalidOperationException("Adding a task must be done before it can be redone");
            if (tasks.TaskExists(taskName))
                throw new InvalidOperationException("Adding a task cannot be redone before it is undone");
            tasks.Add(taskName);
        }

        public override void Undo()
        {
            if (taskName == null)
                throw new InvalidOperationException("Adding a task must be done before it can be undone");
            if (!tasks.TaskExists(taskName))
                throw new InvalidOperationException("Adding a task cannot be undone twice");
            tasks.Remove(taskName);
        }
    }
}
