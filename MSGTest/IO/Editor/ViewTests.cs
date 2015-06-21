using MSG.IO;
using MSG.Types.String;
using NUnit.Framework;
using System;
using System.Diagnostics;
using System.Text;

namespace MSGTest.IO.EditorTests
{
    [TestFixture]
    public class ViewTests
    {
        Editor.Buffer buffer;
        Editor.View view;
        TestPrint print;
        string prompt;

        private void ChunkPrint(string s)
        {
            int chunkLen = 50;
            for (int i = 0; i < s.Length; i += chunkLen)
                Debug.WriteLine(s.Substring(i, Math.Min(chunkLen, s.Length - i)));
        }

        private void CursorLeft(int count, Editor.Buffer buffer, Editor.View view)
        {
            for (int i = 0; i < count; i++)
            {
                buffer.CursorLeft();
                view.CursorLeft();
            }
        }

        private void CursorRight(int count, Editor.Buffer buffer, Editor.View view)
        {
            for (int i = 0; i < count; i++)
            {
                buffer.CursorRight();
                view.CursorRight();
            }
        }

        private void InsertText(string text, Editor.Buffer buffer, Editor.View view)
        {
            foreach (char c in text)
            {
                buffer.Insert(c);
                view.Insert();
            }
        }

        private void Print(string s)
        {
            Debug.WriteLine(Format.ToLiteral(s).Replace(' ', '.'));
        }

        private void PrintCmp(string expected, string actual)
        {
            Debug.WriteLine("Expected:");
            string expectedF = Format.ToLiteral(expected).Replace(' ', '.');
            ChunkPrint(expectedF);
            Debug.WriteLine("Actual:");
            string actualF = Format.ToLiteral(actual).Replace(' ', '.');
            ChunkPrint(actualF);
            Debug.WriteLine("Differences:");
            int diffLen = Math.Max(expectedF.Length, actualF.Length);
            StringBuilder diffs = new StringBuilder();
            for (int i = 0; i < diffLen; i++)
                diffs.Append(expectedF.Length > i
                    && actualF.Length > i
                    && expectedF[i] == actualF[i] ? '.' : '*');
            ChunkPrint(diffs.ToString());
        }

        private void SkipTestingTheInsertOutputSinceItsLongAndBoring()
        {
            print.ClearOutput();
        }

        [SetUp]
        public void SetUp()
        {
            buffer = new Editor.Buffer();
            print = new TestPrint();
            print.BufferWidth = 8;
            // Emulate a prompt that was printed before the editor was created.
            prompt = "> ";
            print.String(prompt);
            print.SetCursorPos(2, 0);
            view = new Editor.View(buffer, print);
        }

