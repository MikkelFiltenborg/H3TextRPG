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
    public class SkillRollClassRepo : IBaseCRUDRepo<SkillRollType>
    {
        Dbcontext context;

        public SkillRollClassRepo(Dbcontext temp)
        {
            context = temp;
        }

        public void Create(SkillRollType model)
        {
            context.SkillRollClass.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.SkillRollClass.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<SkillRollType> GetAll()
        {
            return context.SkillRollClass.ToList();
        }

        public SkillRollType GetById(int id)
        {
            return context.SkillRollClass.First(x => x.Id == id);
        }

        public void Update(SkillRollType model)
        {
            context.SkillRollClass.Update(model);
            context.SaveChanges();
        }
    }
}
