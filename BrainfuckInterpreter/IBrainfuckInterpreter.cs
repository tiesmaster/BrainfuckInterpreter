namespace BrainfuckInterpreter
{
    public interface IBrainfuckInterpreter
    {
        void ParseAndExecute(byte[] program);
    }
}