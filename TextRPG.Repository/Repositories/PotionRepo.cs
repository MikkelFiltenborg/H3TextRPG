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
        /*
        public List<Potion> GetAll()
        {
            return context.Potion
                .Include(x => x.PotionType)
                .ToList();
        }*/

        // GetById
        public async Task<Potion> GetById(int id)
        {
            return await context.Potion
                .Include(x => x.PotionType)
                .FirstAsync(x => x.Id == id);
        }
        /*
        public Potion GetById(int id)
        {
            return context.Potion.First(x => x.Id == id);
            // return context.Potion.Include(x => x.PotionType).First(x => x.Id == id);
        }*/

        // Create
        public async Task<Potion> Create(Potion newPotion)
        {
            context.Potion.Add(newPotion);
            await context.SaveChangesAsync();
            return newPotion;
        }
        /*
        public void Create(Potion model)
        {
            //TODO: Should we return the (Potion)model?
            context.Potion.Add(model);
            context.SaveChanges();
        }*/

        // Update
        public async void Update(Potion updatePotion)
        {
            Potion potion = await GetById(updatePotion.Id);
            if (potion != null && updatePotion != null)
            {
                potion.Amount = updatePotion.Amount;
                potion.PotionTypeId = updatePotion.PotionTypeId;
                await context.SaveChangesAsync();
            }
        }
        /*
        public void Update(Potion potion)
        {
            context.Potion.Update(potion);
            context.SaveChanges();
        }*/

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
        /*
        public void Delete(int id)
        {
            context.Potion.Remove(GetById(id));
            context.SaveChanges();
        }*/
    }
}
