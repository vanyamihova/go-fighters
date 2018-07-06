using System;
using GoFightersBackEnd.Engine;

namespace GoFightersBackEnd
{
    public class GameExecutor : GameDelegate
    {
        private GameEngine gameEngine;

        internal GameExecutor()
        {
            gameEngine = new GameEngine();
        }

        internal void Start()
        {
            Console.WriteLine("Selecting chosen hero to be Elf...");
            gameEngine.ChooseHero("assassin");
            Console.WriteLine("Selecting opponent hero to be Monk...");
            gameEngine.ChooseOpponentHero("teenager");

            Console.WriteLine("Preparing for the fight...");
            gameEngine.PrepareForFight();
            Console.WriteLine("Starting the fight...");

            while (!gameEngine.HasWinner())
            {
                gameEngine.Fight(this);
            }
        }

        public void OnAttackEvent(bool isOpponent, int damage)
        {
            String attacker = (isOpponent) ? "opponent" : "chosen";
            Console.WriteLine("The " + attacker + " hero is attacking with " + damage + " damage...");
        }

        public void OnWinningEvent(bool isOpponent, int damage) {
            String winner = (isOpponent) ? "opponent" : "chosen";
            String defeated = (isOpponent) ? "chosen" : "opponent";

            Console.WriteLine();
            Console.WriteLine("The " + winner + " hero is killed with " + damage + " damage...");
            Console.WriteLine("The " + defeated + " hero is winning...");
        }

    }
}
