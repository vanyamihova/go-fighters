using System;
using GoFightersBackEnd.Utils;

namespace GoFightersBackEnd.Models
{
    /// <summary>
    /// When attacking, has a 20% chance to do 200% damage.
    /// </summary>
    public class Teenager : Hero
    {
        public Teenager() : base("teenager", 150, 40, 15) { }

        public override int Attack()
        {
            bool chance = CalculationUtil.GetChance(20);
            if (chance)
            {
                return base.Attack(200);
            }
            return base.Attack();
        }
    }
}
