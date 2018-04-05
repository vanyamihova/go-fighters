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
            gameEngine.ChooseHero("elf");
            Console.WriteLine("Selecting opponent hero to be Monk...");
            gameEngine.ChooseOpponentHero("monk");

            Console.WriteLine("Preparing for the fight...");
            gameEngine.PrepareForFight();
            Console.WriteLine("Starting the fight...");

            gameEngine.Fight(this);
        }

        public void OnFighterAttackEvent(int damage)
        {
            Console.WriteLine("The chosen hero is attiking with " + damage + " damage...");
            gameEngine.Fight(this);
        }

        public void OnFighterWin(int damage)
        {
            Console.WriteLine("The opponent hero is killed with " + damage + " damage...");
            Console.WriteLine("The chosen hero is winning...");
        }

        public void OnOpponentAttackEvent(int damage)
        {
            Console.WriteLine("The opponent hero is attiking with " + damage + " damage...");
            gameEngine.Fight(this);
        }

        public void OnOpponentWin(int damage)
        {
            Console.WriteLine("The chosen hero is killed with " + damage + " damage...");
            Console.WriteLine("The opponent hero is winning...");
        }
    }
}
