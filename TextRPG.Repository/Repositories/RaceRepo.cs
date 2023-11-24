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
        /*
        public List<Race> GetAll()
        {
            return context.Race.ToList();
        }*/

        // GetById
        public async Task<Race> GetById(int id)
        {
            return await context.Race.FirstAsync(x => x.Id == id);
        }
        /*
        public Race GetById(int id)
        {
            return context.Race.First(x => x.Id == id);
        }*/

        // Create
        public async Task<Race> Create(Race newRace)
        {
            context.Race.Add(newRace);
            await context.SaveChangesAsync();
            return newRace;
        }
        /*
        public void Create(Race model)
        {
            //TODO: Should we return the (Race)model?
            context.Race.Add(model);
            context.SaveChanges();
        }*/

        // Update
        public async void Update(Race updateRace)
        {
            Race race = await GetById(updateRace.Id);
            if (race != null && updateRace != null)
            {
                race.RaceType = updateRace.RaceType;
                await context.SaveChangesAsync();
            }
        }
        /*
        public void Update(Race model)
        {
            context.Race.Update(model);
            context.SaveChanges();
        }*/

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
        /*
        public void Delete(int id)
        {
            context.Race.Remove(GetById(id));
            context.SaveChanges();
        }*/
    }
}
