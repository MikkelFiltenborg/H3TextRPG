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
    public class PotionRepo : IBaseCRUDRepo<Potion>
    {
        Dbcontext context;
        public PotionRepo(Dbcontext temp)
        {
            context = temp;
        }

        public void Create(Potion model)
        {
            //TODO: Should we return the (Potion)model?
            context.Potion.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Potion.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<Potion> GetAll()
        {
            return context.Potion.ToList();
        }

        public Potion GetById(int id)
        {
            return context.Potion.First(x => x.Id == id);
        }

        public void Update(Potion model)
        {
            context.Potion.Update(model);
            context.SaveChanges();
        }
    }
}
