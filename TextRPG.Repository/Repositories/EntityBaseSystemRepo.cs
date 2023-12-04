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

        // GetById
        public async Task<EntityBaseSystem> GetById(int id)
        {
            return await context.EntityBaseSystem.FirstAsync(x => x.Id == id);
        }

        // Create
        public async Task<EntityBaseSystem> Create(EntityBaseSystem newEntityBaseSystem)
        {
            context.EntityBaseSystem.Add(newEntityBaseSystem);
            await context.SaveChangesAsync();
            return newEntityBaseSystem;
        }

        // Update
        public async Task<EntityBaseSystem?> Update(EntityBaseSystem updateEntityBaseSystem)
        {
            EntityBaseSystem entityBaseSystem = await GetById(updateEntityBaseSystem.Id);
            if (entityBaseSystem != null && updateEntityBaseSystem != null)
            {
                entityBaseSystem.Strength = updateEntityBaseSystem.Strength;
                entityBaseSystem.Vigor = updateEntityBaseSystem.Vigor;
                entityBaseSystem.Spirit = updateEntityBaseSystem.Spirit;
                entityBaseSystem.Health = updateEntityBaseSystem.Health;
                entityBaseSystem.Energy = updateEntityBaseSystem.Energy;
                entityBaseSystem.HealthModifier = updateEntityBaseSystem.HealthModifier;
                entityBaseSystem.EnergyModifier = updateEntityBaseSystem.EnergyModifier;
                entityBaseSystem.DamagerModifier = updateEntityBaseSystem.ArmourModifier;
                context.EntityBaseSystem.Update(entityBaseSystem);
                await context.SaveChangesAsync();
            }
            return entityBaseSystem;
        }

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
    }
}
