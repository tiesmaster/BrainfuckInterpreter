using System;
using System.IO;
using System.Text;

using FluentAssertions;

using Xunit;

namespace BrainfuckInterpreter.IntegrationTests
{
    public class AlphabetTest
    {
        [Theory]
        [InlineData(typeof(InterpreterUsingAst))]
        [InlineData(typeof(SimpleInterpreter))]
        public void SimpleAlphabet(Type interpreterType)
        {
            // arrange
            const string programText =
                // cell 0 = 65
                "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++" +
                // output cell 0, and increment (26 times)
                ".+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+.+";

            var program = Encoding.ASCII.GetBytes(programText);

            var outputStream = new MemoryStream();
            var outputWriter = new StreamWriter(outputStream);
            var sut = CreateSut(interpreterType, outputWriter);

            // act
            sut.ParseAndExecute(program);

            // assert
            outputWriter.Flush();
            outputStream.Position = 0;
            Encoding.ASCII.GetString(outputStream.ToArray()).Should().Be("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        [Theory]
        [InlineData(typeof(InterpreterUsingAst))]
        [InlineData(typeof(SimpleInterpreter))]
        public void Alphabet(Type interpreterType)
        {
            // arrange
            const string programText =
                // cell 0 = 26
                "++++++++++++++++++++++++++" +
                // cell 1 = 65
                ">+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++<" +
                // while cell#0 != 0
                "[" +
                // move to cell 1, output it, and increment it
                 ">.+" +
                 // move back to cell 0, and decrement it
                 "<-" +
                 // end loop (actually jump back to start of loop)
                "]";

            var program = Encoding.ASCII.GetBytes(programText);

            var outputStream = new MemoryStream();
            var outputWriter = new StreamWriter(outputStream);
            var sut = CreateSut(interpreterType, outputWriter);

            // act
            sut.ParseAndExecute(program);

            // assert
            outputWriter.Flush();
            outputStream.Position = 0;
            Encoding.ASCII.GetString(outputStream.ToArray()).Should().Be("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        private IBrainfuckInterpreter CreateSut(Type interpreterType, TextWriter outputWriter)
        {
            if(interpreterType == typeof(InterpreterUsingAst))
            {
                return new InterpreterUsingAst(outputWriter);
            }

            if(interpreterType == typeof(SimpleInterpreter))
            {
                return new SimpleInterpreter(outputWriter);
            }

            throw new NotImplementedException();
        }
    }
}