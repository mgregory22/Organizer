using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;
using MSG.Patterns;
using System;
using System.Collections.Generic;

namespace MSGTest.Console
{
    [TestClass]
    public class MenuDisplayTests
    {
        class ToStringCountMenuItem : MenuItem
        {
            public static int ToStringCount = 0;

            public ToStringCountMenuItem(int n)
                : base('a', new TestCommand(n), "")
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
            menu = new Menu("Test Menu", menuItems);
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
            public override void Do() { executeCount++; }
            public override void Undo() { }
        }

        class ActionCountMenuItem : MenuItem
        {
            public ActionCountMenuItem(char keystroke, Command action, string description)
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
                new ActionCountMenuItem('1', countCommands[0], "Item 1"),
                new ActionCountMenuItem('2', countCommands[1], "Item 2"),
                new ActionCountMenuItem('3', countCommands[2], "Item 3"),
                new ActionCountMenuItem('4', countCommands[3], "Item 4")
            };
            menu = new Menu("Test Menu", menuItems);
        }

        [TestMethod]
        public void TestCorrectMenuItemIsExecutedWhenKeystrokeIsSent()
        {
            menu.DoMatchingItem('2');
            Assert.AreEqual(1, countCommands[1].executeCount);
            // Might as well check that ONLY the correct command was executed
            Assert.AreEqual(0, countCommands[0].executeCount);
            Assert.AreEqual(0, countCommands[2].executeCount);
            Assert.AreEqual(0, countCommands[3].executeCount);
        }

        [TestMethod]
        public void TestExecuteReturnsTrueWhenKeystrokeMatchesMenuItem()
        {
            Assert.IsTrue(menu.DoMatchingItem('2'));
        }

        [TestMethod]
        public void TestMenuTitleDisplays()
        {
            Assert.IsTrue(menu.ToString().StartsWith("Test Menu\n---------\n"));
        }
    }
}
