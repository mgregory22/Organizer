using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;
using MSG.Patterns;
using System;
using System.Collections.Generic;

namespace MSGTest.Console
{
    class ToStringCountMenuItem : MenuItem
    {
        private int toStringCount = 0;
        public override string ToString(int index = 0)
        {
            toStringCount++;
            return "";
        }
        public int ToStringCount
        {
            get { return toStringCount; }
            set { toStringCount = value; }
        }
        public ToStringCountMenuItem(char k, int n)
            : base(k, new TestCommand(n), "")
        {
        }
    }

    [TestClass]
    public class MenuCreationTests
    {
        Menu menu;
        ToStringCountMenuItem[] menuItems;
        [TestInitialize]
        public void Initialize()
        {
            menuItems = new ToStringCountMenuItem[] {
                new ToStringCountMenuItem('a', 1),
                new ToStringCountMenuItem('b', 2),
                new ToStringCountMenuItem('c', 3),
                new ToStringCountMenuItem('d', 4)
            };
            menu = new Menu("Test Menu", menuItems);
        }
        [TestMethod]
        public void TestAllMenuItemsAreStored()
        {
            Assert.AreEqual(menuItems.Length, menu.ItemCount);
        }
        [TestMethod]
        public void TestValidKeyListIsCorrect()
        {
            char[] expectedList = new char[] { 'a', 'b', 'c', 'd' };
            char[] actualList = menu.ValidKeys;
            for (int i = 0; i < expectedList.Length; i++)
            {
                Assert.AreEqual(expectedList[i], actualList[i]);
            }
        }
    }

    [TestClass]
    public class MenuDisplayTests
    {
        Menu menu;
        ToStringCountMenuItem[] menuItems;
        [TestInitialize]
        public void Initialize()
        {
            menuItems = new ToStringCountMenuItem[] {
                new ToStringCountMenuItem('a', 1),
                new ToStringCountMenuItem('b', 2),
                new ToStringCountMenuItem('c', 3),
                new ToStringCountMenuItem('d', 4)
            };
            menu = new Menu("Test Menu", menuItems);
        }
        [TestMethod]
        public void TestMenuTitleDisplays()
        {
            Assert.IsTrue(menu.ToString().StartsWith("Test Menu\n---------\n"));
        }
        [TestMethod]
        public void TestToStringCallsAllMenuItemToStrings()
        {
            menu.ToString();
            foreach (ToStringCountMenuItem menuItem in menuItems)
            {
                Assert.AreEqual(1, menuItem.ToStringCount);
            }
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
            CountCommand action;
            public new CountCommand Action
            {
                get { return action; }
                set { action = value; }
            }
            public ActionCountMenuItem(char keystroke, CountCommand action, string description)
                : base(keystroke, action, description)
            {
                this.action = action;
            }
        }
        Menu menu;
        ActionCountMenuItem[] menuItems;
        [TestInitialize]
        public void Initialize()
        {
            menuItems = new ActionCountMenuItem[] {
                new ActionCountMenuItem('1', new CountCommand(), "Item 1"),
                new ActionCountMenuItem('2', new CountCommand(), "Item 2"),
                new ActionCountMenuItem('3', new CountCommand(), "Item 3"),
                new ActionCountMenuItem('4', new CountCommand(), "Item 4")
            };
            menu = new Menu("Test Menu", menuItems);
        }
        [TestMethod]
        public void TestCorrectMenuItemIsExecutedWhenKeystrokeIsSent()
        {
            menu.DoMatchingItem('2');
            Assert.AreEqual(1, menuItems[1].Action.executeCount);
            // Might as well check that ONLY the correct command was executed
            Assert.AreEqual(0, menuItems[0].Action.executeCount);
            Assert.AreEqual(0, menuItems[2].Action.executeCount);
            Assert.AreEqual(0, menuItems[3].Action.executeCount);
        }
        [TestMethod]
        public void TestExecuteReturnsTrueWhenKeystrokeMatchesMenuItem()
        {
            Assert.IsTrue(menu.DoMatchingItem('2'));
        }
    }
}
