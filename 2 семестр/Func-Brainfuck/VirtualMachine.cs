using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public VirtualMachine(string program, int memorySize)
		{
            Instructions = program;
            InstructionPointer = 0;
            Memory = new byte[memorySize];
            MemoryPointer = 0;
		}

        private Dictionary<char, Action<IVirtualMachine>> commands = new Dictionary<char, Action<IVirtualMachine>>();

        public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
            commands.Add(symbol, execute);
		}

		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

		public void Run()
		{
            for (; InstructionPointer < Instructions.Length; InstructionPointer++)
            {
                var currentCommand = Instructions[InstructionPointer];
                if (commands.ContainsKey(currentCommand))
                    commands[currentCommand](this);
            }
        }
	}
}