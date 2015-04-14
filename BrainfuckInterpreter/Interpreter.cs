using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using BrainfuckInterpreter.Instructions;

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

        private readonly BrainfuckInstruction[] _loopInstructions =
        {
            BrainfuckInstruction.StartLoop,
            BrainfuckInstruction.EndLoop
        };

        public Interpreter(TextWriter outputWriter)
        {
            _outputWriter = outputWriter;
        }

        public void ParseAndExecute(byte[] program)
        {
            var brainfuckInstructions = TokenizeProgram(program);
            var tree = Parse(brainfuckInstructions);
            Execute(tree);
        }

        private IEnumerable<BrainfuckInstruction> TokenizeProgram(IEnumerable<byte> program)
        {
            return program.Select(Tokenize).OfType<BrainfuckInstruction>();
        }

        private BrainfuckInstruction? Tokenize(byte programElement)
        {
            BrainfuckInstruction token;
            var validInstruction = _programElementToBrainfuckInstructionMapping.TryGetValue((char) programElement, out token);
            return validInstruction ? token : (BrainfuckInstruction?) null;
        }

        // ReSharper disable once PossibleNullReferenceException

        private IEnumerable<Instruction> Parse(IEnumerable<BrainfuckInstruction> brainfuckInstructions)
        {
            var instructions = new List<Instruction>();
            var loopInstruction = default(LoopInstruction);
            foreach (var brainfuckInstruction in brainfuckInstructions)
            {
                if (!_loopInstructions.Contains(brainfuckInstruction) && loopInstruction == null)
                {
                    instructions.Add(CreateInstruction(brainfuckInstruction));
                }
                else
                {
                    if (brainfuckInstruction == BrainfuckInstruction.StartLoop)
                    {
                        loopInstruction = new LoopInstruction();
                    }
                    else if (brainfuckInstruction == BrainfuckInstruction.EndLoop)
                    {
                        instructions.Add(loopInstruction);
                        loopInstruction = null;
                    }
                    else
                    {
                        loopInstruction.Add(CreateInstruction(brainfuckInstruction));
                    }
                }
            }
            return instructions;
        }

        private Instruction CreateInstruction(BrainfuckInstruction brainfuckInstruction)
        {
            switch (brainfuckInstruction)
            {
                case BrainfuckInstruction.IncrementDataPointer:
                    return new IncrementDataPointerInstruction();
                case BrainfuckInstruction.DecrementDataPointer:
                    return new DecrementDataPointerInstruction();
                case BrainfuckInstruction.IncrementData:
                    return new IncrementDataInstruction();
                case BrainfuckInstruction.DecrementData:
                    return new DecrementDataInstruction();
                case BrainfuckInstruction.OutputData:
                    return new OutputDataInstruction();
                case BrainfuckInstruction.InputData:
                    return new InputDataInstruction();
            }
            throw new NotSupportedException();
        }

        private void Execute(IEnumerable<Instruction> syntaxTree)
        {
            var vm = new VirtualMachine(_outputWriter);
            vm.Execute(syntaxTree);
        }
    }
}