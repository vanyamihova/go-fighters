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

            if (gameEngine.GetChosenHero() != null && !gameEngine.GetChosenHero().IsAlive()) {
                status = "OPPONENT_IS_WINNER";
            }

            if (gameEngine.GetOpponentHero() != null && !gameEngine.GetOpponentHero().IsAlive())
            {
                status = "CHOSEN_IS_WINNER";
            }

            return new BaseDTO(status, new object[] {
                gameEngine.GetChosenHero(),
                gameEngine.GetOpponentHero()
            });
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
        public BaseDTO Post([FromBody]ChooseHerosDTO chooseHerosDTO)
        {
            //
            if (chooseHerosDTO.ChosenId != null)
            {
                gameEngine.ChooseHero(chooseHerosDTO.ChosenId);
            }

            //
            if (chooseHerosDTO.OpponentId != null)
            {
                gameEngine.ChooseOpponentHero(chooseHerosDTO.OpponentId);
            }

            //
            bool ready = gameEngine.PrepareForFight();
            if (ready) {
                return new BaseDTO("OK", chooseHerosDTO);
            }
            return new BaseDTO("ERROR", chooseHerosDTO);
        }


        /////////// INTERFACE IMPLEMENTATION

        public void OnOpponentAttackEvent(int damage)
        {
            AttackDTO attackDTO = new AttackDTO();
            attackDTO.Damage = damage;
            attackDTO.AttackedId = gameEngine.GetChosenHero().Id;
            attackDTO.HealthPoints = gameEngine.GetChosenHero().CurrentHealthPoints;

            BaseDTO baseDTO = new BaseDTO("CHOSEN_IS_ATTACKED", attackDTO);
            task.SetResult(baseDTO);
        }

        public void OnFighterAttackEvent(int damage)
        {
            AttackDTO attackDTO = new AttackDTO();
            attackDTO.Damage = damage;
            attackDTO.AttackedId = gameEngine.GetOpponentHero().Id;
            attackDTO.HealthPoints = gameEngine.GetOpponentHero().CurrentHealthPoints;

            BaseDTO baseDTO = new BaseDTO("OPPONENT_IS_ATTACKED", attackDTO);
            task.SetResult(baseDTO);
        }

        public void OnOpponentWin(int damage)
        {
            WinnerDTO winnerDTO = new WinnerDTO();
            winnerDTO.WinnerId = gameEngine.GetOpponentHero().Id;
            winnerDTO.Damage = damage;

            BaseDTO baseDTO = new BaseDTO("OPPONENT_IS_WINNER", winnerDTO);
            task.SetResult(baseDTO);
        }

        public void OnFighterWin(int damage)
        {
            WinnerDTO winnerDTO = new WinnerDTO();
            winnerDTO.WinnerId = gameEngine.GetChosenHero().Id;
            winnerDTO.Damage = damage;

            BaseDTO baseDTO = new BaseDTO("CHOSEN_IS_WINNER", winnerDTO);
            task.SetResult(baseDTO);
        }

    }
}
