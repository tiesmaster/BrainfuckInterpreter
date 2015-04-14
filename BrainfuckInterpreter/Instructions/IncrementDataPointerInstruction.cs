namespace BrainfuckInterpreter.Instructions
{
    internal class IncrementDataPointerInstruction : Instruction
    {
        public override void Execute(VirtualMachine vm)
        {
            vm.DataPointer++;
        }
    }
}