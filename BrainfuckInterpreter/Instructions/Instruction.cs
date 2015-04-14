namespace BrainfuckInterpreter.Instructions
{
    internal abstract class Instruction
    {
        public abstract void Execute(VirtualMachine vm);
    }
}