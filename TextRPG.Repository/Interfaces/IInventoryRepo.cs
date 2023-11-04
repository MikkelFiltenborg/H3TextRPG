using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface IInventoryRepo
    {
        List<Inventory> GetAll();
        Inventory GetById(int id);
        void Create(Inventory inventory);
        void Update(Inventory inventory);
        void Delete(int id);
    }
}
