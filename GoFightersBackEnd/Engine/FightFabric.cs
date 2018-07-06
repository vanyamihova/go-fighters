using System;
using GoFightersBackEnd.Models;
using GoFightersBackEnd.Utils;

namespace GoFightersBackEnd.Engine
{
    /// <summary>
    /// This class remembers which are selected heroes and whose turn is. It works
    /// with a delegate on which are sent the events.
    /// </summary>
    public class FightFabric
    {
        // Whose turn is
        private bool isOpponentTurn;
        // The opponent
        private Hero opponentHero;
        // The users chosen hero
        private Hero chosenHero;

        internal FightFabric(Hero chosenHero, Hero opponentHero)
        {
            this.isOpponentTurn = CalculationUtil.GetChance(2);
            this.opponentHero = opponentHero;
            this.chosenHero = chosenHero;
        }
        
        internal void Fight(GameDelegate gameDelegate)
        {
            // Calculate how many are the damage points
            int damagePoints = this.AttackFromHeros(gameDelegate);
            // Check if there is a killed hero and define the winner
            this.DefineWinner(gameDelegate, damagePoints);
        }

        // Returns the hero whose attack
        internal Hero GetAttacked()
        {
            return (isOpponentTurn) ? chosenHero : opponentHero;
        }

        private int AttackFromHeros(GameDelegate gameDelegate)
        {
            // Only alive heroes can attack and can be attacked
            if (!chosenHero.IsAlive() || !opponentHero.IsAlive())
            {
                // If somebody is killed, we don't need the damage points
                return 0;
            }

            // Switches the turn
            isOpponentTurn = !isOpponentTurn;

            int damagePoints = 0;
            if (isOpponentTurn)
            {
                // Calculates the damage points and get them from the health of the chosen one
                damagePoints = Attack(opponentHero, chosenHero);
            } else {
                // Calculates the damage points and get them from the health of the opponent
                damagePoints = Attack(chosenHero, opponentHero);
            }

            // If the heroes are still alive, sends a message to the delegate
            if (!(chosenHero.IsAlive() ^ opponentHero.IsAlive()))
            {
                gameDelegate.OnAttackEvent(isOpponentTurn, damagePoints);
            }

            // Returns the damage points;
            return damagePoints;
        }

        private void DefineWinner(GameDelegate gameDelegate, int damagePoints)
        {
            // Checks if there is a winner
            if (chosenHero.IsAlive() && opponentHero.IsAlive())
            {
                return;
            }
            // If there is someone defeated, sends a message to the delegate
            gameDelegate.OnWinningEvent(isOpponentTurn, damagePoints);
        }

        private int Attack(Hero attackHero, Hero attackedHero)
        {
            // Calculates the attacking points
            int points = attackHero.Attack();
            // Reduces the health of the attacked user
            return attackedHero.Defend(points);
        }

    }
}
