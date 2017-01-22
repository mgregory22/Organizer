//
// MSGTest/Console/MenuTests.cs
//

using MSG.Console;
using MSG.IO;
using MSGTest.Patterns;
using NUnit.Framework;

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
            : base(k, new TestCommand(), "")
        {
        }
    }

    [TestFixture]
    public class MenuCreationTests
    {
        Menu menu;
        ToStringCountMenuItem[] menuItems;
        [SetUp]
        public void Initialize()
        {
            menuItems = new ToStringCountMenuItem[] {
                new ToStringCountMenuItem('a', 1),
                new ToStringCountMenuItem('b', 2),
                new ToStringCountMenuItem('c', 3),
                new ToStringCountMenuItem('d', 4)
            };
            Print print = new Print();
            Read read = new Read(print);
            CharPrompt prompt = new CharPrompt(print, read, "");
            menu = new Menu("Test Menu", menuItems, prompt);
        }
        [Test]
        public void TestAllMenuItemsAreStored()
        {
            Assert.AreEqual(menuItems.Length, menu.ItemCount);
        }
        [Test]
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

    [TestFixture]
    public class MenuDisplayTests
    {
        Menu menu;
        ToStringCountMenuItem[] menuItems;
        [SetUp]
        public void Initialize()
        {
            menuItems = new ToStringCountMenuItem[] {
                new ToStringCountMenuItem('a', 1),
                new ToStringCountMenuItem('b', 2),
                new ToStringCountMenuItem('c', 3),
                new ToStringCountMenuItem('d', 4)
            };
            Print print = new Print();
            Read read = new Read(print);
            CharPrompt prompt = new CharPrompt(print, read, "");
            menu = new Menu("Test Menu", menuItems, prompt);
        }
        [Test]
        public void TestMenuTitleDisplays()
        {
            Assert.IsTrue(menu.ToString().StartsWith("Test Menu\n---------\n"));
        }
        [Test]
        public void TestToStringCallsAllMenuItemToStrings()
        {
            menu.ToString();
            foreach (ToStringCountMenuItem menuItem in menuItems)
            {
                Assert.AreEqual(1, menuItem.ToStringCount);
            }
        }
    }

    [TestFixture]
    public class MenuCommandTests
    {
        class CommandCountMenuItem : MenuItem
        {
            TestCommand command;
            public new TestCommand Command
            {
                get { return command; }
                set { command = value; }
            }
            public CommandCountMenuItem(char keystroke, TestCommand command, string description)
                : base(keystroke, command, description)
            {
                this.command = command;
            }
        }
        Menu menu;
        CommandCountMenuItem[] menuItems;
        [SetUp]
        public void Initialize()
        {
            menuItems = new CommandCountMenuItem[] {
                new CommandCountMenuItem('0', new TestCommand(), "Item 0"),
                new CommandCountMenuItem('1', new TestCommand(), "Item 1"),
                new CommandCountMenuItem('2', new TestCommand(), "Item 2"),
                new CommandCountMenuItem('3', new TestCommand(), "Item 3")
            };
            Print print = new Print();
            Read read = new Read(print);
            CharPrompt prompt = new CharPrompt(print, read, "");
            menu = new Menu("Test Menu", menuItems, prompt);
        }
        [Test]
        public void TestCorrectMenuItemIsExecutedWhenKeystrokeIsSent()
        {
            MenuItem m = menu.FindMatchingItem('1');
            m.Do();
            Assert.AreEqual(1, menuItems[1].Command.doCount);
            // Might as well check that ONLY the correct command was executed
            Assert.AreEqual(0, menuItems[0].Command.doCount);
            Assert.AreEqual(0, menuItems[2].Command.doCount);
            Assert.AreEqual(0, menuItems[3].Command.doCount);
        }
        [Test]
        public void TestFindReturnsMatchingMenuItem()
        {
            Assert.AreEqual(menuItems[1], menu.FindMatchingItem('1'));
        }
    }
}
