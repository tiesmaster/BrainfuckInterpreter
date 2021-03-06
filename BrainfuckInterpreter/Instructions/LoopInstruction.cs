﻿using System.Collections.Generic;

namespace BrainfuckInterpreter.Instructions
{
    internal class LoopInstruction : Instruction
    {
        private readonly List<Instruction> _bodyInstructions = new List<Instruction>();

        public void Add(Instruction instruction)
        {
            _bodyInstructions.Add(instruction);
        }

        public override void Execute(VirtualMachine vm)
        {
            while (vm.Data[vm.DataPointer] != 0)
            {
                vm.Execute(_bodyInstructions);
            }
        }

        public void AddRange(IEnumerable<Instruction> instructions)
        {
            _bodyInstructions.AddRange(instructions);
        }
    }
}