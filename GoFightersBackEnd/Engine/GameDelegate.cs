using System;

namespace GoFightersBackEnd.Engine
{
    public interface GameDelegate
    {
        /// <summary>
        /// This is used when the opponent hero is attacking.
        /// It passes the damage points from the opponent to the chosen
        /// </summary>
        void OnOpponentAttackEvent(int damage);

        /// <summary>
        /// This is used when the chosen hero is attacking.
        /// It passes the damage points from the chosen to the opponent
        /// </summary>
        void OnFighterAttackEvent(int damage);

        /// <summary>
        /// This is used when the chosen hero has health equals to 0
        /// </summary>
        void OnOpponentWin(int damage);

        /// <summary>
        /// This is used when the opponent hero has health equals to 0
        /// </summary>
        void OnFighterWin(int damage);
    }
}
