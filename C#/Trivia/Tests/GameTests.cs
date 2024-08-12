using System.Linq;
using Trivia;
using Xunit;
using NSubstitute;
using NSubstitute.ReceivedExtensions;

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

    [Fact]
    public void Some()
    {
        // Niklas would like to keep this test because we don't have an agreed dice size limitation
        const string testPlayerName = "TestPlayer 1";
        
        var writer = Substitute.For<IWriter>();
        var game = new Game(writer);
        game.Add(testPlayerName);
        game.Roll(13);
        
        writer.Received().WriteLine($"{testPlayerName}'s new location is 1");
    }
    
    [Fact]
    public void Some2()
    {
        const string testPlayerName = "TestPlayer 1";
        
        var writer = Substitute.For<IWriter>();
        var game = new Game(writer);
        game.Add(testPlayerName);
        game.Roll(6);
        game.Roll(6);
        game.Roll(1);
        
        writer.Received().WriteLine($"{testPlayerName}'s new location is 6");
        writer.Received().WriteLine($"{testPlayerName}'s new location is 0");
        writer.Received().WriteLine($"{testPlayerName}'s new location is 1");
        // TODO: Lets continue here with: ensure that the sequence of calls is right / the last call is "new location is 1".
    }
    
    
}