﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public class Hero : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string? HeroName { get; set; }
        public int HeroXp { get; set; }
        public int Level { get; set; }
        public int RaceId { get; set; }
        public int CareerId { get; set; }
        public string? Note { get; set; }

        public Race? Race { get; set; }
        public Career? Career { get; set; }
    }
}
