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
    public class SkillRollTypeRepo : IBaseCRUDRepo<SkillRollType>
    {
        Dbcontext context;

        public SkillRollTypeRepo(Dbcontext temp)
        {
            context = temp;
        }

        public void Create(SkillRollType model)
        {
            context.SkillRollType.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.SkillRollType.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<SkillRollType> GetAll()
        {
            return context.SkillRollType.ToList();
        }

        public SkillRollType GetById(int id)
        {
            return context.SkillRollType.First(x => x.Id == id);
        }

        public void Update(SkillRollType model)
        {
            context.SkillRollType.Update(model);
            context.SaveChanges();
        }
    }
}
