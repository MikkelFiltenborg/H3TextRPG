using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface IRaceRepo
    {
        List<Race> GetAll();
        Race GetById(int id);
        void Create(Race race);
        void Update(Race race);
        void Delete(int id);
    }
}
