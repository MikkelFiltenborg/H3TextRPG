using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface IPotionsRepo
    {
        List<Potion> GetAll();
        Potion GetById(int id);
        void Create(Potion potion);
        void Update(Potion potion);
        void Delete(int id);
    }
}
