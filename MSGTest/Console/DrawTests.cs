using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;

namespace MSGTest.Console
{
    [TestClass]
    public class DrawTests
    {
        [TestMethod]
        public void TestRulerReturns10CharRulerWith1AtTheEnd()
        {
            Assert.AreEqual("---------1", Draw.Ruler(10));
        }
        [TestMethod]
        public void TestRulerReturns20CharRulerWith2AtTheEnd()
        {
            Assert.AreEqual("---------1---------2", Draw.Ruler(20));
        }
        [TestMethod]
        public void TestUnderlinedTextReturnsMainMenuTitle()
        {
            Assert.AreEqual("Main Menu\n---------\n", Draw.UnderlinedText("Main Menu"));
        }
    }
}
