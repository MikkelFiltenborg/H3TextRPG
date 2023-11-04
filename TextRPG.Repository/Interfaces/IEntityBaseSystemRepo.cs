using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface IEntityBaseSystemRepo
    {
        List<EntityBaseSystem> GetAll();
        EntityBaseSystem GetById(int id);
        void Create(EntityBaseSystem entityBaseSystem);
        void Update(EntityBaseSystem entityBaseSystem);
        void Delete(int id);
    }
}
