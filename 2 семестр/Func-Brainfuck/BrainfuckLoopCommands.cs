using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        private static Dictionary<int, int> bracketDictionary = new Dictionary<int, int>();
        private static Dictionary<int, int> reverseBracketDictionary = new Dictionary<int, int>();

        private static void MakeBracketDictionary(IVirtualMachine vm)
        {
            var tempStack = new Stack<int>();
            for (int i = 0; i < vm.Instructions.Length; i++)
            {
                if (vm.Instructions[i] == '[') tempStack.Push(i);
                if (vm.Instructions[i] == ']')
                {
                    reverseBracketDictionary[i] = tempStack.Peek();
                    bracketDictionary[tempStack.Pop()] = i;
                }
            }
        }

        public static void RegisterTo(IVirtualMachine vm)
        {
            MakeBracketDictionary(vm);

            vm.RegisterCommand('[', x => {
                if (x.Memory[x.MemoryPointer] == 0)
                    x.InstructionPointer = bracketDictionary[x.InstructionPointer];
            });

            vm.RegisterCommand(']', x => {
                if (x.Memory[x.MemoryPointer] != 0)
                    x.InstructionPointer = reverseBracketDictionary[x.InstructionPointer];
            });
        }
    }
}