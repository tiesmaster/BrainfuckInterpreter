using System;
using System.IO;
using System.Text;

using FluentAssertions;

using Xunit;

namespace BrainfuckInterpreter.IntegrationTests
{
    public class WhitespaceTests
    {
        [Theory]
        [InlineData(typeof(InterpreterUsingAst))]
        [InlineData(typeof(SimpleInterpreter))]
        public void ProgramData_WithWhitespace_ShouldNotBang(Type interpreterType)
        {
            // arrange
            const string programText = "hoi";

            var program = Encoding.ASCII.GetBytes(programText);

            var outputStream = new MemoryStream();
            var outputWriter = new StreamWriter(outputStream);
            var sut = CreateSut(interpreterType, outputWriter);

            // act
            Action act = () =>
                sut.ParseAndExecute(program);

            // assert
            act.ShouldNotThrow();
        }

        private IBrainfuckInterpreter CreateSut(Type interpreterType, TextWriter outputWriter)
        {
            if (interpreterType == typeof(InterpreterUsingAst))
            {
                return new InterpreterUsingAst(outputWriter);
            }

            if (interpreterType == typeof(SimpleInterpreter))
            {
                return new SimpleInterpreter(outputWriter);
            }

            throw new NotImplementedException();
        }
    }
}