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
    public class ArmourRepo : IBaseCRUDRepo<Armour>
    {
        Dbcontext context;
        public ArmourRepo(Dbcontext temp)
        {
            context = temp;
        }

        public void Create(Armour model)
        {
            //TODO: Should we return the Model?
            context.Armour.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Armour.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<Armour> GetAll()
        {
            return context.Armour.ToList();
        }

        public Armour GetById(int id)
        {
            return context.Armour.First(x => x.Id == id);
        }

        public void Update(Armour model)
        {
            context.Armour.Update(model);
            context.SaveChanges();
        }
    }
}
