using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface IHeroRepo
    {
        List<Hero> GetAll();
        Hero GetById(int id);
        void Create(Hero hero);
        void Update(Hero hero);
        void Delete(int id);
    }
}
