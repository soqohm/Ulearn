using System.Collections.Generic;

namespace TodoApplication
{
    public class LimitedSizeStack<T>
    {
        private LinkedList<T> link = new LinkedList<T>();
        private int limit;

        public LimitedSizeStack(int limit) { this.limit = limit; }

        public void Push(T item)
        {
            link.AddLast(item);
            if (Count > limit)
                link.RemoveFirst();
        }

        public T Pop()
        {
            var deletedElement = link.Last.Value;
            link.RemoveLast();
            return deletedElement;
        }

        public int Count { get { return link.Count; } }
    }
}