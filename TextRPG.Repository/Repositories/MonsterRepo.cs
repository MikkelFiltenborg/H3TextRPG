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
    public class MonsterRepo : IBaseCRUDRepo<Monster>
    {
        Dbcontext context;
        public MonsterRepo(Dbcontext temp)
        {
            context = temp;
        }
        public void Create(Monster model)
        {
            //TODO: Should we return the (Monster)model?
            context.Monster.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Monster.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<Monster> GetAll()
        {
            return context.Monster.ToList();
        }

        public Monster GetById(int id)
        {
            return context.Monster.First(x => x.Id == id);
        }

        public void Update(Monster model)
        {
            context.Monster.Update(model);
            context.SaveChanges();
        }
    }
}
