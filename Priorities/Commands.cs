using MSG.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priorities
{
    class AddTask : Command
    {
        public void Execute()
        {
            Console.WriteLine("Add Task");
        }

        public void Unexecute()
        {
            Console.WriteLine("Undo add task");
        }
    }

    class DeleteTask : Command
    {
        public void Execute()
        {
            Console.WriteLine("Delete task");
        }
        public void Unexecute()
        {
            Console.WriteLine("Undo delete task");
        }
    }

    class ListTasks : Command
    {
        public void Execute()
        {
            Console.WriteLine("List tasks");
        }
        public void Unexecute()
        {
            Console.WriteLine("Can't undo list tasks!");
        }
    }

    class MoveTask : Command
    {
        public void Execute()
        {
            Console.WriteLine("Move task");
        }
        public void Unexecute()
        {
            Console.WriteLine("Undo move task");
        }
    }

    class Options : Command
    {
        public void Execute()
        {
            Console.WriteLine("Go to options menu");
        }
        public void Unexecute()
        {
            Console.WriteLine("Can't undo go to options menu!");
        }
    }

    class RenameTask : Command
    {
        public void Execute()
        {
            Console.WriteLine("Rename task");
        }
        public void Unexecute()
        {
            Console.WriteLine("Undo rename task");
        }
    }

    class Quit : Command
    {
        public void Execute()
        {
            Console.WriteLine("Quit");
        }
        public void Unexecute()
        {
            Console.WriteLine("Can't undo quit!");
        }
    }

}
