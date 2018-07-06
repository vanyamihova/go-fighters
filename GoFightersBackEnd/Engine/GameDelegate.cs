using System;

namespace GoFightersBackEnd.Engine
{
    public interface GameDelegate
    {
        /// <summary>
        /// This is used when some of the heroes is attacking.
        /// It passes the current turn and the damage points.
        /// </summary>
        void OnAttackEvent(bool isOpponent, int damage);

        /// <summary>
        /// This is used when some of the heroes has health equals to 0
        /// </summary>
        void OnWinningEvent(bool isOpponent, int damage);

    }
}
