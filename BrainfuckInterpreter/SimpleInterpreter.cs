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
                char instruction = (char)program[instructionPointer];
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
                        if(data[dataPointer] == 0)
                        {
                            var unmatchedOpenBraces = 1;
                            do
                            {
                                instructionPointer++;
                                instruction = (char)program[instructionPointer];

                                if (instruction == '[')
                                {
                                    unmatchedOpenBraces++;
                                }

                                if (instruction == ']')
                                {
                                    unmatchedOpenBraces--;
                                }

                            } while (instruction != ']' && unmatchedOpenBraces != 0);
                        }

                        break;
                    case ']':
                        if(data[dataPointer] != 0)
                        {
                            var unmatchedClosingBraces = 1;
                            do
                            {
                                instructionPointer--;
                                instruction = (char)program[instructionPointer];

                                if (instruction == ']')
                                {
                                    unmatchedClosingBraces++;
                                }

                                if (instruction == '[')
                                {
                                    unmatchedClosingBraces--;
                                }

                            } while (instruction != '[' && unmatchedClosingBraces != 0);
                        }

                        break;

                    default:
                        // rest is a no op
                        break;
                }

                instructionPointer++;
            }

        }
    }
}