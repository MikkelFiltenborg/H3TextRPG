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
    public class WeaponClassRepo : IBaseCRUDRepo<WeaponType>
    {
        Dbcontext context;

        public WeaponClassRepo(Dbcontext temp)
        {
            context = temp;
        }

        public void Create(WeaponType model)
        {
            context.WeaponClass.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.WeaponClass.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<WeaponType> GetAll()
        {
            return context.WeaponClass.ToList();
        }

        public WeaponType GetById(int id)
        {
            return context.WeaponClass.First(x => x.Id == id);
        }

        public void Update(WeaponType model)
        {
            context.WeaponClass.Update(model);
            context.SaveChanges();
        }
    }
}
