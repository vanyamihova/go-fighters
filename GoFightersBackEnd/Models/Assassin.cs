using System;
using GoFightersBackEnd.Utils;

namespace GoFightersBackEnd.Models
{
    /// <summary>
    /// When attacking, has a 30% chance to do 300% damage.
    /// </summary>
    class Assassin : Hero
    {
			
        public Assassin() : base("assassin", 100, 50, 10) {}

		public override int Attack() {
			bool chance = CalculationUtil.GetChance(30);
			if (chance) {
				return base.Attack(300);
			}
			return base.Attack();
		}

	}
}