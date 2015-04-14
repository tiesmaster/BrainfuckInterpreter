namespace BrainfuckInterpreter.Instructions
{
    internal class DecrementDataInstruction : Instruction
    {
        public override void Execute(VirtualMachine vm)
        {
            vm.DecrementData();
        }
    }
}