using MSG.IO;
using MSG.Types.Array;
using MSG.Types.String;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace MSGTest.IO.EditorTests
{
    [TestFixture]
    public class WordWrapperTests
    {
        Editor.Buffer buffer;
        EndlessArray<int> lineWidths;
        string text;
        Editor.WordWrapper wordWrapper;

        private Editor.WordWrapper CreateWordWrapper(string text, params int[] lineWidths)
        {
            this.text = text;
            this.lineWidths = new EndlessArray<int>(lineWidths);
            this.buffer = new Editor.Buffer(text, text.Length);
            return new Editor.WordWrapper(text, this.lineWidths);
        }

        [Test]
        public void TestLineEqualToWindowWidthYieldsOneLine()
        {
            wordWrapper = CreateWordWrapper("Word", 4);
            Assert.AreEqual(1, wordWrapper.Count);
            Assert.AreEqual(text, wordWrapper.GetLine(buffer.Text, 0));
        }

        [Test]
        public void TestLineShorterThanWindowWidthYieldsOneLine()
        {
            wordWrapper = CreateWordWrapper("Word", 10);
            Assert.AreEqual(1, wordWrapper.Count);
            Assert.AreEqual(text, wordWrapper.GetLine(buffer.Text, 0));
        }

        [Test]
        public void TestLineFilledToEndIgnoresSpaceThatWouldPrefixNextLine()
        {
            wordWrapper = CreateWordWrapper("Word soup", 4);
            Assert.AreEqual(3, wordWrapper.Count);
            Assert.AreEqual("Word", wordWrapper.GetLine(buffer.Text, 0));
            Assert.AreEqual(" ", wordWrapper.GetLine(buffer.Text, 1));
            Assert.AreEqual("soup", wordWrapper.GetLine(buffer.Text, 2));
        }

        [Test]
        public void TestLineWithWordLongerThanWindowWidthYieldsTwoLines()
        {
            wordWrapper = CreateWordWrapper("Words", 4);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word", wordWrapper.GetLine(buffer.Text, 0));
            Assert.AreEqual("s", wordWrapper.GetLine(buffer.Text, 1));
        }

        [Test]
        public void TestLineWithWordsLongerThanWindowWidthYieldsTwoLinesBrokenBetweenWords()
        {
            wordWrapper = CreateWordWrapper("Word break", 6);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word ", wordWrapper.GetLine(buffer.Text, 0));
            Assert.AreEqual("break", wordWrapper.GetLine(buffer.Text, 1));
        }

        [Test]
        public void TestWhitespaceBeforeAndAfterSoftBreakIsRespected()
        {
            wordWrapper = CreateWordWrapper("Word     break", 6);
            Assert.AreEqual(3, wordWrapper.Count);
            Assert.AreEqual("Word  ", wordWrapper.GetLine(buffer.Text, 0));
            Assert.AreEqual("   ", wordWrapper.GetLine(buffer.Text, 1));
            Assert.AreEqual("break", wordWrapper.GetLine(buffer.Text, 2));
        }

        [Test]
        public void TestHardBreaksAreRespected()
        {
            wordWrapper = CreateWordWrapper("Word\nbreak", 6);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word\n", wordWrapper.GetLine(buffer.Text, 0));
            Assert.AreEqual("break", wordWrapper.GetLine(buffer.Text, 1));
        }

        [Test]
        public void TestWhiteSpaceBeforeHardBreakIsRespected()
        {
            wordWrapper = CreateWordWrapper("Word \nbreak", 8);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word \n", wordWrapper.GetLine(buffer.Text, 0));
            Assert.AreEqual("break", wordWrapper.GetLine(buffer.Text, 1));
        }

        [Test]
        public void TestNonWhiteSpaceCharsBeforeHardBreakAreNotThrownAway()
        {
            wordWrapper = CreateWordWrapper("Word br\neak", 8);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word br\n", wordWrapper.GetLine(buffer.Text, 0));
            Assert.AreEqual("eak", wordWrapper.GetLine(buffer.Text, 1));
        }

        [Test]
        public void TestHardBreakResetsWordWrapLineStart()
        {
            wordWrapper = CreateWordWrapper("Word break fury is here.", 12);
            Assert.AreEqual(3, wordWrapper.Count);
            Assert.AreEqual("Word break ", wordWrapper.GetLine(buffer.Text, 0));
            Assert.AreEqual("fury is ", wordWrapper.GetLine(buffer.Text, 1));
            Assert.AreEqual("here.", wordWrapper.GetLine(buffer.Text, 2));
            wordWrapper = CreateWordWrapper("Word \nbreak fury is here.", 12);
            Assert.AreEqual(3, wordWrapper.Count);
            Assert.AreEqual("Word \n", wordWrapper.GetLine(buffer.Text, 0));
            Assert.AreEqual("break fury ", wordWrapper.GetLine(buffer.Text, 1));
            Assert.AreEqual("is here.", wordWrapper.GetLine(buffer.Text, 2));
        }

        [Test]
        public void TestWhitespaceAfterHardBreakIsRespected()
        {
            wordWrapper = CreateWordWrapper("Word\n     break", 12);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word\n", wordWrapper.GetLine(buffer.Text, 0));
            Assert.AreEqual("     break", wordWrapper.GetLine(buffer.Text, 1));
        }

        //[Test]
        //public void TestCursorPositionIsCorrectWithOneCharacter()
        //{
        //    wordWrapper = CreateWordWrapper("a", 10);
        //    Assert.AreEqual(1, wordWrapper.CursorPos.Left);
        //    Assert.AreEqual(0, wordWrapper.CursorPos.Top);
        //}

        //[Test]
        //public void TestCursorPositionIsCorrectWithTwoCharacters()
        //{
        //    wordWrapper = CreateWordWrapper("ab", 10);
        //    Assert.AreEqual(2, wordWrapper.CursorPos.Left);
        //    Assert.AreEqual(0, wordWrapper.CursorPos.Top);
        //}

        //[Test]
        //public void TestCursorPositionIsCorrectWhenTheresNoWrapping()
        //{
        //    wordWrapper = CreateWordWrapper("Word", 10);
        //    Assert.AreEqual(text.Length, wordWrapper.CursorPos.Left);
        //    Assert.AreEqual(0, wordWrapper.CursorPos.Top);
        //}

        //[Test]
        //public void TestCursorPositionIsCorrectAfterWrappingOnce()
        //{
        //    wordWrapper = CreateWordWrapper("Word break", 6);
        //    Assert.AreEqual(5, wordWrapper.CursorPos.Left);
        //    Assert.AreEqual(1, wordWrapper.CursorPos.Top);
        //}

        [Test]
        public void TestSpaceBeginningAfterWidthWrappedLineWrapsPreviousWord()
        {
            wordWrapper = CreateWordWrapper("Word soup ", 9);
            //Debug.WriteLine(wordWrapper.Count);
            //Debug.WriteLine(Format.ToLiteral(wordWrapper.GetLine(0)));
            //Debug.WriteLine(Format.ToLiteral(wordWrapper.GetLine(1)));
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word ", wordWrapper.GetLine(buffer.Text, 0));
            Assert.AreEqual("soup ", wordWrapper.GetLine(buffer.Text, 1));
        }
    }
}
