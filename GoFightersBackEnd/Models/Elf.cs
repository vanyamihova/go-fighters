using System;
using GoFightersBackEnd.Utils;

namespace GoFightersBackEnd.Models
{
    /// <summary>
    /// When defending, has a 10% chance to completely block the attack and receive no
    /// damage.
    /// When attacking, has a 10% chance to do 150% damage.
    /// </summary>
    public class Elf : Hero
    {
        public Elf() : base("elf", 120, 30, 30) { }

        protected override int Defend(int damage)
        {
            bool chance = CalculationUtil.GetChance(10);
            return base.Defend((chance) ? 0 : damage);
        }

        public override int Attack()
        {
            bool chance = CalculationUtil.GetChance(10);
            if (chance)
            {
                return base.Attack(150);
            }
            return base.Attack();
        }
    }
}
