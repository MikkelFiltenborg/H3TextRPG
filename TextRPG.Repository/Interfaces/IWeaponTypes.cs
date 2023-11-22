using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Interfaces
{
    public interface IWeaponTypes
    {
        List<WeaponType> GetAll();
        WeaponType GetById(int id);
        void Create(WeaponType weaponClass);
        void Update(WeaponType weaponClass);
        void Delete(int id);
    }
}
