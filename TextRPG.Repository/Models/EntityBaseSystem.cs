using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public class EntityBaseSystem
    {
        [Key]
        public int Id { get; set; }
        public int Stength { get; set; }
        public int Agility { get; set; }
        public int Vigor { get; set; }
        public int Spirit { get; set; }
        public int Health { get; set; }
        public int Energy { get; set; }
        public int HealthModifier { get; set; }
        public int EnergyModifier { get; set; }
        public int DamagerModifier { get; set; }
        public int ArmourModifier { get; set; }

        //public int? HeroId { get; set; }
    }
}
