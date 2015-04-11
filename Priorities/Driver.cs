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
                    new MenuItem('a', new AddTask(print, read, tasks), "Add Task"),
                    new MenuItem('d', new DeleteTask(print, read, tasks), "Delete Task"),
                    new MenuItem('l', new ListTasks(print, read, tasks), "List Tasks"),
                    new MenuItem('m', new MoveTask(print, read, tasks), "Move Task/Change Priority"),
                    new MenuItem('o', new Options(print, read, tasks), "Options Menu"),
                    new MenuItem('q', new Quit(print, read, tasks), "Quit Program"),
                    new MenuItem('r', new RenameTask(print, read, tasks), "Rename Task"),
                    new MenuItem('?', new Help(print, read, tasks), "Help") //TODO
                };
            Menu mainMenu = new Menu("Main Menu", menuItems, promptMsg, print, read);
            return mainMenu.Loop();
        }
    }
}
