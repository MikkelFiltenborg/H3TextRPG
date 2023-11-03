using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public class Potion
    {
        [Key]
        public int Id { get; set; }
        public int PotionTypeId { get; set; } //fk
        public int Amount { get; set; }
        public bool AvailableToHero { get; set; }
        public int Value { get; set; }
    }
}
