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
    public class WeaponRepo : IBaseCRUDRepo<Weapon>
    {
        Dbcontext context;
        public WeaponRepo(Dbcontext temp)
        {
            context = temp;
        }

        // GetAll
        public async Task<List<Weapon>> GetAll()
        {
            return await context.Weapon
                /*
                .Include(x => x.WeaponDamageModifier)
                .Include(x => x.SkillRoll)
                .Include(x => x.Range)
                .Include(x => x.AvailableToHero)
                .Include(x => x.StarterWeapon)
                .Include(x => x.Value)
                .Include(x => x.Note)*/
                .Include(x => x.WeaponType)
                .ThenInclude(x => x!.SkillRollType)
                .ToListAsync();
        }
        /*
        //public List<Weapon> GetAll()
        //{
        //    return context.Weapon
        //        .Include(x => x.WeaponType)
        //        .ToList();
        //}*/

        // GetById
        public async Task<Weapon> GetById(int id)
        {
            return await context.Weapon
                /*
                .Include(x => x.WeaponDamageModifier)
                .Include(x => x.SkillRoll)
                .Include(x => x.Range)
                .Include(x => x.AvailableToHero)
                .Include(x => x.StarterWeapon)
                .Include(x => x.Value)
                .Include(x => x.Note)*/
                .Include(x => x.WeaponType)
                .ThenInclude(x => x!.SkillRollType)
                .FirstAsync(x => x.Id == id);
        }
        /*
        //public Weapon GetById(int id)
        //{
        //    return context.Weapon
        //        .Include(x => x.WeaponType)
        //        .First(x => x.Id == id);
        //}*/

        // Create
        public async Task<Weapon> Create(Weapon newWeapon)
        {
            context.Weapon.Add(newWeapon);
            await context.SaveChangesAsync();
            return newWeapon;
        }
        /*
        //public void Create(Weapon model)
        //{
        //    //TODO: Should we return the (Weapon)model?
        //    context.Weapon.Add(model);
        //    context.SaveChanges();
        //}*/

        // Update
        public async Task<Weapon?> Update(Weapon updateWeapon)
        {
            Weapon weapon = await GetById(updateWeapon.Id);
            if (weapon != null && updateWeapon != null)
            {
                if (!string.IsNullOrWhiteSpace(updateWeapon.WeaponName))
                    weapon.WeaponName = updateWeapon.WeaponName;
                weapon.WeaponDamageModifier = updateWeapon.WeaponDamageModifier;
                weapon.WeaponTypeId = updateWeapon.WeaponTypeId;
                weapon.SkillRoll = updateWeapon.SkillRoll;
                weapon.AvailableToHero = updateWeapon.AvailableToHero;
                weapon.StarterWeapon = updateWeapon.StarterWeapon;
                weapon.Value = updateWeapon.Value;
                if (!string.IsNullOrWhiteSpace(updateWeapon.Note))
                    weapon.Note = updateWeapon.Note;

                context.Weapon.Update(weapon);
                await context.SaveChangesAsync();
            }
            return weapon;
        }
        /*
        //public void Update(Weapon model)
        //{
        //    context.Weapon.Update(model);
        //    context.SaveChanges();
        //}*/

        // Delete
        public async Task<Weapon> Delete(int id)
        {
            Weapon weapon = await GetById(id);
            if (weapon != null)
            {
                context.Weapon.Remove(weapon);
                await context.SaveChangesAsync();
            }
            return weapon!;
        }
        /*
        //public void Delete(int id)
        //{
        //    context.Weapon.Remove(GetById(id));
        //    context.SaveChanges();
        //}*/
    }
}
