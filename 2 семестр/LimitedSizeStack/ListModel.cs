using System;
using System.Collections.Generic;

namespace TodoApplication
{
    public class ListModel<TItem>
    {
        public List<TItem> Items { get; }
        public int Limit;
        private LimitedSizeStack<Tuple<TItem, int, string>> cancel;

        public ListModel(int limit)
        {
            Items = new List<TItem>();
            Limit = limit;
            cancel = new LimitedSizeStack<Tuple<TItem, int, string>>(Limit);
        }

        public void AddItem(TItem item)
        {
            Items.Add(item);
            cancel.Push(new Tuple<TItem, int, string>(item, Items.Count - 1, "Add"));
        }

        public void RemoveItem(int index)
        {
            cancel.Push(new Tuple<TItem, int, string>(Items[index], index, "Remove"));
            Items.RemoveAt(index);
        }

        public bool CanUndo()
        {
            return cancel.Count > 0;
        }

        public void Undo()
        {
            var undoElement = cancel.Pop();
            if (undoElement.Item3 == "Add")
                Items.RemoveAt(undoElement.Item2);
            else Items.Insert(undoElement.Item2, undoElement.Item1);
        }
    }
}