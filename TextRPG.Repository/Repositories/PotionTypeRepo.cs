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
    public class PotionTypeRepo : IBaseCRUDRepo<PotionType>
    {
        Dbcontext context;
        public PotionTypeRepo(Dbcontext temp)
        {
            context = temp;
        }

        public void Create(PotionType model)
        {
            context.PotionType.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.PotionType.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<PotionType> GetAll()
        {
            return context.PotionType.ToList();
        }

        public PotionType GetById(int id)
        {
            return context.PotionType.First(x => x.Id == id);
        }

        public void Update(PotionType model)
        {
            context.PotionType.Update(model);
            context.SaveChanges();
        }
    }
}
