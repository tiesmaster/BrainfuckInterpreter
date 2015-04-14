using System;
using System.Collections.Generic;
using System.IO;

using BrainfuckInterpreter.Instructions;

namespace BrainfuckInterpreter
{
    internal class VirtualMachine
    {
        private readonly TextWriter _outputWriter;
        private readonly byte[] _data = new byte[30000];

        public VirtualMachine(TextWriter outputWriter)
        {
            _outputWriter = outputWriter;
        }

        public byte[] Data
        {
            get { return _data; }
        }

        public int DataPointer { get; set; }

        public void Execute(IEnumerable<Instruction> syntaxTree)
        {
            foreach (var instruction in syntaxTree)
            {
                Execute(instruction);
            }
        }

        private void Execute(Instruction instruction)
        {
            instruction.Execute(this);
        }

        public void IncrementData()
        {
            Data[DataPointer]++;
        }

        public void DecrementData()
        {
            Data[DataPointer]--;
        }

        public void InputData()
        {
            Data[DataPointer] = (byte) Console.Read();
        }

        public void OutputData()
        {
            _outputWriter.Write((char)Data[DataPointer]);
        }
    }
}