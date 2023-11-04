using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface IWeaponRepo
    {
        List<Weapon> GetAll();
        Weapon GetById(int id);
        void Create(Weapon weapon);
        void Update(Weapon weapon);
        void Delete(int id);
    }
}
