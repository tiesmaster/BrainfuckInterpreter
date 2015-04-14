namespace BrainfuckInterpreter.Instructions
{
    internal class DecrementDataPointerInstruction : Instruction
    {
        public override void Execute(VirtualMachine vm)
        {
            vm.DataPointer--;
        }
    }
}