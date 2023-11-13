using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Server;

namespace TextRPG.Repository.Repositories
{
    public class InventoryRepo : IBaseCRUDRepo<Inventory>
    {
        Dbcontext context;

        public InventoryRepo(Dbcontext temp)
        {
            context = temp;
        }

        public void Create(Inventory model)
        {
            context.Inventory.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Inventory.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<Inventory> GetAll()
        {
            return context.Inventory.ToList();
        }

        public Inventory GetById(int id)
        {
            return context.Inventory.First(x => x.Id == id);
        }

        public void Update(Inventory model)
        {
            context.Inventory.Update(model);
            context.SaveChanges();
        }
    }
}
