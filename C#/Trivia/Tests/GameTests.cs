using Trivia;
using Xunit;
using NSubstitute;

namespace Tests;

public class GameTests
{
    [Fact]
    public void Given_Fresh_Player_When_Rolling_Die_Then_Roll_Is_Added_To_Location()
    {
        const string testPlayerName = "TestPlayer 1";
        
        var writer = Substitute.For<IWriter>();
        var game = new Game(writer);
        game.Add(testPlayerName);
        game.Roll(1);
        
        writer.Received().WriteLine($"{testPlayerName}'s new location is 1");
    }
    
    
}