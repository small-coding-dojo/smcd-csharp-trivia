using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    public class Game
    {
        private readonly List<string> _players = new List<string>();

        // TODO smell: player properties are distributed into separate arrays with fixed length
        private readonly int[] _places = new int[6]; // TODO smell: Magic number 6 and mysterious name
        private readonly int[] _purses = new int[6]; // TODO smell: Magic number 6 and mysterious name

        private readonly bool[] _inPenaltyBox = new bool[6]; // TODO smell: Magic number 6 and mysterious name

        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportsQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();

        private int _currentPlayer; // TODO smell: Single responsibility is violated - current player and questions are in one clas
        private bool _isGettingOutOfPenaltyBox;
        private readonly IWriter _consoleWriter;

        public Game() : this(new ConsoleWriter())
        {
        }

        private Game(IWriter writer)
        {
            _consoleWriter = writer;
            for (var i = 0; i < 50; i++) // TODO smell: Magic number 50
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportsQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast(CreateRockQuestion(i)); // TODO smell: inconsistent level of abstraction
            }
        }

        public string CreateRockQuestion(int index) // TODO smell: hide implementation details
        {
            return "Rock Question " + index;
        }

        // TODO smell: Unused method IsPlayable
        public bool IsPlayable()
        {
            return (HowManyPlayers() >= 2);
        }

        public bool Add(string playerName) // TODO smell: unused return value; method throws exception if too many players
        {
            _players.Add(playerName);
            _places[HowManyPlayers()] = 0;
            _purses[HowManyPlayers()] = 0;
            _inPenaltyBox[HowManyPlayers()] = false;

            // TODO smell: output is coupled to static ressources (Console)
            _consoleWriter.WriteLine(playerName + " was added");
            _consoleWriter.WriteLine("They are player number " + _players.Count);
            
            // TODO smell: function always returns true
            return true;
        }

        public int HowManyPlayers() // TODO smell: hide implementation details
        {
            return _players.Count;
        }

        public void Roll(int roll) // TODO smell: long method; complex method
        {
            _consoleWriter.WriteLine(_players[_currentPlayer] + " is the current player");
            _consoleWriter.WriteLine("They have rolled a " + roll);

            if (_inPenaltyBox[_currentPlayer])
            {
                // TODO smell: different levels of abstraction - penalty box handling, moving the player, asking questions
                // TODO smell: SRP violated - penalty box handling, moving the player, asking questions
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true; // TODO smell: penaltyBox state handling implemented as class member; name is mysterious OR bug - player does not get out of penalty box, they just may provide an answer

                    _consoleWriter.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");
                    // TODO smell: duplicate code (new location of player, ask question)
                    _places[_currentPlayer] = _places[_currentPlayer] + roll;
                    if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                    _consoleWriter.WriteLine(_players[_currentPlayer]
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    _consoleWriter.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    _consoleWriter.WriteLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                // TODO smell: duplicate code (new location of player, ask question)
                _places[_currentPlayer] = _places[_currentPlayer] + roll;
                if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                _consoleWriter.WriteLine(_players[_currentPlayer]
                        + "'s new location is "
                        + _places[_currentPlayer]);
                _consoleWriter.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        private void AskQuestion()
        {
            // TODO smell: redundant code structure
            // TODO smell: fixed number of categories; category names are hard coded; categories cannot be extended easily
            if (CurrentCategory() == "Pop")
            {
                // TODO smell: linked list is enumerated without necessity (use property First instead)
                // TODO smell: List of questions can be empty - missing error handling
                _consoleWriter.WriteLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                _consoleWriter.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                _consoleWriter.WriteLine(_sportsQuestions.First());
                _sportsQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                _consoleWriter.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }

        private string CurrentCategory()
        {
            // TODO smell: magic numbers; magic strings
            if (_places[_currentPlayer] == 0) return "Pop";
            if (_places[_currentPlayer] == 4) return "Pop";
            if (_places[_currentPlayer] == 8) return "Pop";
            if (_places[_currentPlayer] == 1) return "Science";
            if (_places[_currentPlayer] == 5) return "Science";
            if (_places[_currentPlayer] == 9) return "Science";
            if (_places[_currentPlayer] == 2) return "Sports";
            if (_places[_currentPlayer] == 6) return "Sports";
            if (_places[_currentPlayer] == 10) return "Sports";
            return "Rock";
        }

        public bool WasCorrectlyAnswered()
        {
            if (_inPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    _consoleWriter.WriteLine("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    _consoleWriter.WriteLine(_players[_currentPlayer]
                            + " now has "
                            + _purses[_currentPlayer]
                            + " Gold Coins.");

                    var winner = DidPlayerWin();
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;

                    return winner;
                }
                else
                {
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;
                    return true;
                }
            }
            else
            {
                _consoleWriter.WriteLine("Answer was corrent!!!!");
                _purses[_currentPlayer]++;
                _consoleWriter.WriteLine(_players[_currentPlayer]
                        + " now has "
                        + _purses[_currentPlayer]
                        + " Gold Coins.");

                var winner = DidPlayerWin();
                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;

                return winner;
            }
        }

        public bool WrongAnswer()
        {
            _consoleWriter.WriteLine("Question was incorrectly answered");
            _consoleWriter.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _inPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            return true;
        }


        private bool DidPlayerWin()
        {
            return !(_purses[_currentPlayer] == 6);
        }
    }

}
