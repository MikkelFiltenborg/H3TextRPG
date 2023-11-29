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
        public int SkillRollTypeId { get; set; }
        public int EnergyCost { get; set; }
        public int DamageDice { get; set; }
        public int Range { get; set; }

        public SkillRollType? SkillRollType { get; set; }
    }
}
