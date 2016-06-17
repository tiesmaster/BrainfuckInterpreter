using System;
using System.IO;

namespace BrainfuckInterpreter
{
    public class SimpleInterpreter : IBrainfuckInterpreter
    {
        private readonly TextWriter _outputWriter;

        public SimpleInterpreter(TextWriter outputWriter)
        {
            _outputWriter = outputWriter;
        }

        public void ParseAndExecute(byte[] program)
        {
            var instructionPointer = 0;
            var dataPointer = 0;
            var data = new byte[30000];

            var programLength = program.Length;

            while(instructionPointer < programLength)
            {
                char instruction = '0'; //program[instructionPointer];
                switch (instruction)
                {
                    case '<':
                        dataPointer--;
                        break;
                    case '>':
                        dataPointer++;
                        break;
                    case '-':
                        data[dataPointer]--;
                        break;
                    case '+':
                        data[dataPointer]++;
                        break;
                    case ',':
                        data[dataPointer] = (byte)Console.Read();
                        break;
                    case '.':
                        _outputWriter.Write((char)data[dataPointer]);
                        break;
                    case '[':
                        // TODO: jump forward, if data[dataPointer] == 0
                        break;
                    case ']':
                        // TODO: jump backwards, if data[dataPointer] != 0
                        break;

                        // rest is a no op
                }
                instructionPointer++;
            }

        }
    }
}