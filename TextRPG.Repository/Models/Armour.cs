using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public class Armour
    {
        [Key]
        public int Id { get; set; }
        public string? ArmourTypeName { get; set; }
        public int ArmourModifier { get; set; }
        public bool AvailableToHero { get; set; }
        public int Value { get; set; }
        public string? Note { get; set; }
    }
}
