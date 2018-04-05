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
            isOpponentTurn = CalculationUtil.GetChance(2);
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

        private int AttackFromHeros(GameDelegate gameDelegate)
        {
            // Only alive heroes can attack and can be attacked
            if (chosenHero.IsAlive() && opponentHero.IsAlive())
            {
                // Switch the turn
                isOpponentTurn = !isOpponentTurn;
                // If it's opponent turn
                if (isOpponentTurn)
                {
                    // Calculate the damage points and get them from the health of the chosen one
                    int damagePointsFromOpponent = Attack(opponentHero, chosenHero);
                    // If the chosen hero is still alive, send a message to the delegate
                    if (chosenHero.IsAlive())
                    {
                        gameDelegate.OnOpponentAttackEvent(damagePointsFromOpponent);
                    }
                    // Return the damage points;
                    return damagePointsFromOpponent;
                }

                // Calculate the damage points and get them from the health of the opponent
                int damagePointsFromChosen = Attack(chosenHero, opponentHero);
                // If the opponent hero is still alive, send a message to the delegate
                if (opponentHero.IsAlive())
                {
                    gameDelegate.OnFighterAttackEvent(damagePointsFromChosen);
                }
                // Return the damage points;
                return damagePointsFromChosen;
            }
            // If somebody is killed, we don't need the damage points
            return 0;
        }

        private void DefineWinner(GameDelegate gameDelegate, int damagePoints)
        {
            // If it's opponent turn and the chosen hero is killed
            // then the opponent is the winner -> send a message to the delegate
            if (isOpponentTurn && !chosenHero.IsAlive())
            {
                gameDelegate.OnOpponentWin(damagePoints);
                return;
            }

            // If it's chosen turn and the opponent hero is killed
            // then the chosen is the winner -> send a message to the delegate
            if (!isOpponentTurn && !opponentHero.IsAlive()) {
                gameDelegate.OnFighterWin(damagePoints);
            }
        }

        private int Attack(Hero attackHero, Hero attackedHero)
        {
            // Calculate the attacking points
            int points = attackHero.Attack();
            // Reduce the health of the attacked user
            return attackedHero.Attacked(points);
        }
    }
}
