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
        List<SkillRollType> GetAll();
        SkillRollType GetById(int id);
        void Create(SkillRollType skillRollClass);
        void Update(SkillRollType skillRollClass);
        void Delete(int id);
    }
}
