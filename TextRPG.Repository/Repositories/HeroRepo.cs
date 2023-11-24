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
    public class HeroRepo : IBaseCRUDRepo<Hero>
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
        /*
        public List<Hero> GetAll()
        {
            return context.Hero
                .Include(x => x.Inventory)
                .Include(x => x.Inventory!.Armour)
                .Include(x => x.Inventory!.Potions!)
                .ThenInclude(x => x.PotionType)
                .Include(x => x.Inventory!.Weapons!)
                .ThenInclude(x => x.WeaponType)
                .ThenInclude(x => x!.SkillRollType)
                .Include(x => x.EntityBaseSystem)
                .Include(x => x.Race)
                .Include(x => x.Career)
                .ToList();
        }*/

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
                .FirstAsync();
        }
        /*
        public Hero GetById(int id)
        {
            return context.Hero
                .Include(x => x.Inventory)
                .Include(x => x.Inventory!.Armour)
                .Include(x => x.Inventory!.Potions!)
                .ThenInclude(x => x.PotionType)
                .Include(x => x.Inventory!.Weapons!)
                .ThenInclude(x => x.WeaponType)
                .ThenInclude(x => x!.SkillRollType)
                .Include(x => x.EntityBaseSystem)
                .Include(x => x.Race)
                .Include(x => x.Career)
                .First(x => x.Id == id);
        }*/

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
                /*
                if (model.Inventory.Potions != null)
                {
                    var PotionsSelected = context.Potion.
                        Where(p => model.Inventory.Potions.Select(x => x.Id).ToArray().Contains(p.Id)).ToList();
                    
                        temp.Potions!.AddRange(PotionsSelected);
                }*/
                model.Inventory = temp;
            }

            context.Hero.Add(model);
            await context.SaveChangesAsync();
            return model;

        }
        /*
        public void Create(Hero model)
        {
            context.Hero.Add(model);
            context.SaveChanges();
        }*/

        // Update
        public async void Update(Hero updateHero)
        {
            Hero hero = await GetById(updateHero.Id);
            //if (hero != null && updateHero != null)
            //{
            //    hero.HeroName = updateHero.HeroName;
            //    hero.HeroXp = updateHero.HeroXp;
            //    hero.Level = updateHero.Level;
            //    hero.Note = updateHero.Note;
            //    await context.SaveChangesAsync();
            //}
            if (updateHero.Inventory != null && hero.Inventory != null)
            {
                hero.Inventory.Id = updateHero.Inventory.Id;
                hero.Inventory.Gold = updateHero.Inventory.Gold;
                hero.Inventory.ArmourId = updateHero.Inventory.ArmourId;

                
                if (updateHero.Inventory.Weapons != null)
                {
                    var weaponsSelected = context.Weapon.
                        Where(w => updateHero.Inventory.Weapons.Select(x => x.Id).ToArray().Contains(w.Id)).ToList();

                    hero.Inventory.Weapons = weaponsSelected;
                }

                if (updateHero.Inventory.Potions != null)
                {
                    var PotionsSelected = context.Potion.
                        Where(p => updateHero.Inventory.Potions.Select(x => x.Id).ToArray().Contains(p.Id)).ToList();

                    foreach (var potion in updateHero.Inventory.Potions)
                    {
                        if (!PotionsSelected.Contains(potion))
                        {
                            PotionsSelected.Add(potion);
                        }
                    }
                    hero.Inventory.Potions = PotionsSelected;
                    //temp.Potions!.AddRange(PotionsSelected);
                }
                //updateHero.Inventory = temp;
            }

            context.Hero.Add(hero);
            await context.SaveChangesAsync();
        }
        /*
        public void Update(Hero model)
        {
            var oldModel = GetById(model.Id);

            //throw new NotImplementedException("Update not here yet, fuck off");
            context.Hero.Update(oldModel);
            context.SaveChanges();
        }*/

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

        /*
        // Delete (ditjo)
        public async Task<List<Hero>> Delete(int id)
        {
            var hero = GetById(id);
            if (hero != null)
            {
                if (hero.EntityBaseSystem is not null)
                    context.EntityBaseSystem.Remove(hero.EntityBaseSystem);
                if (hero.Inventory is not null)
                {
                    if (hero.Inventory.Potions is not null)
                        hero.Inventory.Potions.ForEach(x => context.Potion.Remove(x));
                    context.Inventory.Remove(hero.Inventory);
                }
                context.Hero.Remove(hero);
            }
            context.SaveChanges();
        }*/
        /*
        public void Delete(int id)
        {
            context.Hero.Remove(GetById(id));
            context.SaveChanges();
        }*/
    }
}
