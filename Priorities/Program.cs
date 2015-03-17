using MSG.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Priorities
{
    public sealed class Program
    {
        public static void Main(string[] args)
        {
            int width = 40;
            MenuItem[] menuItems = {
                new MenuItem(ConsoleKey.A, "Add Chicken"),
                new MenuItem(ConsoleKey.B, "Broast Chicken"),
                new MenuItem(ConsoleKey.C, "Chill Chicken"),
                new MenuItem(ConsoleKey.T, "Test menu item, test of wrapping text " +
                    "within a 40 column area of the screen for a nice display of things and stuff that " +
                    "is nice to think about before work when I get there I will work and play.", width)
            };
            Menu menu = new Menu(menuItems);
            DrawRuler(width);
            Console.WriteLine(menu.ToString());
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
