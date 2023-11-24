using Azure.Core;
using Microsoft.EntityFrameworkCore;
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

        // GetAll
        public async Task<List<EntityBaseSystem>> GetAll()
        {
            return await context.EntityBaseSystem.ToListAsync();
        }
        /*
        public List<EntityBaseSystem> GetAll()
        {
            return context.EntityBaseSystem.ToList();
        }*/

        // GetById
        public async Task<EntityBaseSystem> GetById(int id)
        {
            return await context.EntityBaseSystem.FirstAsync(x => x.Id == id);
        }
        /*
        public EntityBaseSystem GetById(int id)
        {
            return context.EntityBaseSystem.First(x => x.Id == id);
        }*/

        // Create
        public async Task<EntityBaseSystem> Create(EntityBaseSystem newEntityBaseSystem)
        {
            context.EntityBaseSystem.Add(newEntityBaseSystem);
            await context.SaveChangesAsync();
            return newEntityBaseSystem;
        }
        /*
        public void Create(EntityBaseSystem model)
        {
            //TODO: Should we return the Model?
            context.EntityBaseSystem.Add(model);
            context.SaveChanges();
        }*/

        // Update
        public async void Update(EntityBaseSystem updateEntityBaseSystem)
        {
            EntityBaseSystem entityBaseSystem = await GetById(updateEntityBaseSystem.Id);
            if (entityBaseSystem != null && updateEntityBaseSystem != null)
            {
                entityBaseSystem.Stength = updateEntityBaseSystem.Stength;
                entityBaseSystem.Vigor = updateEntityBaseSystem.Vigor;
                entityBaseSystem.Spirit = updateEntityBaseSystem.Spirit;
                entityBaseSystem.Health = updateEntityBaseSystem.Health;
                entityBaseSystem.Energy = updateEntityBaseSystem.Energy;
                entityBaseSystem.HealthModifier = updateEntityBaseSystem.HealthModifier;
                entityBaseSystem.EnergyModifier = updateEntityBaseSystem.EnergyModifier;
                entityBaseSystem.DamagerModifier = updateEntityBaseSystem.ArmourModifier;
                await context.SaveChangesAsync();
            }
        }
        /*
        public void Update(EntityBaseSystem model)
        {
            context.EntityBaseSystem.Update(model);
            context.SaveChanges();
        }*/

        // Delete
        public async Task<EntityBaseSystem> Delete(int id)
        {
            EntityBaseSystem entityBaseSystem = await GetById(id);
            if (entityBaseSystem != null)
            {
                context.EntityBaseSystem.Remove(entityBaseSystem);
                await context.SaveChangesAsync();
            }
            return entityBaseSystem!;
        }
        /*
        public void Delete(int id)
        {
            context.EntityBaseSystem.Remove(GetById(id));
            context.SaveChanges();
        }*/
    }
}
