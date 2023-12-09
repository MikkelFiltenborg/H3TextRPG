﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;

namespace TextRPG.Repository.Repositories
{
    public class HeroRepo : BaseEntityRepo, IBaseCRUDRepo<Hero>
    {
        Dbcontext context;

        public HeroRepo(Dbcontext temp)
        {
            context = temp;
        }

        // GetAll
        public async Task<List<Hero>> GetAll()
        {
            return await context.Hero
                .Include(x => x.EntityBaseSystem)
                .Include(x => x.Inventory)
                .Include(x => x.Inventory!.Armour)
                .Include(x => x.Inventory!.Potions!)
                .ThenInclude(x => x.PotionType)
                .Include(x => x.Inventory!.Weapons!)
                .ThenInclude(x => x.WeaponType)
                .ThenInclude(x => x!.SkillRollType)
                .Include(x => x.Race)
                .Include(x => x.Career)
                .ToListAsync();
        }

        // GetById
        public async Task<Hero> GetById(int id)
        {
            return await context.Hero
                .Include(x => x.EntityBaseSystem)
                .Include(x => x.Inventory)
                .Include(x => x.Inventory!.Armour)
                .Include(x => x.Inventory!.Potions!)
                .ThenInclude(x => x.PotionType)
                .Include(x => x.Inventory!.Weapons!)
                .ThenInclude(x => x.WeaponType)
                .ThenInclude(x => x!.SkillRollType)
                .Include(x => x.Race)
                .Include(x => x.Career)
                .FirstAsync(x => x.Id == id);
        }

        // Create
        public async Task<Hero> Create(Hero model)
        {
            if (model.Inventory != null)
            {
                Inventory temp = new()
                {
                    Id = model.Inventory.Id,
                    Gold = model.Inventory.Gold,
                    ArmourId = model.Inventory.ArmourId,
                    Weapons = new List<Weapon>(),
                    Potions = model.Inventory.Potions
                };

                if (model.Inventory.Weapons != null)
                {
                    var weaponsSelected = context.Weapon.
                        Where(w => model.Inventory.Weapons.Select(x => x.Id).ToArray().Contains(w.Id)).ToList();

                    temp.Weapons.AddRange(weaponsSelected);
                }
                //TODO: Reevaluate if this is needed here ↓.
                /*
                if (model.Inventory.Potions != null)
                {
                    var PotionsSelected = context.Potion.
                        Where(p => model.Inventory.Potions.Select(x => x.Id).ToArray().Contains(p.Id)).ToList();
                    
                        temp.Potions!.AddRange(PotionsSelected);
                }*/
                model.Inventory = temp;
            }

            var i = context.Hero.Add(model);
            await context.SaveChangesAsync();
            return model;

        }

        // Update
        public async Task<Hero?> Update(Hero updateHero)
        {
            Hero hero = await GetById(updateHero.Id);

            if (hero != null && updateHero != null)
            {
                if (!string.IsNullOrWhiteSpace(updateHero.HeroName))
                    hero.HeroName = updateHero.HeroName;
                hero.HeroXp = updateHero.HeroXp;
                hero.Level = updateHero.Level;
                hero.CareerId = updateHero.CareerId;
                hero.RaceId = updateHero.RaceId;
                if (!string.IsNullOrWhiteSpace(updateHero.Note))
                    hero.Note = updateHero.Note;

                if (hero.EntityBaseSystem != null && updateHero.EntityBaseSystem != null)
                {
                    UpdateEBS(hero.EntityBaseSystem, updateHero.EntityBaseSystem);
                }

                if (updateHero.Inventory != null && hero.Inventory != null)
                {
                    UpdateInventory(hero.Inventory, updateHero.Inventory);

                    if (updateHero.Inventory.Weapons != null)
                    {
                        var weaponsSelected = context.Weapon.
                            Where(w => updateHero.Inventory.Weapons.Select(x => x.Id).ToArray().Contains(w.Id)).ToList();

                        hero.Inventory.Weapons = weaponsSelected;
                    }

                    if (updateHero.Inventory.Potions != null && hero.Inventory.Potions != null)
                    {
                        UpdatePotion(hero.Inventory, updateHero.Inventory);
                    }
                }
                context.Hero.Update(hero);
                await context.SaveChangesAsync();
                return hero;
            }
            return null;
        }

        // Delete
        public async Task<Hero> Delete(int id)
        {
            Hero hero = await GetById(id);
            if (hero != null)
            {
                context.Hero.Remove(hero);
                await context.SaveChangesAsync();
            }
            return hero!;
        }
    }
}
