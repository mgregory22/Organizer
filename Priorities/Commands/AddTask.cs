using MSG.Console;
using MSG.IO;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class AddTask : Command
    {
        private Tasks tasks;
        public AddTask(Tasks tasks)
        {
            this.tasks = tasks;
        }
        public override int Do(Print print, Read read)
        {
            StringPrompt prompt = new StringPrompt(print, "Enter task name\n> ", read);
            string taskName = prompt.DoPrompt();
            print.String("You entered: " + taskName, true);
            this.tasks.Add(taskName);
            return 0;
        }
        public override int Undo(Print print, Read read)
        {
            Console.WriteLine("Undo add task");
            return 0;
        }
    }
}
