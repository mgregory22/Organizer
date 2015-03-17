using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Priorities;

namespace PrioritiesTest
{
    [TestClass]
    public class MenuTests
    {
        [TestMethod]
        public void TestMenu()
        {
            MenuItem[] menuItems = {
                new MenuItem(ConsoleKey.A, "Add Chicken"),
                new MenuItem(ConsoleKey.B, "Broast Chicken"),
                new MenuItem(ConsoleKey.C, "Chill Chicken"),
                new MenuItem(ConsoleKey.T, "Test menu item, test of wrapping text " +
                    "within a 40 column area of the screen for a nice display of things and stuff that " +
                    "is nice to think about before work when I get there I will work and play.", 40)
            };
            Menu menu = new Menu(menuItems);
            Assert.AreEqual(
                "[A] Add Chicken\n" +
                "[B] Broast Chicken\n" +
                "[C] Chill Chicken\n" +
                "[T] Test menu item, test of wrapping\n" +
                "    text within a 40 column area of the\n" +
                "    screen for a nice display of things\n" +
                "    and stuff that is nice to think\n" +
                "    about before work when I get there I\n" +
                "    will work and play.\n"
              , menu.ToString());
        }
    }
}
