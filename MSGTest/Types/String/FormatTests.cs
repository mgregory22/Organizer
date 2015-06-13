using MSG.IO;
using MSG.Types.String;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace MSGTest.Types.String
{
    [TestFixture]
    public class FormatTests
    {
        [Test]
        public void TestToLiteral()
        {
            Assert.AreEqual("\"\\t\\n\"", Format.ToLiteral("\t\n"));
        }

        [Test]
        public void TestPadding()
        {
            Assert.AreEqual("   ", Format.Padding(3));
        }
    }
}
