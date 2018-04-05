using System;
namespace GoFightersBackEnd.Models.Helpers
{
    /// <summary>
    /// Take care for the current health points
    /// </summary>
    public class CurrentHealth
    {
        private int points;

        public CurrentHealth(int maxHealth)
        {
            this.points = maxHealth;
        }

        public int Points
        {
            get { return this.points; }
        }


        public bool IsZero()
        {
            return this.points == 0;
        }

        public int Reduce(int damage)
        {
            int calculatedPoints = this.points - damage;
            this.points = (calculatedPoints < 0) ? 0 : calculatedPoints;
            return this.points;
        }
    }
}
