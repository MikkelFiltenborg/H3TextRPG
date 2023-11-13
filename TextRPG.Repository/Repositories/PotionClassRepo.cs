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
    public class PotionClassRepo : IBaseCRUDRepo<PotionClass>
    {
        Dbcontext context;
        public PotionClassRepo(Dbcontext temp)
        {
            context = temp;
        }

        public void Create(PotionClass model)
        {
            context.PotionClass.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.PotionClass.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<PotionClass> GetAll()
        {
            return context.PotionClass.ToList();
        }

        public PotionClass GetById(int id)
        {
            return context.PotionClass.First(x => x.Id == id);
        }

        public void Update(PotionClass model)
        {
            context.PotionClass.Update(model);
            context.SaveChanges();
        }
    }
}
