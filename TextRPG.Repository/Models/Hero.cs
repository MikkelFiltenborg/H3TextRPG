using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public class Hero
    {
        [Key]
        public int Id { get; set; }
        public int EntityBaseSystemID { get; set; } //fk
        public int InventoryId { get; set; } //fk
        public string? HeroName { get; set; }
        public int HeroXp { get; set; }
        public int Level { get; set; }
        public int RaceTypeId { get; set; }
        public int CareerTypeId { get; set; }

        public EntityBaseSystem? EntityBaseSystem { get; set; }
        public Inventory? Inventory { get; set; }
        public Race? Race { get; set; }
        public Career? Career { get; set; }
    }
}
