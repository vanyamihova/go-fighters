using System;
namespace GoFightersWebApi.Models
{
    public class AttackDTO
    {
        public int Damage { get; set; }
        public int HealthPoints { get; set; }
        public string AttackedId { get; set; }
    }
}
