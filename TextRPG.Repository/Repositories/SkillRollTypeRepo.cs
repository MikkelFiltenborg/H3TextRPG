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

        // GetById
        public async Task<SkillRollType> GetById(int id)
        {
            return await context.SkillRollType
                .Include(x => x.SkillType)
                .FirstAsync(x => x.Id == id);
        }

        // Create
        public async Task<SkillRollType> Create(SkillRollType newSkillRollType)
        {
            context.SkillRollType.Add(newSkillRollType);
            await context.SaveChangesAsync();
            return newSkillRollType;
        }

        // Update
        public async Task<SkillRollType?> Update(SkillRollType updateSkillRollType)
        {
            SkillRollType skillRollType = await GetById(updateSkillRollType.Id);
            if (skillRollType != null && updateSkillRollType != null)
            {
                skillRollType.SkillType = updateSkillRollType.SkillType;
                context.SkillRollType.Update(skillRollType);
                await context.SaveChangesAsync();
            }
            return skillRollType;
        }

        // Delete
        public async Task<SkillRollType> Delete(int id)
        {
            SkillRollType skillRollType = await GetById(id);
            if (skillRollType != null)
            {
                context.SkillRollType.Remove(skillRollType);
                await context.SaveChangesAsync();
            }
            return skillRollType!;
        }
    }
}
