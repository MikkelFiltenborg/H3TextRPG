using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Server;
using Microsoft.EntityFrameworkCore;

namespace TextRPG.Repository.Repositories
{
    public class PotionRepo : IBaseCRUDRepo<Potion>
    {
        Dbcontext context;
        public PotionRepo(Dbcontext temp)
        {
            context = temp;
        }

        // GetAll
        public async Task<List<Potion>> GetAll()
        {
            return await context.Potion
                .Include(x => x.PotionType)
                .ToListAsync();
        }

        // GetById
        public async Task<Potion> GetById(int id)
        {
            return await context.Potion
                .Include(x => x.PotionType)
                .FirstAsync(x => x.Id == id);
        }

        // Create
        public async Task<Potion> Create(Potion newPotion)
        {
            context.Potion.Add(newPotion);
            await context.SaveChangesAsync();
            return newPotion;
        }

        // Update
        public async Task<Potion?> Update(Potion updatePotion)
        {
            Potion potion = await GetById(updatePotion.Id);
            if (potion != null && updatePotion != null)
            {
                potion.Amount = updatePotion.Amount;
                potion.PotionTypeId = updatePotion.PotionTypeId;
                context.Potion.Update(potion);
                await context.SaveChangesAsync();
            }
            return potion;
        }

        // Delete
        public async Task<Potion> Delete(int id)
        {
            Potion potion = await GetById(id);
            if (potion != null)
            {
                context.Potion.Remove(potion);
                await context.SaveChangesAsync();
            }
            return potion!;
        }
    }
}
