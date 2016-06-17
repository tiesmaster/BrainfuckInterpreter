using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using BrainfuckInterpreter.Instructions;

namespace BrainfuckInterpreter
{
    /// <summary>
    /// This is a brainfuck interpreter, that first compiles to an AST, and then executes that (using the
    /// <see cref="VirtualMachine"/> class.
    /// </summary>
    public class InterpreterUsingAst : IBrainfuckInterpreter
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

        public InterpreterUsingAst(TextWriter outputWriter)
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
            var cursor = brainfuckInstructions.GetEnumerator();

            var instructions = Parse(cursor);

            cursor.Dispose();
            return instructions;
        }

        private IEnumerable<Instruction> Parse(IEnumerator<BrainfuckInstruction> cursor)
        {
            while (cursor.MoveNext())
            {
                var currentBrainfuckInstruction = cursor.Current;

                if (!_loopInstructions.Contains(currentBrainfuckInstruction))
                {
                    yield return CreateInstruction(currentBrainfuckInstruction);
                }
                else if (currentBrainfuckInstruction == BrainfuckInstruction.StartLoop)
                {
                    var loopInstruction = new LoopInstruction();
                    loopInstruction.AddRange(Parse(cursor));
                    yield return loopInstruction;
                }
            }
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