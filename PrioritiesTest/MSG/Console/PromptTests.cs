using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSG.Console;
using System;

namespace PrioritiesTest
{
    [TestClass]
    public class PromptTests
    {
        private Prompt testPrompt;
        private string testPromptMsg = "> ";

        [TestInitialize]
        public void Initialize()
        {
            testPrompt = new Prompt(testPromptMsg);
        }

        [TestMethod]
        public void TestPromptStoresPrompt()
        {
            Assert.AreEqual(testPromptMsg, testPrompt.PromptMsg);
        }
    }
}
