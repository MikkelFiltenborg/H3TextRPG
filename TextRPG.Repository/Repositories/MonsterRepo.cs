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
    public class MonsterRepo : IBaseCRUDRepo<Monster>
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
        /*
        public List<Monster> GetAll()
        {
            return context.Monster.ToList();
        }*/

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
                .FirstAsync();
        }
        /*
        public Monster GetById(int id)
        {
            return context.Monster.First(x => x.Id == id);
        }*/

        // Create
        public async Task<Monster> Create (Monster newMonster)
        {
            context.Monster.Add(newMonster);
            await context.SaveChangesAsync();
            return newMonster;
        }
        /*
        public void Create(Monster model)
        {
            //TODO: Should we return the (Monster)model?
            context.Monster.Add(model);
            context.SaveChanges();
        }*/

        // Update
        public async Task<Monster?> Update (Monster updateMonster)
        {
            Monster monster = await GetById(updateMonster.Id);
            if (monster != null && monster != null)
            {
                monster.MonsterName = updateMonster.MonsterName;
                monster.MonsterXp = updateMonster.MonsterXp;
                monster.LevelDifficulty = updateMonster.LevelDifficulty;
                monster.Note = updateMonster.Note;
                context.Monster.Update(monster);
                await context.SaveChangesAsync();
            }
            return monster;
        }
        /*
        public void Update(Monster model)
        {
            context.Monster.Update(model);
            context.SaveChanges();
        }*/

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
        /*
        public void Delete(int id)
        {
            context.Monster.Remove(GetById(id));
            context.SaveChanges();
        }*/
    }
}
