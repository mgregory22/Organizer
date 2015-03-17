using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;
using System;
using System.Collections.Generic;

namespace PrioritiesTest
{
    class ToStringCountMenuItem : MenuItem
    {
        public static int toStringCalled = 0;

        public ToStringCountMenuItem()
            : base(ConsoleKey.A, "")
        {
        }

        public override string ToString(int index = 0)
        {
            toStringCalled++;
            return "";
        }

    }

    [TestClass]
    public class MenuTests
    {
        Menu menu;
        MenuItem[] menuItems;

        [TestInitialize]
        public void Initialize()
        {
            menuItems = new MenuItem[] {
                new ToStringCountMenuItem(),
                new ToStringCountMenuItem(),
                new ToStringCountMenuItem(),
                new ToStringCountMenuItem()
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
            Assert.AreEqual(menuItems.Length, ToStringCountMenuItem.toStringCalled);
        }
    }
}
