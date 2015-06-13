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
            return new Editor.WordWrapper(this.buffer, this.lineWidths);
        }

        [Test]
        public void TestLineEqualToWindowWidthYieldsOneLine()
        {
            wordWrapper = CreateWordWrapper("Word", 4);
            Assert.AreEqual(1, wordWrapper.Count);
            Assert.AreEqual(text, wordWrapper[0].ToString());
        }

        [Test]
        public void TestLineShorterThanWindowWidthYieldsOneLine()
        {
            wordWrapper = CreateWordWrapper("Word", 10);
            Assert.AreEqual(1, wordWrapper.Count);
            Assert.AreEqual(text, wordWrapper[0].ToString());
        }

        [Test]
        public void TestLineFilledToEndIgnoresSpaceThatWouldPrefixNextLine()
        {
            wordWrapper = CreateWordWrapper("Word soup", 4);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word", wordWrapper[0].ToString());
            Assert.AreEqual("soup", wordWrapper[1].ToString());
        }

        [Test]
        public void TestLineWithWordLongerThanWindowWidthYieldsTwoLines()
        {
            wordWrapper = CreateWordWrapper("Words", 4);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word", wordWrapper[0].ToString());
            Assert.AreEqual("s", wordWrapper[1].ToString());
        }

        [Test]
        public void TestLineWithWordsLongerThanWindowWidthYieldsTwoLinesBrokenBetweenWords()
        {
            wordWrapper = CreateWordWrapper("Word break", 6);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word", wordWrapper[0].ToString());
            Assert.AreEqual("break", wordWrapper[1].ToString());
        }

        [Test]
        public void TestWhitespaceBeforeAndAfterSoftBreakIsIgnored()
        {
            wordWrapper = CreateWordWrapper("Word     break", 6);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word", wordWrapper[0].ToString());
            Assert.AreEqual("break", wordWrapper[1].ToString());
        }

        [Test]
        public void TestHardBreaksAreRespected()
        {
            wordWrapper = CreateWordWrapper("Word\nbreak", 6);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word", wordWrapper[0].ToString());
            Assert.AreEqual("break", wordWrapper[1].ToString());
        }

        [Test]
        public void TestWhiteSpaceBeforeHardBreakIsIgnored()
        {
            wordWrapper = CreateWordWrapper("Word \nbreak", 8);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word", wordWrapper[0].ToString());
            Assert.AreEqual("break", wordWrapper[1].ToString());
        }

        [Test]
        public void TestNonWhiteSpaceCharsBeforeHardBreakAreNotThrownAway()
        {
            wordWrapper = CreateWordWrapper("Word br\neak", 8);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word br", wordWrapper[0].ToString());
            Assert.AreEqual("eak", wordWrapper[1].ToString());
        }

        [Test]
        public void TestHardBreakResetsWordWrapLineStart()
        {
            wordWrapper = CreateWordWrapper("Word break fury is here.", 12);
            Assert.AreEqual(3, wordWrapper.Count);
            Assert.AreEqual("Word break", wordWrapper[0].ToString());
            Assert.AreEqual("fury is", wordWrapper[1].ToString());
            Assert.AreEqual("here.", wordWrapper[2].ToString());
            wordWrapper = CreateWordWrapper("Word \nbreak fury is here.", 12);
            Assert.AreEqual(3, wordWrapper.Count);
            Assert.AreEqual("Word", wordWrapper[0].ToString());
            Assert.AreEqual("break fury", wordWrapper[1].ToString());
            Assert.AreEqual("is here.", wordWrapper[2].ToString());
        }

        [Test]
        public void TestWhitespaceAfterHardBreakIsRespected()
        {
            wordWrapper = CreateWordWrapper("Word\n     break", 12);
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word", wordWrapper[0].ToString());
            Assert.AreEqual("     break", wordWrapper[1].ToString());
        }

        [Test]
        public void TestCursorPositionIsCorrectWithOneCharacter()
        {
            wordWrapper = CreateWordWrapper("a", 10);
            Assert.AreEqual(1, wordWrapper.CursorPos.Left);
            Assert.AreEqual(0, wordWrapper.CursorPos.Top);
        }

        [Test]
        public void TestCursorPositionIsCorrectWithTwoCharacters()
        {
            wordWrapper = CreateWordWrapper("ab", 10);
            Assert.AreEqual(2, wordWrapper.CursorPos.Left);
            Assert.AreEqual(0, wordWrapper.CursorPos.Top);
        }

        [Test]
        public void TestCursorPositionIsCorrectWhenTheresNoWrapping()
        {
            wordWrapper = CreateWordWrapper("Word", 10);
            Assert.AreEqual(text.Length, wordWrapper.CursorPos.Left);
            Assert.AreEqual(0, wordWrapper.CursorPos.Top);
        }

        [Test]
        public void TestCursorPositionIsCorrectAfterWrappingOnce()
        {
            wordWrapper = CreateWordWrapper("Word break", 6);
            Assert.AreEqual(5, wordWrapper.CursorPos.Left);
            Assert.AreEqual(1, wordWrapper.CursorPos.Top);
        }

        [Test]
        public void TestSpaceBeginningAfterWidthWrappedLineDoesNotWrapPreviousWord()
        {
            wordWrapper = CreateWordWrapper("Word soup ", 9);
            //Debug.WriteLine(wordWrapper.Count);
            //Debug.WriteLine(Format.ToLiteral(wordWrapper[0].ToString()));
            //Debug.WriteLine(Format.ToLiteral(wordWrapper[1].ToString()));
            Assert.AreEqual(2, wordWrapper.Count);
            Assert.AreEqual("Word soup", wordWrapper[0].ToString());
            Assert.AreEqual("", wordWrapper[1].ToString());
        }
    }
}
