namespace BrainfuckInterpreter.Instructions
{
    internal class IncrementDataInstruction : Instruction
    {
        public override void Execute(VirtualMachine vm)
        {
            vm.IncrementData();
        }
    }
}