using System;
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
                .FirstAsync(x => x.Id == id);
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
        public async Task<Hero?> Update(Hero updateHero)
        {
            Hero hero = await GetById(updateHero.Id);

            if (hero != null && updateHero != null)
            {
                if (!string.IsNullOrWhiteSpace(updateHero.HeroName))
                    hero.HeroName = updateHero.HeroName;
                hero.HeroXp = updateHero.HeroXp;
                hero.Level = updateHero.Level;
                if (!string.IsNullOrWhiteSpace(updateHero.Note))
                    hero.Note = updateHero.Note;

                if (hero.EntityBaseSystem != null && updateHero.EntityBaseSystem != null)
                {
                    hero.EntityBaseSystem.Stength = updateHero.EntityBaseSystem.Stength;
                    hero.EntityBaseSystem.Agility = updateHero.EntityBaseSystem.Agility;
                    hero.EntityBaseSystem.Vigor = updateHero.EntityBaseSystem.Vigor;
                    hero.EntityBaseSystem.Spirit = updateHero.EntityBaseSystem.Spirit;
                    hero.EntityBaseSystem.Health = updateHero.EntityBaseSystem.Health;
                    hero.EntityBaseSystem.Energy = updateHero.EntityBaseSystem.Energy;
                    hero.EntityBaseSystem.HealthModifier = updateHero.EntityBaseSystem.HealthModifier;
                    hero.EntityBaseSystem.EnergyModifier = updateHero.EntityBaseSystem.EnergyModifier;
                    hero.EntityBaseSystem.DamagerModifier = updateHero.EntityBaseSystem.DamagerModifier;
                    hero.EntityBaseSystem.ArmourModifier = updateHero.EntityBaseSystem.ArmourModifier;
                }

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

                    if (updateHero.Inventory.Potions != null && hero.Inventory.Potions != null)
                    {
                        List<Potion> temp = new List<Potion>();
                        foreach (var potion in updateHero.Inventory.Potions)
                        {
                            if (hero.Inventory.Potions.Exists(x => x.PotionTypeId == potion.PotionTypeId))
                            {
                                //Update potion
                                var p = hero.Inventory.Potions.Find(x => x.PotionTypeId == potion.PotionTypeId);
                                if (p != null)
                                {
                                    p.InventoryId = potion.InventoryId;
                                    p.PotionTypeId = potion.PotionTypeId;
                                    p.Amount = potion.Amount;
                                }
                            }
                            else
                            {
                                //Add New potion
                                temp.Add(new Potion
                                {
                                    InventoryId = hero.Inventory.Id,
                                    PotionTypeId = potion.PotionTypeId,
                                    Amount = potion.Amount
                                });
                            }
                        }
                        hero.Inventory.Potions.AddRange(temp);
                    }
                }
                context.Hero.Update(hero);
                await context.SaveChangesAsync();
                return hero;
            }
            return null;
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
