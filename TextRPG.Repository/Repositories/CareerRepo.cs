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
    public class CareerRepo : IBaseCRUDRepo<Career>
    {
        Dbcontext context;
        public CareerRepo(Dbcontext temp)
        {
            context = temp;
        }
        public void Create(Career model)
        {
            //TODO: Should we return the (Career)model?
            context.Career.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Career.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<Career> GetAll()
        {
            return context.Career.ToList();
        }

        public Career GetById(int id)
        {
            return context.Career.First(x => x.Id == id);
        }

        public void Update(Career model)
        {
            context.Career.Update(model);
            context.SaveChanges();
        }
    }
}
