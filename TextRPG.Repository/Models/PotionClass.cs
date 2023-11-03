using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public class PotionClass
    {
        [Key]
        public int Id { get; set; }
        public string? PotionType { get; set; }
    }
}
