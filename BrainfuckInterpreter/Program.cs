﻿using System;
using System.IO;
using System.Linq;

namespace BrainfuckInterpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            var programFile = args.First();
            var program = File.ReadAllBytes(programFile);

            new InterpreterUsingAst(Console.Out).ParseAndExecute(program);
        }
    }
}