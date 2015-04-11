using MSG.Console;
using MSG.IO;
using Priorities.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priorities
{
    public class Driver
    {
        public static string promptMsg = "> ";
        Print print;
        Read read;

        public Driver(Print print, Read read)
	    {
            this.print = print;
            this.read = read;
	    }

        public int Run()
        {
            Tasks tasks = new Tasks();
            MenuItem[] menuItems = {
                    new MenuItem('a', new AddTask(tasks), "Add Task"),
                    new MenuItem('d', new DeleteTask(), "Delete Task"),
                    new MenuItem('l', new ListTasks(), "List Tasks"),
                    new MenuItem('m', new MoveTask(), "Move Task/Change Priority"),
                    new MenuItem('o', new Options(), "Options Menu"),
                    new MenuItem('q', new Quit(), "Quit Program"),
                    new MenuItem('r', new RenameTask(), "Rename Task"),
                    new MenuItem('?', null, "Help") //TODO
                };
            Menu mainMenu = new Menu("Main Menu", menuItems, promptMsg, print, read);
            return mainMenu.Loop();
        }
    }
}
