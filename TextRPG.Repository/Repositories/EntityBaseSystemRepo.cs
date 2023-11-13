using Azure.Core;
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
    public class EntityBaseSystemRepo : IBaseCRUDRepo<EntityBaseSystem>
    {
        Dbcontext context;
        public EntityBaseSystemRepo(Dbcontext temp)
        {
            context = temp;
        }

        public void Create(EntityBaseSystem model)
        {
            //TODO: Should we return the Model?
            context.EntityBaseSystem.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.EntityBaseSystem.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<EntityBaseSystem> GetAll()
        {
            return context.EntityBaseSystem.ToList();
        }

        public EntityBaseSystem GetById(int id)
        {
            return context.EntityBaseSystem.First(x => x.Id == id);
        }

        public void Update(EntityBaseSystem model)
        {
            context.EntityBaseSystem.Update(model);
            context.SaveChanges();
        }
    }
}
