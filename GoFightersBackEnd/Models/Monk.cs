using System;
using GoFightersBackEnd.Utils;

namespace GoFightersBackEnd.Models
{
    /// <summary>
    /// When defending, has a 30% chance to avoid the attack and receive no damage.
    /// </summary>
    class Monk : Hero
    {
			
        public Monk() : base("monk", 80, 10, 60) {}

		protected override int Defend(int damage) {
			bool chance = CalculationUtil.GetChance(30);
			return base.Defend((chance) ? 0 : damage);
		}

	}
}