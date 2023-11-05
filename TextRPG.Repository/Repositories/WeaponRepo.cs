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
    public class WeaponRepo : IBaseCRUDRepo<Weapon>
    {
        Dbcontext context;
        public WeaponRepo(Dbcontext temp)
        {
            context = temp;
        }

        public void Create(Weapon model)
        {
            //TODO: Should we return the (Weapon)model?
            context.Weapon.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Weapon.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<Weapon> GetAll()
        {
            return context.Weapon.ToList();
        }

        public Weapon GetById(int id)
        {
            return context.Weapon.First(x => x.Id == id);
        }

        public void Update(Weapon model)
        {
            context.Weapon.Update(model);
            context.SaveChanges();
        }
    }
}
