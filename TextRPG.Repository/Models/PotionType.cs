using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public class PotionType
    {
        [Key]
        public int Id { get; set; }
        public string? PotionTypeName { get; set; }
        public int PotionDice { get; set; }
        public bool AvailableToHero { get; set; }
        public int Value { get; set; }
    }
}
