using System;
using GoFightersBackEnd.Models;

namespace GoFightersBackEnd.Engine
{
    /// <summary>
    /// It returns the correct instance of the Hero by it's type
    /// </summary>
    public class HeroFabric
    {
        
		internal static Hero Generate(HeroType heroType) {
			switch (heroType)
            {
                case HeroType.ASSASSIN:
                    return new Assassin();
                case HeroType.ELF:
                    return new Elf();
                case HeroType.MONK:
                    return new Monk();
                case HeroType.WARRIOR:
                    return new Warrior();
                case HeroType.TEENAGER:
                    return new Teenager();
                case HeroType.KNIGHT:
                    return new Knight();
            }
            throw new NullReferenceException("The hero type (${heroType}) is not supported");
		}

    }
}
