using System;

namespace Trivia
{
    public class GameRunner
    {
        private static bool _notAWinner;

        public static void Main(string[] args)
        {
            var aGame = new Game();

            aGame.Add("Chet");
            aGame.Add("Pat");
            aGame.Add("Sue");

            var rand = InitializeRandom(args);

            do
            {
                aGame.Roll(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    _notAWinner = aGame.WrongAnswer();
                }
                else
                {
                    _notAWinner = aGame.WasCorrectlyAnswered();
                }
            } while (_notAWinner);
        }

        private static Random InitializeRandom(string[] args)
        {
            Random rand;
            if (args.Length > 0)
            {
                var seed = int.Parse(args[0]);
                rand = new Random(seed);
            }
            else
            {
                rand = new Random();
            }

            return rand;
        }
    }
}