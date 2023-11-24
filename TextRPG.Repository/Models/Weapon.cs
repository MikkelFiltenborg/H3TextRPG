using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public class Weapon
    {
        [Key]
        public int Id { get; set; }
        public int WeaponTypeId { get; set; } //fk
        public string? WeaponName { get; set; }
        public int WeaponDamageModifier { get; set; }
        public int SkillRoll { get; set; }
        public bool AvailableToHero { get; set; }
        public bool StarterWeapon { get; set; }
        public int Value { get; set; } = 0;
        public string? Note { get; set; } = string.Empty;
        public WeaponType? WeaponType { get; set; }
        [JsonIgnore]
        public List<Inventory>? Inventories { get; set; }
    }
}
