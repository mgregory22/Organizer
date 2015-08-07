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
                buffer.RetreatPoint();
                view.UpdateCursor(buffer.Point);
            }
        }

        private void CursorRight(int count, Editor.Buffer buffer, Editor.View view)
        {
            for (int i = 0; i < count; i++)
            {
                buffer.AdvancePoint();
                view.UpdateCursor(buffer.Point);
            }
        }

        private void InsertText(string text, Editor.Buffer buffer, Editor.View view)
        {
            foreach (char c in text)
            {
                buffer.Insert(c);
                view.RedrawEditor(buffer.Text, buffer.Point);
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
            view = new Editor.View(buffer, print);
        }

        [Test]
        public void TestBackspaceThatUnwrapsErasesSecondLineAndMovesCursorToBeginningOfBlankLine()
        {
            InsertText("Word wr", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            buffer.RetreatPoint();
            buffer.Delete();
            view.RedrawEditor(buffer.Text, buffer.Point);
            string expected = "<7^0w  <0^1";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestBackspaceAtEndOfTextToPreviousLineErasesLastCharacterOnThatLineAndPutsCursorAtTheEnd()
        {
            InsertText("234567", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            buffer.RetreatPoint();
            buffer.Delete();
            view.RedrawEditor(buffer.Text, buffer.Point);
            string expected = "<7^0 <7^0";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorDownAtEndOfSingleLineOfInputHasNoEffect()
        {
            InsertText("2345", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            int point = view.CursorDown(buffer.Point);
            buffer.MovePoint(point);
            Assert.AreEqual("", print.Output);
        }

        [Test]
        public void TestCursorDownInMiddleOfSingleLineOfInputHasNoEffect()
        {
            InsertText("2345", buffer, view);
            CursorLeft(2, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            int point = view.CursorDown(buffer.Point);
            buffer.MovePoint(point);
            Assert.AreEqual("", print.Output);
        }

        [Test]
        public void TestCursorDownOnFirstLineOfTwoPutsCursorDirectlyUnderOriginalPosition()
        {
            InsertText("23456 01234567", buffer, view);
            CursorLeft(11, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            int point = view.CursorDown(buffer.Point);
            buffer.MovePoint(point);
            Assert.AreEqual("<5^1", print.Output);
        }

        [Test]
        public void TestCursorDownOnSecondLineOfThreePutsCursorDirectlyUnderOriginalPosition()
        {
            InsertText("23456 01234567012345", buffer, view);
            CursorLeft(11, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            int point = view.CursorDown(buffer.Point);
            buffer.MovePoint(point);
            Assert.AreEqual("<3^2", print.Output);
        }

        [Test]
        public void TestCursorDownOnEmptyLineHasNoEffect()
        {
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            int point = view.CursorDown(buffer.Point);
            buffer.MovePoint(point);
            Assert.AreEqual("", print.Output);
        }

        [Test]
        public void TestCursorEndMovesCursorToEndOfSingleLine()
        {
            InsertText("2345", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorLeft(3, buffer, view);
            view.CursorEnd();
            Assert.AreEqual("<5^0<4^0<3^0<6^0", print.Output);
        }

        [Test]
        public void TestCursorEndOnSecondLineMovesCursorToEndOfSecondLine()
        {
            InsertText("2345 70123", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorLeft(3, buffer, view);
            view.CursorEnd();
            Assert.AreEqual("<4^1<3^1<2^1<5^1", print.Output);
        }

        [Test]
        public void TestCursorEndOnFirstLineOfTwoLineEntryMovesCursorToEndOfFirstLine()
        {
            InsertText("23456 0123456", buffer, view);
            CursorLeft(12, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            view.CursorEnd();
            Assert.AreEqual("<7^0", print.Output);
        }

        [Test]
        public void TestCursorEndOnFullLinePutsCursorAtEndOfSameLine()
        {
            InsertText("23456 01234567", buffer, view);
            CursorLeft(3, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            view.CursorEnd();
            Assert.AreEqual("<7^1", print.Output);
        }

        [Test]
        public void TestCursorHomeMovesCursorToStartOfSingleLine()
        {
            InsertText("2345", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            view.CursorHome();
            Assert.AreEqual("<2^0", print.Output);
        }

        [Test]
        public void TestCursorHomeOnSecondLineMovesCursorToStartOfSecondLine()
        {
            InsertText("2345 70123", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            view.CursorHome();
            Assert.AreEqual("<0^1", print.Output);
        }

        [Test]
        public void TestCursorHomeOnFirstLineOfTwoLineInputMovesCursorToStartOfFirstLine()
        {
            InsertText("2345 70123", buffer, view);
            CursorLeft(6, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            view.CursorHome();
            Assert.AreEqual("<2^0", print.Output);
        }

        [Test]
        public void TestCursorLeftAtBeginningOfNewLineBringsCursorToEndOfPreviousLine()
        {
            InsertText("2345 70", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorLeft(3, buffer, view);
            string expected = "<1^1<0^1<6^0";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorLeftCannotMoveCursorBeforeBeginning()
        {
            buffer.RetreatPoint();
            view.UpdateCursor(buffer.Point);
            Assert.AreEqual(prompt.Length, print.CursorLeft);
        }

        [Test]
        public void TestCursorLeftIntoFullLinePutsCursorAtEndOfLine()
        {
            InsertText("234567 ", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorLeft(2, buffer, view);
            string expected = "<0^1<7^0";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorLeftWorksOnSecondLine()
        {
            InsertText("2345\n01234", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorLeft(1, buffer, view);
            string expected = "<4^1";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorLeftWorksOnWrappedSecondLine()
        {
            InsertText("2345 7012", buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorLeft(1, buffer, view);
            string expected = "<3^1";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorRightBeforeEndOfFirstLineAndBeforeEndOfBufferMovesCursorOneRight()
        {
            InsertText("2345", buffer, view);
            CursorLeft(3, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorRight(1, buffer, view);
            string expected = "<4^0";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorRightAtEndOfFirstLineAndBeforeEndOfBufferPutsCursorAtBeginningOfNextLine()
        {
            InsertText("23456701234", buffer, view);
            CursorLeft(6, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            CursorRight(1, buffer, view);
            string expected = "<0^1";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestCursorRightCannotMoveCursorPastEnd()
        {
            buffer.AdvancePoint();
            view.UpdateCursor(buffer.Point);
            Assert.AreEqual(prompt.Length, print.CursorLeft);
        }

        [Test]
        public void TestCursorUpInMiddleOfSecondLineOfTwoPutsCursorDirectlyAboveOriginalPosition()
        {
            InsertText("23456 01234567", buffer, view);
            CursorLeft(5, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            int point = view.CursorUp(buffer.Point);
            buffer.MovePoint(point);
            Assert.AreEqual("<3^0", print.Output);
        }

        [Test]
        public void TestCursorUpInMiddleOfThirdLineOfThreePutsCursorDirectlyAboveOriginalPosition()
        {
            InsertText("23456 01234567012345", buffer, view);
            CursorLeft(3, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            int point = view.CursorUp(buffer.Point);
            buffer.MovePoint(point);
            Assert.AreEqual("<3^1", print.Output);
        }

        [Test]
        public void TestCursorUpToShorterLinesAndBackDownToOriginalLinePutsCursorInOriginalColumn()
        {
            InsertText("1234 12345 123456 1234567", buffer, view);
            CursorLeft(1, buffer, view);
            view.CursorUp(buffer.Point);
            view.CursorUp(buffer.Point);
            view.CursorDown(buffer.Point);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            int point = view.CursorDown(buffer.Point);
            buffer.MovePoint(point);
            Assert.AreEqual("<8^3", print.Output);
        }

        [Test]
        public void TestCursorUpToTheRightOfLastCharInFirstLinePutsCursorOnLastCharOfFirstLine()
        {
            InsertText("2345 70123456", buffer, view);
            CursorLeft(1, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            int point = view.CursorUp(buffer.Point);
            buffer.MovePoint(point);
            Assert.AreEqual("<6^0", print.Output);
        }

        [Test]
        public void TestCursorUpUnderLastCharInFirstLinePutsCursorOnLastCharOfFirstLine()
        {
            InsertText("2345 70123456", buffer, view);
            CursorLeft(2, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            int point = view.CursorUp(buffer.Point);
            buffer.MovePoint(point);
            Assert.AreEqual("<6^0", print.Output);
        }

        [Test]
        public void TestEnterOnLinesOtherThanTheLastStillPrintsNextPromptAfterAllTheEnteredLines()
        {
            InsertText("2345 70 23456701 34 67012 456", buffer, view);
            CursorLeft(19, buffer, view);
            SkipTestingTheInsertOutputSinceItsLongAndBoring();
            view.ExitEditor();
            Assert.AreEqual("<0^5\n", print.Output);
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
            Assert.DoesNotThrow(() => InsertText("234567 1234", buffer, view));
        }

        [Test]
        public void TestInsertLineThatNeedsToBeWrapped()
        {
            InsertText("Word wrap", buffer, view);
            string expected = "> Word <6^0 w<0^1<7^0 wrap";
            Assert.AreEqual(expected, print.Output);
        }

        [Test]
        public void TestInsertOnce()
        {
            InsertText("a", buffer, view);
            Assert.AreEqual("> a", print.Output);
        }

        [Test]
        public void TestInsertTwice()
        {
            InsertText("ab", buffer, view);
            Assert.AreEqual("> ab", print.Output);
        }

    }

}
