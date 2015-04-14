namespace BrainfuckInterpreter
{
    internal enum BrainfuckInstruction
    {
        IncrementDataPointer,
        DecrementDataPointer,
        IncrementData,
        DecrementData,
        OutputData,
        InputData,
        StartLoop,
        EndLoop
    }
}