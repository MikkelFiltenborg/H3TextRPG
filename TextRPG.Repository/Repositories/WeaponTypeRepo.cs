using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Server;

namespace TextRPG.Repository.Repositories
{
    public class WeaponTypeRepo : IBaseCRUDRepo<WeaponType>
    {
        Dbcontext context;

        public WeaponTypeRepo(Dbcontext temp)
        {
            context = temp;
        }

        public void Create(WeaponType model)
        {
            context.WeaponType.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.WeaponType.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<WeaponType> GetAll()
        {
            return context.WeaponType.ToList();
        }

        public WeaponType GetById(int id)
        {
            return context.WeaponType.First(x => x.Id == id);
        }

        public void Update(WeaponType model)
        {
            context.WeaponType.Update(model);
            context.SaveChanges();
        }
    }
}
