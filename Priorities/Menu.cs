using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSG.Console
{
    /// <summary>
    ///   The responsibilities of this class are:
    ///   1. Organize menu items together to create a menu and generate its display.
    ///   2. Receive a user-entered shortcut and perform the action(s)
    ///      of the corresponding menu item.
    /// </summary>
    public class Menu
    {
        MenuItem[] menuItems;

        public Menu(MenuItem[] menuItems)
        {
            this.menuItems = menuItems;
        }

        public int ItemCount
        {
            get { return menuItems.Count();  }
        }

        public override string ToString()
        {
            string s = "";
            foreach (MenuItem menuItem in menuItems)
            {
                for (int i = 0; i < menuItem.LineCount; i++)
                {
                    s += menuItem.ToString(i) + "\n";
                }
            }
            return s;
        }
    }

}
