using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface ICareerRepo
    {
        List<Career> GetAll();
        Career GetById(int id);
        void Create(Career armour);
        void Update(Career armour);
        void Delete(int id);
    }
}
