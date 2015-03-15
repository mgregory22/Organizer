using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColoredConsole = MSGLib.Colored.Console;
using Menu = MSGLib.Colored.Console.Menu;
using MenuItem = MSGLib.Colored.Console.Menu.Item;
using MenuItems = MSGLib.Colored.Console.Menu.Item.List;

namespace Priorities
{
    class OptionsMenu
    {
        /// <summary>
        ///   The list of items for the program menu.
        /// </summary>
        private MenuItems _items;
        /// <summary>
        ///   Creates the options menu items.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="con"></param>
        public OptionsMenu(Options options, ColoredConsole con)
        {
            _items = new MenuItems();
            var optionQuery = from SettingsProperty option in options.Properties
                              orderby option.Name
                              select option;
            foreach (var option in optionQuery)
            {
                _items.ItemAdd(new MenuItem(null, option.Name, () => options[option.Name] = con.PromptForType(option.Name, option.PropertyType.ToString())));
            }
        }
        /// <summary>
        ///   Run menu print/read/execute loop.
        /// </summary>
        /// <param name="options">
        ///   Program options.
        /// </param>
        /// <param name="con">
        ///   The console on which to print prompts and error messages.
        /// </param>
        public void PRELoop(Options options, ColoredConsole con)
        {
            var menu = new Menu();
            do
            {
                if ((bool)options["AutoList"]) _items.ItemExec(ConsoleKey.L);
                con = menu.PRELoop(_items, con);
                if (menu.Selection != ConsoleKey.Q)
                {
                    options.Save();
                }
            } while (menu.Selection != ConsoleKey.Q);
        }
    }
}
