using System;
using GoFightersBackEnd.Models.Helpers;
using GoFightersBackEnd.Utils;

namespace GoFightersBackEnd.Models
{
    /// <summary>
    /// When attacking, all heroes do randomly between 80% and 120% of their attack points as raw damage.
    /// 
    /// When defending, all heroes take damage, reduced randomly with between 80% and 120% of their armor
    /// points.The actual damage received reduces their health points.When the health points become zero or
    /// less, the hero is dead.
    /// </summary>
    public class Hero
    {
        // Hero's id
        private string id;
        // Max health points
		private int health;
        // Max attack points
		private int attack;
        // Max armor points
        private int armor;
        // Instance for current health points
        private CurrentHealth currentHealth;

		public Hero(string id, int health, int attack, int armor) {
            this.id = id;
			this.health = health;
			this.attack = attack;
			this.armor = armor;
            this.currentHealth = new CurrentHealth(health);
		}

        public string Id
        {
            get { return this.id; }
        }

        public int HealthPoints
        {
            get { return this.health; }
        }

        public int CurrentHealthPoints
        {
            get { return this.currentHealth.Points; }
        }

        public int AttackPoints
        {
            get { return this.attack; }
        }

        public int ArmorPoints
        {
            get { return this.armor; }
        }

        public bool IsAlive() {
            return !this.currentHealth.IsZero();
        }

		protected virtual int Defend(int damage) {
			int calculatedArmor = CalculationUtil.CalculatePercentage(this.armor, 80, 120);
			int calculatedDamage = damage - calculatedArmor;
			return (calculatedDamage < 0) ? 0 : calculatedDamage;
		}

		public virtual int Attack() {
			int random = CalculationUtil.GetPercentageFromRange(80, 120);
			return this.Attack(random);
		}

		public virtual int Attack(double percent) {
			return CalculationUtil.GetRoundPercentage(this.attack, percent);
		}

		public virtual int Attacked(int damage) {
            int calculatedDamage = this.Defend(damage);
            this.currentHealth.Reduce(calculatedDamage);
            return calculatedDamage;
		}

    }
}
