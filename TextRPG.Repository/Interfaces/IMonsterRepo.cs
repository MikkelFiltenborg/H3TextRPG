using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface IMonsterRepo
    {
        List<Monster> GetAll();
        Monster GetById(int id);
        void Create(Monster monster);
        void Update(Monster monster);
        void Delete(int id);
    }
}
