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
    public class WeaponTypeRepo : IBaseCRUDRepo<WeaponType>
    {
        Dbcontext context;

        public WeaponTypeRepo(Dbcontext temp)
        {
            context = temp;
        }

        // GetAll
        public async Task<List<WeaponType>> GetAll()
        {
            return await context.WeaponType
                .Include(x => x.SkillRollType)
                .ToListAsync();
        }

        // GetById
        public async Task<WeaponType> GetById(int id)
        {
            return await context.WeaponType
                .Include(x => x.SkillRollType)
                .FirstAsync(x => x.Id == id);
        }

        // Create
        public async Task<WeaponType> Create(WeaponType newWeaponType)
        {
            context.WeaponType.Add(newWeaponType);
            await context.SaveChangesAsync();
            return newWeaponType;
        }

        // Update
        public async Task<WeaponType?> Update(WeaponType updateWeaponType)
        {
            WeaponType weaponType = await GetById(updateWeaponType.Id);
            if (weaponType != null && updateWeaponType != null)
            {
                weaponType.WeaponTypeName = updateWeaponType.WeaponTypeName;
                weaponType.EnergyCost = updateWeaponType.EnergyCost;
                weaponType.DamageDice = updateWeaponType.DamageDice;
                weaponType.SkillRollTypeId = updateWeaponType.SkillRollTypeId;
                context.WeaponType.Update(weaponType);
                await context.SaveChangesAsync();
            }
            return weaponType;
        }

        // Delete
        public async Task<WeaponType> Delete(int id)
        {
            WeaponType weaponType = await GetById(id);
            if (weaponType != null)
            {
                context.WeaponType.Remove(weaponType);
                await context.SaveChangesAsync();
            }
            return weaponType!;
        }
    }
}
