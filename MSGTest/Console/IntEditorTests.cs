//
// MSGTest/Console/IntEditorTests.cs
//

using MSGTest.IO;
using NUnit.Framework;

namespace MSGTest.Console
{
    class IntEditorTests
    {
        MSG.Console.IntEditor editor;
        TestPrint print;
        string promptMsg = "> ";
        TestRead read;
        int input;

        [SetUp]
        public void SetUp()
        {
            print = new TestPrint();
            print.BufferWidth = 8;
            read = new TestRead(null);
            editor = new MSG.Console.IntEditor(print, read, promptMsg);
        }

        [Test]
        public void TestDigitCanBeInsertedIntoBuffer()
        {
            read.PushString("0\r");
            input = editor.PromptAndInput();
            Assert.AreEqual(0, input);
        }

        [Test]
        public void TestLetterCannotBeInsertedIntoBuffer()
        {
            read.PushString("a0b\r");
            input = editor.PromptAndInput();
            Assert.AreEqual(0, input);
        }

        [Test]
        public void TestMinusCanBeInsertedIntoBeginningOfBuffer()
        {
            read.PushString("-0");
            input = editor.PromptAndInput();
            Assert.AreEqual(0, input);
        }

        [Test]
        public void TestMinusInsertedIntoMiddleOfBufferIsNotValid()
        {
            read.PushString("1-\r");
            // Hack so this test won't get stuck
            editor.PrintPrompt();
            string strInput = editor.GetAndProcessKeys();
            Assert.IsFalse(editor.InputIsValid(strInput));
        }
    }
}
