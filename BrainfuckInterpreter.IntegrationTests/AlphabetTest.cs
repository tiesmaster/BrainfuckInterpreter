using System.IO;
using System.Text;

using FluentAssertions;

using Xunit;

namespace BrainfuckInterpreter.IntegrationTests
{
    public class AlphabetTest
    {
        [Fact]
        public void SimpleAlphabet()
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
            var sut = new InterpreterUsingAst(outputWriter);

            // act
            sut.ParseAndExecute(program);

            // assert
            outputWriter.Flush();
            outputStream.Position = 0;
            Encoding.ASCII.GetString(outputStream.ToArray()).Should().Be("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }

        [Fact]
        public void Alphabet()
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
            var sut = new InterpreterUsingAst(outputWriter);

            // act
            sut.ParseAndExecute(program);

            // assert
            outputWriter.Flush();
            outputStream.Position = 0;
            Encoding.ASCII.GetString(outputStream.ToArray()).Should().Be("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
        }
    }
}