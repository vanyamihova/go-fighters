using System;
using GoFightersBackEnd.Models;

namespace GoFightersBackEnd.Engine
{
    public class GameEngine
    {
        // Keep the instance
        private static GameEngine Instance = new GameEngine();

        public static GameEngine Get()
        {
            return Instance;
        }

        private Hero opponentHero;
        private Hero chosenHero;
        private FightFabric fightFabric;

        public void ChooseHero(string heroName)
        {   
            // Get the correct instance of the hero
            HeroType heroType = (HeroType) System.Enum.Parse(typeof(HeroType), heroName.ToUpper());
            chosenHero = HeroFabric.Generate(heroType);
        }

        public void ChooseOpponentHero(string heroName)
        {
            // Get the correct instance of the hero
            HeroType heroType = (HeroType)System.Enum.Parse(typeof(HeroType), heroName.ToUpper());
            opponentHero = HeroFabric.Generate(heroType);
        }

        public bool PrepareForFight()
        {
            // Create an instance for the fightFabric.
            fightFabric = new FightFabric(chosenHero, opponentHero);
            // Check if every hero is selected
            return chosenHero != null && opponentHero != null;
        }

        // Pass the delegate to the fightFabric
        public void Fight(GameDelegate gameDelegate)
        {
            fightFabric.Fight(gameDelegate);
        }

        // Get all possible heroes in the game
        public Hero[] GetAllHeroes() {
            return new Hero[] {
                new Assassin(),
                new Elf(),
                new Knight(),
                new Monk(),
                new Teenager(),
                new Warrior()
            };
        }

        // Reset the fight
        public void Reset() {
            chosenHero = null;
            opponentHero = null;
            fightFabric = null;
        }

        // Get the chosen hero
        public Hero GetChosenHero()
        {
            return chosenHero;
        }

        // Get the opponent hero
        public Hero GetOpponentHero()
        {
            return opponentHero;
        }

    }
}
