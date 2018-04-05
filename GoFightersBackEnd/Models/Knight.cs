using System;
using GoFightersBackEnd.Utils;

namespace GoFightersBackEnd.Models
{
    /// <summary>
    /// When defending, has a 20% chance to completely block the attack and receive no
    /// damage.
    /// When attacking, has a 10% chance to do 200% damage.
    /// </summary>
    class Knight : Hero
    {
			
        public Knight() : base("knight", 140, 30, 40) {}

		protected override int Defend(int damage) {
			bool chance = CalculationUtil.GetChance(20);
			return base.Defend((chance) ? 0 : damage);
		}

		public override int Attack() {
			bool chance = CalculationUtil.GetChance(10);
			if (chance) {
				return base.Attack(200);
			}
			return base.Attack();
		}

	}
}