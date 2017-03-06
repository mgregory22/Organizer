//
// OrganizerTest/DriverTests.cs
//

using MSG.Console;
using MSG.IO;
using MSG.Types.Dir;
using MSGTest.IO;
using NUnit.Framework;
using Organizer;
using Organizer.Modules.Tasks;

namespace OrganizerTest
{
    [TestFixture]
    public class DbDirDriverTests
    {

        [Test]
        public void TestInitialPromptDisplaysHowToGetHelp()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);

            using (Database db = new Database("DbDirDriverTests.s3db", Database.OpenStyle.RecreateExistent)) {
                DbDir<Task> tasksDbDir = new DbDir<Task>(db, "Tasks");
                read.PushString("q");
                CharPrompt prompt = new CharPrompt();

                Driver.Run(io, prompt, tasksDbDir);

                // The menu should display once how to get help
                string start = string.Format("\nMain Menu (? for help)\n{0}q\n", prompt.Prompt);
                Assert.AreEqual(start.Length, print.Output.Length,
                    string.Format("\"{0}\" != \"{1}\"", start, print.Output));
                Assert.AreEqual(start, print.Output.Substring(0, start.Length));
            }
        }

        [Test]
        public void TestHowToGetHelpDisplaysOnceOnly()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);

            using (Database db = new Database("DbDirDriverTests.s3db", Database.OpenStyle.RecreateExistent)) {
                DbDir<Task> tasksDbDir = new DbDir<Task>(db, "Tasks");
                read.PushString("?q");
                CharPrompt prompt = new CharPrompt();

                Driver.Run(io, prompt, tasksDbDir);

                // The second Main Menu prompt should not have a help message
                string end = string.Format("\n\nMain Menu\n{0}q\n", prompt.Prompt);
                Assert.AreEqual(end, print.Output.Substring(print.Output.Length - end.Length));
            }
        }

        [Test]
        public void TestMainMenuAppearsWhenActivated()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);

            using (Database db = new Database("DbDirDriverTests.s3db", Database.OpenStyle.RecreateExistent)) {
                DbDir<Task> tasksDbDir = new DbDir<Task>(db, "Tasks");
                read.PushString("?q");
                CharPrompt prompt = new CharPrompt();

                Driver.Run(io, prompt, tasksDbDir);

                // Look for Task Menu prompt
                string[] testStrings = {
                    "Main Menu"
                    , "New File"
                    , "Open File"
                    , "Tasks"
                    , "Quit Program"
                };
                foreach (string testString in testStrings) {
                    Assert.IsTrue(print.Output.Contains(testString), string.Format(
                        "\"{0}\" not contained in {1}"
                        , testString
                        , print.Output
                    ));
                }
            }
        }

        [Test]
        public void TestTaskMenuAppearsWhenActivated()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);

            using (Database db = new Database("DbDirDriverTests.s3db", Database.OpenStyle.RecreateExistent)) {
                DbDir<Task> tasksDbDir = new DbDir<Task>(db, "Tasks");
                read.PushString("t?qq");
                CharPrompt prompt = new CharPrompt();

                Driver.Run(io, prompt, tasksDbDir);

                // Look for Task Menu prompt
                string[] testStrings = {
                    "Task Menu"
                    , "Add Task"
                    , "List Tasks"
                    , "Quit To Main"
                };
                foreach (string testString in testStrings) {
                    Assert.IsTrue(print.Output.Contains(testString), string.Format(
                        "\"{0}\" not contained in {1}"
                        , testString
                        , print.Output
                    ));
                }
            }
        }

        [Test]
        public void TestTaskMenuShouldNotShowExistentItemCommandsWithNoItems()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);

            using (Database db = new Database("DbDirDriverTests.s3db", Database.OpenStyle.RecreateExistent)) {
                DbDir<Task> tasksDbDir = new DbDir<Task>(db, "Tasks");
                read.PushString("t?qq");
                CharPrompt prompt = new CharPrompt();

                Driver.Run(io, prompt, tasksDbDir);

                // Look for Task Menu prompt
                string[] testStrings = {
                    "Delete Task"
                    , "Rename Task"
                    , "Edit Subtasks"
                };
                foreach (string testString in testStrings) {
                    Assert.IsFalse(print.Output.Contains(testString), string.Format(
                        "\"{0}\" is contained in {1}\nbut should not"
                        , testString
                        , print.Output
                    ));
                }
            }
        }

        [Test]
        public void TestUpToParentInTaskMenuWithNoTasksShouldNotCrashProgram()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);

            using (Database db = new Database("DbDirDriverTests.s3db", Database.OpenStyle.RecreateExistent)) {
                DbDir<Task> tasksDbDir = new DbDir<Task>(db, "Tasks");
                read.PushString("tuqq");
                CharPrompt prompt = new CharPrompt();

                Assert.DoesNotThrow(() => Driver.Run(io, prompt, tasksDbDir));
            }
        }
    }

    [TestFixture]
    public class MemDirDriverTests
    {
        [Test]
        public void TestInitialPromptDisplaysHowToGetHelp()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);
            MemDir<Task> tasksMemDir = new MemDir<Task>();

            read.PushString("q");
            CharPrompt prompt = new CharPrompt();

            Driver.Run(io, prompt, tasksMemDir);

            // The menu should display once how to get help
            string start = string.Format("\nMain Menu (? for help)\n{0}q\n", prompt.Prompt);
            Assert.AreEqual(start.Length, print.Output.Length,
                string.Format("\"{0}\" != \"{1}\"", start, print.Output));
            Assert.AreEqual(start, print.Output.Substring(0, start.Length));
        }

        [Test]
        public void TestHowToGetHelpDisplaysOnceOnly()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);
            MemDir<Task> tasksMemDir = new MemDir<Task>();

            read.PushString("?q");
            CharPrompt prompt = new CharPrompt();

            Driver.Run(io, prompt, tasksMemDir);

            // The second Main Menu prompt should not have a help message
            string end = string.Format("\n\nMain Menu\n{0}q\n", prompt.Prompt);
            Assert.AreEqual(end, print.Output.Substring(print.Output.Length - end.Length));
        }
        
        [Test]
        public void TestMainMenuAppearsWhenActivated()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);
            MemDir<Task> tasksMemDir = new MemDir<Task>();

            read.PushString("?q");
            CharPrompt prompt = new CharPrompt();

            Driver.Run(io, prompt, tasksMemDir);

            // Look for Task Menu prompt
            string[] testStrings = {
                "Main Menu"
                , "New File"
                , "Open File"
                , "Tasks"
                , "Quit Program"
            };
            foreach (string testString in testStrings) {
                Assert.IsTrue(print.Output.Contains(testString), string.Format(
                    "\"{0}\" not contained in {1}"
                    , testString
                    , print.Output
                ));
            }
        }

        [Test]
        public void TestTaskMenuAppearsWhenActivated()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);
            MemDir<Task> tasksMemDir = new MemDir<Task>();

            read.PushString("t?qq");
            CharPrompt prompt = new CharPrompt();

            Driver.Run(io, prompt, tasksMemDir);

            // Look for Task Menu prompt
            string[] testStrings = {
                "Task Menu"
                , "Add Task"
                , "List Tasks"
                , "Quit To Main"
            };
            foreach (string testString in testStrings) {
                Assert.IsTrue(print.Output.Contains(testString), string.Format(
                    "\"{0}\" not contained in {1}"
                    , testString
                    , print.Output
                ));
            }
        }

        [Test]
        public void TestTaskMenuShouldNotShowExistentItemCommandsWithNoItems()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);
            MemDir<Task> tasksMemDir = new MemDir<Task>();

            read.PushString("t?qq");
            CharPrompt prompt = new CharPrompt();

            Driver.Run(io, prompt, tasksMemDir);

            // Look for Task Menu prompt
            string[] testStrings = {
                "Delete Task"
                , "Rename Task"
                , "Edit Subtasks"
            };
            foreach (string testString in testStrings) {
                Assert.IsFalse(print.Output.Contains(testString), string.Format(
                    "\"{0}\" is contained in {1}\nbut should not"
                    , testString
                    , print.Output
                ));
            }
        }

        [Test]
        public void TestUpToParentInTaskMenuWithNoTasksShouldNotCrashProgram()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);

            MemDir<Task> tasksMemDir = new MemDir<Task>();
            read.PushString("tuqq");
            CharPrompt prompt = new CharPrompt();

            Assert.DoesNotThrow(() => Driver.Run(io, prompt, tasksMemDir));
        }

        [Test]
        public void TestUndoAfterDescendingIntoNewlyAddedItemShouldNotCrashProgram()
        {
            TestPrint print = new TestPrint();
            TestRead read = new TestRead();
            Io io = new Io(print, read);

            MemDir<Task> tasksMemDir = new MemDir<Task>();
            read.PushString("taTask\rs1\rzqq");
            CharPrompt prompt = new CharPrompt();

            Assert.DoesNotThrow(() => Driver.Run(io, prompt, tasksMemDir));
        }
    }
}
