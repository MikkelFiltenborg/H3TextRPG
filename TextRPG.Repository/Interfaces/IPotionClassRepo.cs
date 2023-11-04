using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface IPotionClassRepo
    {
        List<PotionClass> GetAll();
        PotionClass GetById(int id);
        void Create(PotionClass potionClass);
        void Update(PotionClass potionClass);
        void Delete(int id);
    }
}
