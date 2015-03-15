using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Colored = MSGLib.Colored;
using ColoredConsole = MSGLib.Colored.Console;
using ColoredStringList = MSGLib.Colored.String.List;
using Menu = MSGLib.Colored.Console.Menu;
using MenuItem = MSGLib.Colored.Console.Menu.Item;
using MenuItems = MSGLib.Colored.Console.Menu.Item.List;
using Table = MSGLib.Colored.Console.Table;
using TableCols = MSGLib.Colored.Console.Table.Col.List;
using TableRow = MSGLib.Colored.Console.Table.Col.List.Row;
using TableRows = MSGLib.Colored.Console.Table.Col.List.Row.List;

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
            var options = new Options();
            var con = new ColoredConsole();
            var prog = new Program(args, options, con);
            prog.Run(options, con);
            options.Save();
        }
        /// <summary>
        ///   Parses the command-line arguments.  Command-line arguments override settings, but don't
        ///   replace them in the settings file.
        /// </summary>
        /// <param name="args"></param>
        /// <param name="options"></param>
        /// <param name="con"></param>
        Program(string[] args, Options options, ColoredConsole con)
        {
            // Init properties
            this._filename = options.FileName;
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
        void Run(Options options, ColoredConsole con)
        {
            var tasks = new Tasks();
            tasks.FileLoad(_filename, con);
            var tasksMenu = new ProgramMenu(options, con, tasks);
            tasksMenu.PRELoop(options, con, tasks);
            tasks.FileSave(_filename, con);
        }
        /// <summary>
        ///   Prints command line usage information.
        /// </summary>
        void UsagePrint(ColoredConsole con)
        {
            Console.Write(@"Example menu-based console program.

" + AppDomain.CurrentDomain.FriendlyName + @" [filename]

  [filename]
              To-do tasks file to read from and save to.
");
        }
    }

    /// <summary>
    ///   Program options.
    /// </summary>
    public class Options : ApplicationSettingsBase
    {
        [UserScopedSetting()]
        [DefaultSettingValue("false")]
        public bool AutoList
        {
            get
            {
                return ((bool)this["AutoList"]);
            }
            set
            {
                this["AutoList"] = (bool)value;
            }
        }

        [UserScopedSetting()]
        [DefaultSettingValue("tasks.txt")]
        public string FileName
        {
            get
            {
                return ((string)this["FileName"]);
            }
            set
            {
                this["FileName"] = (string)value;
            }
        }
    }
}
