using System;

namespace Priorities.Types
{
    public class Task : IEquatable<Task>
    {
        string name;
        int parent;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public Task(string name, int parent)
        {
            this.name = name;
            this.parent = parent;
        }

        public bool Equals(Task other)
        {
            return this.name.Equals(other.name);
        }

        public override int GetHashCode()
        {
            return name.GetHashCode() ^ parent.GetHashCode();
        }
    }

}
