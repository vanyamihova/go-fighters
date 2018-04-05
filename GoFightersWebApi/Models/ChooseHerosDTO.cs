using System;
namespace GoFightersWebApi.Models
{
    public class ChooseHerosDTO
    {
        public ChooseHerosDTO()
        {
        }

        public string ChosenId { get; set; }
        public string OpponentId { get; set; }
    }
}
