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
                /*
                .Include(x => x.WeaponTypeName)
                .Include(x => x.EnergyCost)
                .Include(x => x.DamageDice)
                .Include(x => x.SkillRollTypeId)*/
                .Include(x => x.SkillRollType)
                .ToListAsync();
        }
        /*
        public void Create(WeaponType model)
        {
            context.WeaponType.Add(model);
            context.SaveChanges();
        }*/

        // GetById
        public async Task<WeaponType> GetById(int id)
        {
            return await context.WeaponType
                /*
                .Include(x => x.WeaponTypeName)
                .Include(x => x.EnergyCost)
                .Include(x => x.DamageDice)
                .Include(x => x.SkillRollTypeId)*/
                .Include(x => x.SkillRollType)
                .FirstAsync(x => x.Id == id);
        }
        /*
        public void Delete(int id)
        {
            context.WeaponType.Remove(GetById(id));
            context.SaveChanges();
        }*/

        // Create
        public async Task<WeaponType> Create(WeaponType newWeaponType)
        {
            context.WeaponType.Add(newWeaponType);
            await context.SaveChangesAsync();
            return newWeaponType;
        }
        /*
        public List<WeaponType> GetAll()
        {
            return context.WeaponType.ToList();
        }*/

        // Update
        public async void Update(WeaponType updateWeaponType)
        {
            WeaponType weaponType = await GetById(updateWeaponType.Id);
            if (weaponType != null && updateWeaponType != null)
            {
                weaponType.WeaponTypeName = updateWeaponType.WeaponTypeName;
                weaponType.EnergyCost = updateWeaponType.EnergyCost;
                weaponType.DamageDice = updateWeaponType.DamageDice;
                weaponType.SkillRollTypeId = updateWeaponType.SkillRollTypeId;
                await context.SaveChangesAsync();
            }
        }
        /*
        public WeaponType GetById(int id)
        {
            return context.WeaponType.First(x => x.Id == id);
        }*/

        // Delete
        public async void Delete(int id)
        {
            WeaponType weaponType = await GetById(id);
            if (weaponType != null)
            {
                context.WeaponType.Remove(weaponType);
                await context.SaveChangesAsync();
            }
        }
        /*
        public void Update(WeaponType model)
        {
            context.WeaponType.Update(model);
            context.SaveChanges();
        }*/
    }
}
