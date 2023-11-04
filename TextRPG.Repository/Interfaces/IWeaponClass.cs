using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface IWeaponClass
    {
        List<WeaponClass> GetAll();
        WeaponClass GetById(int id);
        void Create(WeaponClass weaponClass);
        void Update(WeaponClass weaponClass);
        void Delete(int id);
    }
}
