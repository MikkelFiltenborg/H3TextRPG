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
    public class ArmourRepo : IBaseCRUDRepo<Armour>
    {
        Dbcontext context;
        public ArmourRepo(Dbcontext temp)
        {
            context = temp;
        }
        // GetAll
        public async Task<List<Armour>> GetAll()
        {
            return await context.Armour.ToListAsync();
        }
        /*
        public List<Armour> GetAll()
        {
            return context.Armour.ToList();
        }*/

        // GetById
        public async Task<Armour> GetById(int id)
        {
            return await context.Armour.FirstAsync(x => x.Id == id);
        }
        /*
        public Armour GetById(int id)
        {
            return context.Armour.First(x => x.Id == id);
        }*/

        // Create
        public async Task<Armour> Create(Armour newArmour)
        {
            context.Armour.Add(newArmour);
            await context.SaveChangesAsync();
            return newArmour;
        }
        /*
        public void Create(Armour model)
        {
            //TODO: Should we return the Model?
            context.Armour.Add(model);
            context.SaveChanges();
        }*/

        // Update
        public async Task<Armour?> Update(Armour updateArmour)
        {
            Armour armour = await GetById(updateArmour.Id);
            if (armour != null && updateArmour != null)
            {
                armour.ArmourTypeName = updateArmour.ArmourTypeName;
                armour.ArmourModifier = updateArmour.ArmourModifier;
                armour.AvailableToHero = updateArmour.AvailableToHero;
                armour.Value = updateArmour.Value;
                armour.Note = updateArmour.Note;
                context.Armour.Update(armour);
                await context.SaveChangesAsync();
                return armour;
            }
            return null;
        }
        /*
        public void Update(Armour model)
        {
            context.Armour.Update(model);
            context.SaveChanges();
        }*/

        // Delete
        public async Task<Armour> Delete(int id)
        {
            Armour armour = await GetById(id);
            if (armour != null)
            {
                context.Armour.Remove(armour);
                await context.SaveChangesAsync();
            }
            return armour!;
        }
        /*
        public void Delete(int id)
        {
            context.Armour.Remove(GetById(id));
            context.SaveChanges();
        }*/
    }
}
