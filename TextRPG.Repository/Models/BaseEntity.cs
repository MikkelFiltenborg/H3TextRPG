using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Models
{
    public abstract class BaseEntity
    {
        public EntityBaseSystem? EntityBaseSystem { get; set; }
        public Inventory? Inventory { get; set; }
    }
}
