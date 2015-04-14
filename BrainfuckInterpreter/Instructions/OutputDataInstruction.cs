namespace BrainfuckInterpreter.Instructions
{
    internal class OutputDataInstruction : Instruction
    {
        public override void Execute(VirtualMachine vm)
        {
            vm.OutputData();
        }
    }
}