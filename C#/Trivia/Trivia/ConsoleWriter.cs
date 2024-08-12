using System;

namespace Trivia;

public class ConsoleWriter : IWriter
{
    public void WriteLine(string input)
    {
        Console.WriteLine(input);
    }
}