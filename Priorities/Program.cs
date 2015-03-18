using MSG.Console;
using MSG.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priorities
{
    public sealed class Program
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

        public static void Main(string[] args)
        {
            MenuItem[] menuItems = {
                new MenuItem(ConsoleKey.A, new AddTask(), "Add Task"),
                new MenuItem(ConsoleKey.D, new DeleteTask(), "Delete Task"),
                new MenuItem(ConsoleKey.L, new ListTasks(), "List Tasks"),
                new MenuItem(ConsoleKey.M, new MoveTask(), "Move Task/Change Priority"),
                new MenuItem(ConsoleKey.O, new Options(), "Options Menu"),
                new MenuItem(ConsoleKey.Q, new Quit(), "Quit Program"),
                new MenuItem(ConsoleKey.R, new RenameTask(), "Rename Task")
            };
            Menu mainMenu = new Menu("Main Menu", menuItems);
            Console.WriteLine(mainMenu.ToString());
            Console.ReadKey(true);
        }

        public static void DrawRuler(int width)
        {
            for (int i = 1; i <= width; i++)
                Console.Write(i % 10 > 0 ? "-" : (i / 10).ToString());
            Console.WriteLine();
        }
    }
}
