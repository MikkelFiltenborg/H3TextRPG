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

        public void Create(Hero model)
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
                //if (model.Inventory.Potions != null)
                //{
                //    //var PotionsSelected = context.Potion.
                //    //    Where(p => model.Inventory.Potions.Select(x => x.Id).ToArray().Contains(p.Id)).ToList();
                    
                //        temp.Potions!.AddRange(PotionsSelected);
                //}
                model.Inventory = temp;
            }

            context.Hero.Add(model);
            context.SaveChanges();

        }

        public void Delete(int id)
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
        }

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
        }

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
        }

        public void Update(Hero model)
        {
            var oldModel = GetById(model.Id);

            //throw new NotImplementedException("Update not here yet, fuck off");
            context.Hero.Update(oldModel);
            context.SaveChanges();
        }
    }
}
