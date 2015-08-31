//
// Priorities/Commands/AddTask.cs
//

using MSG.Console;
using MSG.IO;
using System;

namespace Priorities.Commands
{
    public class AddTask : TaskCommand
    {
        protected string taskName;
        Print print;
        Read read;
        public string promptMsg = "Enter task name\n> ";

        public AddTask(Print print, Read read, Tasks tasks)
            : base(tasks)
        {
            this.print = print;
            this.read = read;
        }

        override public void Do()
        {
            Editor editor = new Editor(print, read, promptMsg);
            taskName = editor.PromptAndInput();
            if (taskName == "")
            {
                print.StringNL("Add cancelled");
                return;
            }
            Redo();
        }

        override public void Redo()
        {
            if (taskName == null)
                throw new InvalidOperationException("Adding a task must be done before it can be redone");
            if (tasks.TaskExists(taskName))
                throw new InvalidOperationException("Adding a task cannot be redone before it is undone");
            tasks.Add(taskName);
        }

        override public void Undo()
        {
            if (taskName == null)
                throw new InvalidOperationException("Adding a task must be done before it can be undone");
            if (!tasks.TaskExists(taskName))
                throw new InvalidOperationException("Adding a task cannot be undone twice");
            tasks.Remove(taskName);
        }
    }
}
