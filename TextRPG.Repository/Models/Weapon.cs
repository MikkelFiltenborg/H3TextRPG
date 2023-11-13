using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public class Weapon
    {
        [Key]
        public int Id { get; set; }
        public int WeaponTypeId { get; set; } //fk
        public int WeaponDamageModifier { get; set; }
        public int MinimumSkillRoll { get; set; }
        public int Range { get; set; }
        public bool AvailableToHero { get; set; }
        public bool StarterWeapon { get; set; }
        public int Value { get; set; }
        public WeaponType? WeaponType { get; set; }
    }
}
