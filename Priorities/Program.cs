using System;
using System.Collections.Generic;
using System.Linq;
using Console = System.Console;

namespace Priorities
{
    class Program
    {
        /// <summary>
        ///   The to-do item file name.
        /// </summary>
        private string _filename;
        /// <summary>
        ///   Program entry point.
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            var con = new MSGLib.Console();
            var prog = new Program(con, args);
            prog.Run(con);
        }
        /// <summary>
        ///   Parses the command line arguments.
        /// </summary>
        /// <param name="args"></param>
        Program(MSGLib.Console con, string[] args)
        {
            // Init properties
            this._filename = "tasks.txt";
            // Process command line args
            int anonArgCnt = 0;
            foreach (string arg in args)
            {
                switch (arg)
                {
                    case "/?":
                        UsagePrint(con);
                        System.Environment.Exit(1);
                        break;
                    default:
                        switch (anonArgCnt++)
                        {
                            case 0:
                                this._filename = arg;
                                break;
                        }
                        break;
                }
            }
        }
        /// <summary>
        ///   Main processing routine.
        /// </summary>
        void Run(MSGLib.Console con)
        {
            var tasks = new Tasks();
            tasks.FileLoad(_filename, con);
            var tasksMenu = new TasksMenu();
            tasksMenu.PreLoop(tasks, con);
            tasks.FileSave(_filename, con);
        }
        /// <summary>
        ///   Prints command line usage information.
        /// </summary>
        void UsagePrint(MSGLib.Console con)
        {
            Console.Write(@"Example menu-based console program.

" + AppDomain.CurrentDomain.FriendlyName + @" [filename]

  [filename]
              To-do tasks file to read from and save to.
");
        }

    }
}