        [Test]
        public void TestBackspace()
        {
            InsertText("Word", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            buffer.Backspace();
            view.Backspace();
            string expected = "<2^0Wor  <5^0";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestBackspaceThatUnwrapsErasesSecondLine()
        {
            InsertText("Word wr", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            buffer.Backspace();
            view.Backspace();
            string expected = "<2^0Word w       <0^1";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestBackspaceAtEndOfTextToPreviousLineErasesLastCharacterOnThatLine()
        {
            InsertText("Wordw", buffer, view);
            InsertText("r", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            buffer.Backspace();
            view.Backspace();
            string expected = "<2^0Wordw        <7^0";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorEndKeyMovesCursorToEndOfSingleLine()
        {
            InsertText("Test", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorLeft(3, buffer, view);
            view.CursorEnd();
            Assert.AreEqual("<5^0<4^0<3^0<6^0", print.Output);
        }

        [Test]
        public void TestCursorEndKeyOnSecondLineMovesCursorToEndOfSecondLine()
        {
            InsertText("Test words", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorLeft(3, buffer, view);
            view.CursorEnd();
            Assert.AreEqual("<4^1<3^1<2^1<5^1", print.Output);
        }

        [Test]
        public void TestCursorHomeKeyMovesCursorToStartOfSingleLine()
        {
            InsertText("Test", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            view.CursorHome();
            Assert.AreEqual("<2^0", print.Output);
        }

        [Test]
        public void TestCursorHomeKeyOnSecondLineMovesCursorToStartOfSecondLine()
        {
            InsertText("Test words", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            view.CursorHome();
            Assert.AreEqual("<0^1", print.Output);
        }

        [Test]
        public void TestCursorLeftAtBeginningOfNewLineBringsCursorToEndOfPreviousLine()
        {
            InsertText("Word wr", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorLeft(3, buffer, view);
            string expected = "<1^1<0^1<6^0";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorLeftCannotMoveCursorBeforeBeginning()
        {
            buffer.CursorLeft();
            view.CursorLeft();
            Assert.AreEqual(prompt.Length, print.CursorLeft);
        }

        [Test]
        public void TestCursorLeftIntoFullLinePutsCursorAtEndOfLine()
        {
            InsertText("Wordwr ", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorLeft(1, buffer, view);
            string expected = "<6^0";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorLeftWorksOnSecondLine()
        {
            InsertText("Test\nmulti", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorLeft(1, buffer, view);
            string expected = "<4^1";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorLeftWorksOnWrappedSecondLine()
        {
            //        012345678
            InsertText("Word wrap", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorLeft(1, buffer, view);
            string expected = "<3^1";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorRightBeforeEndOfFirstLineAndBeforeEndOfBufferMovesCursorOneRight()
        {
            //        012345678
            InsertText("Test", buffer, view);
            CursorLeft(3, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorRight(1, buffer, view);
            string expected = "<4^0";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorRightBeforeEndOfFirstLineAndAtEndOfBufferThrowsException()
        {
            Assert.Fail();
        }

        [Test]
        public void TestCursorRightAtEndOfFirstLineAndBeforeEndOfBufferPutsCursorAtBeginningOfNextLine()
        {
            //        012345678012345678
            InsertText("Testingwrapping", buffer, view);
            CursorLeft(9, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorRight(1, buffer, view);
            string expected = "<0^1";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorRightCannotMoveCursorPastEnd()
        {
            buffer.CursorRight();
            view.CursorRight();
            Assert.AreEqual(prompt.Length, print.CursorLeft);
        }

        [Test]
        public void TestEnterOnLinesOtherThanTheLastStillPrintsNextPromptAfterAllTheEnteredLines()
        {
            Assert.Fail(); //TODO
        }

        [Test]
        public void TestInsertDoesNotScrollWindowUnnecessarilyWhenClearingToEol()
        {
            InsertText("A", buffer, view);
            int cursorMotionLen = 4;
            int nonInputTextOutputLen = prompt.Length + cursorMotionLen * 3;
            Assert.Less(print.Output.Length - nonInputTextOutputLen, print.BufferWidth - prompt.Length);
        }

        [Test]
        public void TestInsertDoesNotThrowExceptionWhenTextWraps()
        {
            Assert.DoesNotThrow(() => InsertText("Testof wrap", buffer, view));
        }

        [Test]
        public void TestInsertLineThatNeedsToBeWrapped()
        {
            InsertText("Word wrap", buffer, view);
            string expected = "> <2^0" // 6
                    + "<2^0W    <3^0" // 20
                    + "<2^0Wo   <4^0" // 34
                    + "<2^0Wor  <5^0" // 48
                    + "<2^0Word <6^0" // 62
                    + "<2^0Word <7^0" // 76
                    + "<2^0Word w       <0^1" // 91
                    + "<2^0Word  wr     <2^1"
                    + "<2^0Word  wra    <3^1"
                    + "<2^0Word  wrap   <4^1";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestInsertOnce()
        {
            InsertText("a", buffer, view);
            Assert.AreEqual("> <2^0"
                + "<2^0a    <3^0", print.Output);
        }

        [Test]
        public void TestInsertTwice()
        {
            InsertText("ab", buffer, view);
            Assert.AreEqual("> <2^0"
                + "<2^0a    <3^0"
                + "<2^0ab   <4^0", print.Output);
        }

    }

}
