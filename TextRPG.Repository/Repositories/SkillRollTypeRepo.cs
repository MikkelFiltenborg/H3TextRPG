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
    public class SkillRollTypeRepo : IBaseCRUDRepo<SkillRollType>
    {
        Dbcontext context;

        public SkillRollTypeRepo(Dbcontext temp)
        {
            context = temp;
        }

        // GetAll
        public async Task<List<SkillRollType>> GetAll()
        {
            return await context.SkillRollType.ToListAsync();
        }
        /*
        public List<SkillRollType> GetAll()
        {
            return context.SkillRollType.ToList();
        }*/

        // GetById
        public async Task<SkillRollType> GetById(int id)
        {
            return await context.SkillRollType
                .Include(x => x.SkillType)
                .FirstAsync(x => x.Id == id);
        }
        /*
        public SkillRollType GetById(int id)
        {
            return context.SkillRollType.First(x => x.Id == id);
        }*/

        // Create
        public async Task<SkillRollType> Create(SkillRollType newSkillRollType)
        {
            context.SkillRollType.Add(newSkillRollType);
            await context.SaveChangesAsync();
            return newSkillRollType;
        }
        /*
        public void Create(SkillRollType model)
        {
            context.SkillRollType.Add(model);
            context.SaveChanges();
        }*/

        // Update
        public async void Update(SkillRollType updateSkillRollType)
        {
            SkillRollType skillRollType = await GetById(updateSkillRollType.Id);
            if (skillRollType != null && updateSkillRollType != null)
            {
                skillRollType.SkillType = updateSkillRollType.SkillType;
                await context.SaveChangesAsync();
            }
        }
        /*
        public void Delete(int id)
        {
            context.SkillRollType.Remove(GetById(id));
            context.SaveChanges();
        }*/

        // Delete
        public async void Delete(int id)
        {
            SkillRollType skillRollType = await GetById(id);
            if (skillRollType != null)
            {
                context.SkillRollType.Remove(skillRollType);
                await context.SaveChangesAsync();
            }
        }
        /*
        public void Update(SkillRollType model)
        {
            context.SkillRollType.Update(model);
            context.SaveChanges();
        }*/
    }
}
