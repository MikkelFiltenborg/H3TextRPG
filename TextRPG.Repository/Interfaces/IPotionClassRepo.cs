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
        List<PotionType> GetAll();
        PotionType GetById(int id);
        void Create(PotionType potionClass);
        void Update(PotionType potionClass);
        void Delete(int id);
    }
}
