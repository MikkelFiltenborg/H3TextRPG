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

        // GetById
        public async Task<Armour> GetById(int id)
        {
            return await context.Armour.FirstAsync(x => x.Id == id);
        }

        // Create
        public async Task<Armour> Create(Armour newArmour)
        {
            context.Armour.Add(newArmour);
            await context.SaveChangesAsync();
            return newArmour;
        }

        // Update
        public async Task<Armour?> Update(Armour updateArmour)
        {
            Armour armour = await GetById(updateArmour.Id);
            if (armour != null && updateArmour != null)
            {
                if (!string.IsNullOrWhiteSpace(updateArmour.ArmourTypeName))
                    armour.ArmourTypeName = updateArmour.ArmourTypeName;
                armour.ArmourModifier = updateArmour.ArmourModifier;
                armour.AvailableToHero = updateArmour.AvailableToHero;
                armour.Value = updateArmour.Value;
                if (!string.IsNullOrWhiteSpace(updateArmour.Note))
                    armour.Note = updateArmour.Note;

                context.Armour.Update(armour);
                await context.SaveChangesAsync();
                return armour;
            }
            return null;
        }

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
    }
}
