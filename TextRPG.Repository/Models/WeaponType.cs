using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public class WeaponType
    {
        [Key]
        public int Id { get; set; }
        public string? WeaponTypeName { get; set; }
        public string? SkillRollTypeId { get; set; } //fk
        public int EnergyCost { get; set; }
        public int DamageDice { get; set; }
        //TODO: Does it need SkillRollType, as a property?
        public SkillRollType? SkillRollType { get; set; }
    }
}
