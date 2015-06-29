using MSG.IO;
using MSG.Types.String;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace MSGTest.IO
{
    [TestFixture]
    public class EditorNewBufferTests
    {
        Editor.Buffer buffer; // creation buffer

        [SetUp]
        public void SetUp()
        {
            buffer = new Editor.Buffer();
        }

        [Test]
        public void TestBackspaceHasNoEffect()
        {
            buffer.Backspace();
            Assert.AreEqual("", buffer.Text);
            Assert.AreEqual(0, buffer.Point);
        }

        [Test]
        public void TestClearResetsCursorPosition()
        {
            buffer.Clear();
            Assert.AreEqual(0, buffer.Point);
        }

        [Test]
        public void TestClearResetsText()
        {
            buffer.Clear();
            Assert.AreEqual("", buffer.Text);
        }

        [Test]
        public void TestCreatingEmptyBufferResetsCursorPosition()
        {
            Assert.AreEqual(0, buffer.Point);
        }

        [Test]
        public void TestCreatingEmptyBufferResetsText()
        {
            Assert.AreEqual("", buffer.Text);
        }

        [Test]
        public void TestCursorLeftHasNoEffectOnEmptyBuffer()
        {
            buffer.RetreatPoint();
            Assert.AreEqual(0, buffer.Point);
            Assert.AreEqual("", buffer.Text);
        }

        [Test]
        public void TestCursorRightHasNoEffectOnEmptyBuffer()
        {
            buffer.AdvancePoint();
            Assert.AreEqual(0, buffer.Point);
            Assert.AreEqual("", buffer.Text);
        }

        [Test]
        public void TestDeleteHasNoEffectOnEmptyBuffer()
        {
            buffer.Delete();
            Assert.AreEqual("", buffer.Text);
            Assert.AreEqual(0, buffer.Point);
        }

        [Test]
        public void TestInsertCharIntoEmptyBuffer()
        {
            buffer.Insert('z');
            Assert.AreEqual("z", buffer.Text);
        }

        [Test]
        public void TestIsEmptyIsTrueOnEmptyBuffer()
        {
            Assert.IsTrue(buffer.IsEmpty());
        }

        [Test]
        public void TestToStringOnEmptyBufferReturnsEmptyString()
        {
            Assert.AreEqual("", buffer.ToString());
        }
    }

    [TestFixture]
    public class EditorOldBufferTests
    {
        Editor.Buffer buffer; // editing buffer

        [SetUp]
        public void SetUp()
        {
            buffer = new Editor.Buffer("abcd", 4); // editing "abcd", cursor position 4
        }

        [Test]
        public void TestCreatingExistingTextBufferSetsInitialCursorPosition()
        {
            Assert.AreEqual(4, buffer.Point);
        }

        [Test]
        public void TestCreatingExistingTextBufferSetsInitialString()
        {
            Assert.AreEqual("abcd", buffer.Text);
        }

        [Test]
        public void TestBackspaceRemovesCharBeforeCursor()
        {
            buffer.Backspace();
            Assert.AreEqual("abc", buffer.Text);
        }

        [Test]
        public void TestBackspaceMovesCursorBackOne()
        {
            buffer.Backspace();
            Assert.AreEqual(3, buffer.Point);
        }

        [Test]
        public void TestBackspaceAndInsertReplacesChar()
        {
            buffer.Backspace();
            buffer.Insert('x');
            Assert.AreEqual("abcx", buffer.Text);
        }

        [Test]
        public void TestBackspaceAndInsertRestoresCursorToSamePosition()
        {
            buffer.Backspace();
            buffer.Insert('x');
            Assert.AreEqual(4, buffer.Point);
        }

        [Test]
        public void TestClearResetsCursorPosition()
        {
            buffer.Clear();
            Assert.AreEqual(0, buffer.Point);
        }

        [Test]
        public void TestClearResetsText()
        {
            buffer.Clear();
            Assert.AreEqual("", buffer.Text);
        }

        [Test]
        public void TestCursorLeftAndInsert()
        {
            buffer.RetreatPoint();
            buffer.Insert('x');
            Assert.AreEqual("abcxd", buffer.Text);
        }

        [Test]
        public void TestCursorLeftAndInsertRestoresCursorPosition()
        {
            buffer.RetreatPoint();
            buffer.Insert('x');
            Assert.AreEqual(4, buffer.Point);
        }

        [Test]
        public void TestCursorLeftMovesCursorLeftOne()
        {
            buffer.RetreatPoint();
            Assert.AreEqual(3, buffer.Point);
        }

        [Test]
        public void TestCursorLeftHasNoEffectOnText()
        {
            buffer.RetreatPoint();
            Assert.AreEqual("abcd", buffer.Text);
        }

        [Test]
        public void TestCursorRightAtEndHasNoEffectOnCursorPosition()
        {
            buffer.AdvancePoint();
            Assert.AreEqual(4, buffer.Point);
        }

        [Test]
        public void TestCursorRightAtEndHasNoEffectOnText()
        {
            buffer.AdvancePoint();
            Assert.AreEqual("abcd", buffer.Text);
        }

        [Test]
        public void TestDeleteAtEndDoesntMoveCursor()
        {
            buffer.Delete();
            Assert.AreEqual(4, buffer.Point);
        }

        [Test]
        public void TestDeleteAtEndDoesntRemoveChar()
        {
            buffer.Delete();
            Assert.AreEqual("abcd", buffer.Text);
        }

        [Test]
        public void TestInsertCharAtEnd()
        {
            buffer.Insert('x');
            Assert.AreEqual("abcdx", buffer.Text);
        }

        [Test]
        public void TestIsEmptyIsFalse()
        {
            Assert.IsFalse(buffer.IsEmpty());
        }

        [Test]
        public void TestToStringReturnsText()
        {
            Assert.AreEqual("abcd", buffer.ToString());
        }
    }

}