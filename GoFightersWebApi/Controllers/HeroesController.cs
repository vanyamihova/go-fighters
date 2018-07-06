using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoFightersBackEnd.Engine;
using GoFightersBackEnd.Models;
using GoFightersWebApi.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GoFightersWebApi.Controllers
{
    [Route("api/[controller]")]
    public class HeroesController : Controller, GameDelegate
    {
        private TaskCompletionSource<BaseDTO> task;
        private GameEngine gameEngine = GameEngine.Get();

        // GET: api/heroes
        [HttpGet]
        public BaseDTO GetAllHeroes()
        {
            return new BaseDTO("OK", gameEngine.GetAllHeroes());
        }

        // GET api/heroes/fighters
        [HttpGet("fighters")]
        public BaseDTO GetChosenHero()
        {
            string status = "OK";

            if (gameEngine.HasWinner())
            {
                status = (gameEngine.IsOpponentAlive()) ? "OPPONENT_IS_WINNER" : "CHOSEN_IS_WINNER";
            }

            return new BaseDTO(status, gameEngine.GetSelectedHeroes());
        }

        // GET api/heroes/attack
        [HttpGet("attack")]
        public BaseDTO AttackedOpponentHero()
        {
            task = new TaskCompletionSource<BaseDTO>();
            gameEngine.Fight(this);
            return task.Task.Result;
        }

        // GET api/heroes/reset
        [HttpGet("reset")]
        public BaseDTO ResetTheFight()
        {
            gameEngine.Reset();
            return new BaseDTO("OK", null);
        }

        // POST api/heroes
        [HttpPost]
        public BaseDTO Post([FromBody]ChooseHerosDTO chooseHeroesDTO)
        {
            // Chooses the first hero if needed
            if (chooseHeroesDTO.ChosenId != null)
            {
                gameEngine.ChooseHero(chooseHeroesDTO.ChosenId);
            }

            // Chooses the opponent hero if needed
            if (chooseHeroesDTO.OpponentId != null)
            {
                gameEngine.ChooseOpponentHero(chooseHeroesDTO.OpponentId);
            }

            // Prepares the fight
            bool ready = gameEngine.PrepareForFight();
            String statusLabel = (ready) ? "OK" : "ERROR";

            // Returns the DTO
            return new BaseDTO(statusLabel, chooseHeroesDTO);
        }


        /////////// INTERFACE IMPLEMENTATION

        public void OnAttackEvent(bool isOpponent, int damage)
        {
            AttackDTO attackDTO = new AttackDTO();
            attackDTO.Damage = damage;
            attackDTO.AttackedId = gameEngine.GetAttackedId();
            attackDTO.HealthPoints = gameEngine.GetAttackedHealthPoints();

            String statusLabel = (isOpponent) ? "CHOSEN_IS_ATTACKED" : "OPPONENT_IS_ATTACKED";

            BaseDTO baseDTO = new BaseDTO(statusLabel, attackDTO);
            task.SetResult(baseDTO);
        }

        public void OnWinningEvent(bool isOpponent, int damage)
        {
            WinnerDTO winnerDTO = new WinnerDTO();
            winnerDTO.WinnerId = gameEngine.GetWinnerId();
            winnerDTO.Damage = damage;

            String statusLabel = (isOpponent) ? "OPPONENT_IS_WINNER" : "CHOSEN_IS_WINNER";

            BaseDTO baseDTO = new BaseDTO(statusLabel, winnerDTO);
            task.SetResult(baseDTO);
        }

    }
}
