using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public class Monster : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? MonsterName { get; set; }
        public int MonsterXp { get; set; }
        public int LevelDifficulty { get; set; }
        public string? Note { get; set; }
    }
}
