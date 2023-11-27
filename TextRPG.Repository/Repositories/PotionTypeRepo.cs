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
    public class PotionTypeRepo : IBaseCRUDRepo<PotionType>
    {
        Dbcontext context;
        public PotionTypeRepo(Dbcontext temp)
        {
            context = temp;
        }

        // GetAll
        public async Task<List<PotionType>> GetAll()
        {
            return await context.PotionType.ToListAsync();
        }
        /*
        public List<PotionType> GetAll()
        {
            return context.PotionType.ToList();
        }*/

        // GetById
        public async Task<PotionType> GetById(int id)
        {
            return await context.PotionType.FirstAsync(x => x.Id == id);
        }
        /*
        public PotionType GetById(int id)
        {
            return context.PotionType.First(x => x.Id == id);
        }*/

        // Create
        public async Task<PotionType> Create(PotionType newPotionType)
        {
            context.PotionType.Add(newPotionType);
            await context.SaveChangesAsync();
            return newPotionType;
        }
        /*
        public void Create(PotionType model)
        {
            context.PotionType.Add(model);
            context.SaveChanges();
        }*/

        // Update
        public async Task<PotionType?> Update(PotionType updatePotionType)
        {
            PotionType potionType = await GetById(updatePotionType.Id);
            if (potionType != null && updatePotionType != null)
            {
                potionType.PotionTypeName = updatePotionType.PotionTypeName;
                potionType.PotionDice = updatePotionType.PotionDice;
                potionType.AvailableToHero = updatePotionType.AvailableToHero;
                potionType.Value = updatePotionType.Value;
                potionType.Note = updatePotionType.Note;
                context.PotionType.Update(potionType);
                await context.SaveChangesAsync();
            }
            return potionType;
        }
        /*
        public void Update(PotionType model)
        {
            context.PotionType.Update(model);
            context.SaveChanges();
        }*/

        // Delete
        public async Task<PotionType> Delete(int id)
        {
            PotionType potionType = await GetById(id);
            if (potionType != null)
            {
                context.PotionType.Remove(potionType);
                await context.SaveChangesAsync();
            }
            return potionType!;
        }
        /*
        public void Delete(int id)
        {
            context.PotionType.Remove(GetById(id));
            context.SaveChanges();
        }*/
    }
}
