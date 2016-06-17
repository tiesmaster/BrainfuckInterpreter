using System;
using System.IO;
using System.Text;

using FluentAssertions;

using Xunit;

namespace BrainfuckInterpreter.IntegrationTests
{
    public class WhitespaceTests
    {
        [Fact]
        public void ProgramData_WithWhitespace_ShouldNotBang()
        {
            // arrange
            const string programText = "hoi";

            var program = Encoding.ASCII.GetBytes(programText);

            var outputStream = new MemoryStream();
            var outputWriter = new StreamWriter(outputStream);
            var sut = new InterpreterUsingAst(outputWriter);

            // act
            Action act = () =>
                sut.ParseAndExecute(program);

            // assert
            act.ShouldNotThrow();
        }
    }
}