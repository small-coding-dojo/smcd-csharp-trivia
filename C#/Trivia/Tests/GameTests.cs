using Trivia;
using Xunit;
using NSubstitute;

namespace Tests;

public class GameTests
{
    [Fact]
    public void SomeTest()
    {
        var writer = Substitute.For<IWriter>();
        
        var game = new Game(writer);
        game.Add("TestPlayer 1");
        game.Roll(1);
        
        writer.Received().WriteLine("TestPlayer 1 is the current player");
    }
}