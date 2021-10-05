using System.Collections.Generic;

namespace Clones
{
    public class CloneVersionSystem : ICloneVersionSystem
    {
        private List<Clone> cloneList;

        public CloneVersionSystem() { cloneList = new List<Clone> { new Clone() }; }

        public string Execute(string query)
        {
            var queryArr = query.Split(' ');
            var cloneNum = int.Parse(queryArr[1]) - 1;

            switch (queryArr[0])
            {
                case "learn":
                    cloneList[cloneNum].Learn(queryArr[2]);
                    return null;
                case "rollback":
                    cloneList[cloneNum].RollBack();
                    return null;
                case "relearn":
                    cloneList[cloneNum].Relearn();
                    return null;
                case "clone":
                    cloneList.Add(new Clone(cloneList[cloneNum]));
                    return null;
                case "check":
                    return cloneList[cloneNum].Check();
                default:
                    return null;
            }
        }

        public class Clone
        {
            private readonly Stack learnedProgram;
            private readonly Stack rollBackHistory;

            public Clone()
            {
                learnedProgram = new Stack();
                rollBackHistory = new Stack();
            }

            public void Learn(string newProgram)
            {
                rollBackHistory.Clear();
                learnedProgram.Push(newProgram);
            }

            public void RollBack() { rollBackHistory.Push(learnedProgram.Pop()); }

            public void Relearn() { learnedProgram.Push(rollBackHistory.Pop()); }

            public Clone(Clone anotherClone)
            {
                learnedProgram = anotherClone.learnedProgram.Clone();
                rollBackHistory = anotherClone.rollBackHistory.Clone();
            }

            public string Check() { return learnedProgram.IsEmpty() ? "basic" : learnedProgram.Peek(); }
        }

        public class StackItem
        {
            public string Value;
            public StackItem Previous;
        }

        public class Stack
        {
            private StackItem tail;

            public Stack() { }

            public void Push(string value) { tail = new StackItem { Value = value, Previous = tail }; }

            public string Pop()
            {
                var deletedElement = tail.Value;
                tail = tail.Previous;
                return deletedElement;
            }

            public Stack Clone() { return new Stack() { tail = tail }; }

            public string Peek() { return tail.Value; }

            public void Clear() { tail = null; }

            public bool IsEmpty() { return tail == null; }
        }
    }
}