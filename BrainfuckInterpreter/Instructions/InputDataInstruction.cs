namespace BrainfuckInterpreter.Instructions
{
    internal class InputDataInstruction : Instruction
    {
        public override void Execute(VirtualMachine vm)
        {
            vm.InputData();
        }
    }
}