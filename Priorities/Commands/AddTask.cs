using MSG.Console;
using MSG.Patterns;
using System;

namespace Priorities.Commands
{
    class AddTask : Command
    {
        public override int Do()
        {
            Print print = new Print();
            Read read = new Read();
            StringPrompt prompt = new StringPrompt(print, "Enter task name\n> ", read);
            string taskName = prompt.DoPrompt();
            print.String("You entered: " + taskName, true);
            return 0;
        }
        public override int Undo()
        {
            Console.WriteLine("Undo add task");
            return 0;
        }
    }
}
