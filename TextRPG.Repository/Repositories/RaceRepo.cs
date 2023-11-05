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
    public class RaceRepo : IBaseCRUDRepo<Race>
    {
        Dbcontext context;
        public RaceRepo(Dbcontext temp)
        {
            context = temp;
        }
        public void Create(Race model)
        {
            //TODO: Should we return the (Race)model?
            context.Race.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Race.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<Race> GetAll()
        {
            return context.Race.ToList();
        }

        public Race GetById(int id)
        {
            return context.Race.First(x => x.Id == id);
        }

        public void Update(Race model)
        {
            context.Race.Update(model);
            context.SaveChanges();
        }
    }
}
