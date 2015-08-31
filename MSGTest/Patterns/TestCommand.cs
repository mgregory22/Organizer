//
// MSGTest/Patterns/TestCommand.cs
//

using MSG.Patterns;

namespace MSGTest.Patterns
{
    class TestCommand : Command
    {
        public int doCount;
        public int redoCount;
        public int undoCount;
        public TestCommand()
        {
        }
        override public void Do()
        {
            doCount++;
        }
        override public void Redo()
        {
            redoCount++;
        }
        override public void Undo()
        {
            undoCount++;
        }
    }
}
