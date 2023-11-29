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
    public class RaceRepo : IBaseCRUDRepo<Race>
    {
        Dbcontext context;
        public RaceRepo(Dbcontext temp)
        {
            context = temp;
        }

        // GetAll
        public async Task<List<Race>> GetAll()
        {
            return await context.Race.ToListAsync();
        }

        // GetById
        public async Task<Race> GetById(int id)
        {
            return await context.Race.FirstAsync(x => x.Id == id);
        }

        // Create
        public async Task<Race> Create(Race newRace)
        {
            context.Race.Add(newRace);
            await context.SaveChangesAsync();
            return newRace;
        }

        // Update
        public async Task<Race?> Update(Race updateRace)
        {
            Race race = await GetById(updateRace.Id);
            if (race != null && updateRace != null)
            {
                if (!string.IsNullOrWhiteSpace(updateRace.RaceType))
                    race.RaceType = updateRace.RaceType;
                context.Race.Update(race);
                await context.SaveChangesAsync();
            }
            return race;
        }

        // Delete
        public async Task<Race> Delete(int id)
        {
            Race race = await GetById(id);
            if (race != null)
            {
                context.Race.Remove(race);
                await context.SaveChangesAsync();
            }
            return race!;
        }
    }
}
