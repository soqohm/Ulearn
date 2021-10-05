using System;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
        public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
        {
            vm.RegisterCommand('.', x => write(Convert.ToChar(x.Memory[x.MemoryPointer])));
            vm.RegisterCommand(',', x => x.Memory[x.MemoryPointer] = Convert.ToByte(read()));

            vm.RegisterCommand('+', x => {
                if (x.Memory[x.MemoryPointer] == 255) x.Memory[x.MemoryPointer] = 0;
                else x.Memory[x.MemoryPointer]++;});
            vm.RegisterCommand('-', x => {
                if (x.Memory[x.MemoryPointer] == 0) x.Memory[x.MemoryPointer] = 255;
                else x.Memory[x.MemoryPointer]--;});

            vm.RegisterCommand('>', x => x.MemoryPointer = (x.MemoryPointer + 1) % x.Memory.Length);
            vm.RegisterCommand('<', x => {
                x.MemoryPointer = (x.MemoryPointer + x.Memory.Length - 1) % x.Memory.Length;});

            //const string symbols = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            //foreach (var s in symbols)
            //    vm.RegisterCommand(s, x => x.Memory[x.MemoryPointer] = Convert.ToByte(s));

            for (int i = 48; i < 123; i++) {
                if (i < 58 || (i > 64 && i < 91) || i > 96) { //A-Z a-z 0-9
                    var s = Convert.ToChar(i);
                    vm.RegisterCommand(s, x => x.Memory[x.MemoryPointer] = Convert.ToByte(s));
                }
            }
        }
	}
}