using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface ISkillRollClassRepo
    {
        List<SkillRollClass> GetAll();
        SkillRollClass GetById(int id);
        void Create(SkillRollClass skillRollClass);
        void Update(SkillRollClass skillRollClass);
        void Delete(int id);
    }
}
