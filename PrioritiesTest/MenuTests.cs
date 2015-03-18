using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;
using MSG.Patterns;
using System;
using System.Collections.Generic;

namespace PrioritiesTest
{
    [TestClass]
    public class MenuDisplayTests
    {
        class ToStringCountMenuItem : MenuItem
        {
            public static int ToStringCount = 0;

            public ToStringCountMenuItem(int n)
                : base(ConsoleKey.A, new TestCommand(n), "")
            {
            }

            public override string ToString(int index = 0)
            {
                ToStringCount++;
                return "";
            }
        }

        Menu menu;
        MenuItem[] menuItems;

        [TestInitialize]
        public void Initialize()
        {
            menuItems = new MenuItem[] {
                new ToStringCountMenuItem(1),
                new ToStringCountMenuItem(2),
                new ToStringCountMenuItem(3),
                new ToStringCountMenuItem(4)
            };
            menu = new Menu(menuItems);
        }

        [TestMethod]
        public void TestAllMenuItemsAreStored()
        {
            Assert.AreEqual(menuItems.Length, menu.ItemCount);
        }

        [TestMethod]
        public void TestToStringCallsAllMenuItemToStrings()
        {
            menu.ToString();
            Assert.AreEqual(menuItems.Length, ToStringCountMenuItem.ToStringCount);
        }
    }

    [TestClass]
    public class MenuActionTests
    {
        class CountCommand : Command
        {
            public int executeCount = 0;
            public void Execute() { executeCount++; }
            public void Unexecute() { }
        }

        CountCommand countCommand;

        class ActionCountMenuItem : MenuItem
        {
            public ActionCountMenuItem(ConsoleKey keystroke, Command action, string description)
                : base(keystroke, action, description)
            {
            }
        }

        Menu menu;
        MenuItem[] menuItems;
        CountCommand[] countCommands;

        [TestInitialize]
        public void Initialize()
        {
            countCommands = new CountCommand[] {
                new CountCommand(),
                new CountCommand(),
                new CountCommand(),
                new CountCommand()
            };
            menuItems = new MenuItem[] {
                new ActionCountMenuItem(ConsoleKey.D1, countCommands[0], "Item 1"),
                new ActionCountMenuItem(ConsoleKey.D2, countCommands[1], "Item 2"),
                new ActionCountMenuItem(ConsoleKey.D3, countCommands[2], "Item 3"),
                new ActionCountMenuItem(ConsoleKey.D4, countCommands[3], "Item 4")
            };
            menu = new Menu(menuItems);
        }

        [TestMethod]
        public void TestCorrectMenuItemIsExecutedWhenKeystrokeIsSent()
        {
            menu.ExecuteItemThatKeystrokeMatches(ConsoleKey.D2);
            Assert.AreEqual(1, countCommands[1].executeCount);
            // Might as well check that ONLY the correct command was executed
            Assert.AreEqual(0, countCommands[0].executeCount);
            Assert.AreEqual(0, countCommands[2].executeCount);
            Assert.AreEqual(0, countCommands[3].executeCount);
        }

        [TestMethod]
        public void TestExecuteReturnsTrueWhenKeystrokeMatchesMenuItem()
        {
            Assert.IsTrue(menu.ExecuteItemThatKeystrokeMatches(ConsoleKey.D2));
        }
    }
}
