using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public class Inventory
    {
        [Key]
        public int Id { get; set; }
        public int Gold { get; set; }
        //public int ArmourId { get; set; } //fk

        public List<Weapon>? Weapons { get; set; }
        public Armour? Armour { get; set; }
        public List<Potion>? Potions { get; set; }
    }
}
