using System;
using System.Collections.Generic;
using System.IO;

namespace BrainfuckInterpreter
{
    public class Interpreter
    {
        private readonly TextWriter _outputWriter;

        private readonly Dictionary<char, BrainfuckInstruction> _programElementToBrainfuckInstructionMapping =
            new Dictionary<char, BrainfuckInstruction>
            {
                {'>', BrainfuckInstruction.IncrementDataPointer},
                {'<', BrainfuckInstruction.DecrementDataPointer},
                {'+', BrainfuckInstruction.IncrementData},
                {'-', BrainfuckInstruction.DecrementData},
                {',', BrainfuckInstruction.InputData},
                {'.', BrainfuckInstruction.OutputData},
                {'[', BrainfuckInstruction.StartLoop},
                {']', BrainfuckInstruction.EndLoop}
            };

        private readonly byte[] _data = new byte[30000];
        private int _dataPointer;

        public Interpreter(TextWriter outputWriter)
        {
            _outputWriter = outputWriter;
        }

        public void ParseAndExecute(byte[] program)
        {
            foreach (var programElement in program)
            {
                var instruction = Tokenize(programElement);
                if (instruction.HasValue)
                {
                    Execute(instruction.Value);
                }
            }
        }

        private BrainfuckInstruction? Tokenize(byte programElement)
        {
            BrainfuckInstruction token;
            var validInstruction = _programElementToBrainfuckInstructionMapping.TryGetValue((char) programElement, out token);
            return validInstruction ? token : (BrainfuckInstruction?) null;
        }

        private void Execute(BrainfuckInstruction instruction)
        {
            switch (instruction)
            {
                case BrainfuckInstruction.IncrementDataPointer:
                    _dataPointer++;
                    break;
                case BrainfuckInstruction.DecrementDataPointer:
                    _dataPointer--;
                    break;
                case BrainfuckInstruction.IncrementData:
                    _data[_dataPointer]++;
                    break;
                case BrainfuckInstruction.DecrementData:
                    _data[_dataPointer]--;
                    break;
                case BrainfuckInstruction.OutputData:
                    _outputWriter.Write((char)_data[_dataPointer]);
                    break;
                case BrainfuckInstruction.InputData:
                    _data[_dataPointer] = (byte) Console.Read();
                    break;
                case BrainfuckInstruction.StartLoop:
                    throw new NotImplementedException();
                case BrainfuckInstruction.EndLoop:
                    throw new NotImplementedException();
            }
        }
    }
}