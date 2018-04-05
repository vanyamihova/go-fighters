using System;

namespace GoFightersBackEnd.Utils
{
    /// <summary>
    /// Calculate percentages and changes needed for the logic of the game
    /// </summary>
    class CalculationUtil
    {
			private CalculationUtil() {
				// Private constructor to prevent creating instances from it
			}

            /// <summary>
            /// Calculate a percentage in a range
            /// </summary>
			public static int GetPercentageFromRange(int from, int to) {
				Random rand = new Random();
				return rand.Next(from, to);
			}

            /// <summary>
            /// Calculate a percentage from total in a range and round it
            /// </summary>
			public static int CalculatePercentage(int total, int from, int to) {
				Random rand = new Random();
				int random = rand.Next(from, to);
				return (int) Math.Round((double) (total * (random / 100)));
			} 

            /// <summary>
            /// Round the percentage
            /// </summary>
			public static int GetRoundPercentage(int total, double percentage) {
				return (int) Math.Round((double) (total * (percentage / 100)));
			}

            /// <summary>
            /// Get a random number for a range
            /// </summary>
			public static bool GetChance(int chance) {
				Random rand = new Random();
				int nextRand = rand.Next(0, 10);
				return (chance / 10) == nextRand;
			}

		}
}