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
    public class MonsterRepo : BaseEntityRepo, IBaseCRUDRepo<Monster>
    {
        Dbcontext context;
        public MonsterRepo(Dbcontext temp)
        {
            context = temp;
        }

        // GetAll
        public async Task<List<Monster>> GetAll()
        {
            return await context.Monster
                .Include(x => x.EntityBaseSystem)
                .Include(x => x.Inventory)
                .Include(x => x.Inventory!.Armour)
                .Include(x => x.Inventory!.Potions!)
                .ThenInclude(x => x.PotionType)
                .Include(x => x.Inventory!.Weapons!)
                .ThenInclude(x => x.WeaponType)
                .ThenInclude(x => x!.SkillRollType)
                .ToListAsync();
        }

        // GetById
        public async Task<Monster> GetById(int id)
        {
            return await context.Monster
                .Include(x => x.EntityBaseSystem)
                .Include(x => x.Inventory)
                .Include(x => x.Inventory!.Armour)
                .Include(x => x.Inventory!.Potions!)
                .ThenInclude(x => x.PotionType)
                .Include(x => x.Inventory!.Weapons!)
                .ThenInclude(x => x.WeaponType)
                .ThenInclude(x => x!.SkillRollType)
                .FirstAsync(x => x.Id == id);
        }

        // Create
        public async Task<Monster> Create (Monster newMonster)
        {
            if (newMonster.Inventory != null)
            {
                Inventory temp = new()
                {
                    Id = newMonster.Inventory.Id,
                    Gold = newMonster.Inventory.Gold,
                    ArmourId = newMonster.Inventory.ArmourId,
                    Weapons = new List<Weapon>(),
                    Potions = newMonster.Inventory.Potions
                };

                if (newMonster.Inventory.Weapons != null)
                {
                    var weaponsSelected = context.Weapon.
                        Where(w => newMonster.Inventory.Weapons.Select(x => x.Id).ToArray().Contains(w.Id)).ToList();

                    temp.Weapons.AddRange(weaponsSelected);
                }

                newMonster.Inventory = temp;
            }

            context.Monster.Add(newMonster);
            await context.SaveChangesAsync();
            return newMonster;
        }

        // Update
        public async Task<Monster?> Update (Monster updateMonster)
        {
            Monster monster = await GetById(updateMonster.Id);
            if (monster != null && monster != null)
            {
                if (!string.IsNullOrWhiteSpace(updateMonster.MonsterName))
                    monster.MonsterName = updateMonster.MonsterName;
                monster.MonsterXp = updateMonster.MonsterXp;
                monster.LevelDifficulty = updateMonster.LevelDifficulty;

                if (!string.IsNullOrWhiteSpace(updateMonster.Note))
                    monster.Note = updateMonster.Note;

                if (monster.EntityBaseSystem != null && updateMonster.EntityBaseSystem != null)
                {
                    UpdateEBS(monster.EntityBaseSystem, updateMonster.EntityBaseSystem);
                }

                if (updateMonster.Inventory != null && monster.Inventory != null)
                {
                    UpdateInventory(monster.Inventory, updateMonster.Inventory);

                    if (updateMonster.Inventory.Weapons != null)
                    {
                        var weaponsSelected = context.Weapon.
                            Where(w => updateMonster.Inventory.Weapons.Select(x => x.Id).ToArray().Contains(w.Id)).ToList();

                        monster.Inventory.Weapons = weaponsSelected;
                    }

                    if (updateMonster.Inventory.Potions != null && monster.Inventory.Potions != null)
                    {
                        UpdatePotion(monster.Inventory, updateMonster.Inventory);
                    }
                }
                context.Monster.Update(monster);
                await context.SaveChangesAsync();
                return monster;
            }
            return null;
        }

        // Delete
        public async Task<Monster> Delete(int id)
        {
            Monster monster = await GetById (id);
            if (monster != null)
            {
                context.Monster.Remove(monster);
                await context.SaveChangesAsync();
            }
            return monster!;
        }
    }
}
