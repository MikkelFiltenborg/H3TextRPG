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
    public class CareerRepo : IBaseCRUDRepo<Career>
    {
        Dbcontext context;
        public CareerRepo(Dbcontext temp)
        {
            context = temp;
        }

        // GetAll
        public async Task<List<Career>> GetAll()
        {
            return await context.Career.ToListAsync();
        }
        /*
        public List<Career> GetAll()
        {
            return context.Career.ToList();
        }*/

        // GetById
        public async Task<Career> GetById(int id)
        {
            return await context.Career.FirstAsync(x => x.Id == id);
        }
        /*
        public Career GetById(int id)
        {
            return context.Career.First(x => x.Id == id);
        }*/

        // Create
        public async Task<Career> Create(Career newCareer)
        {
            context.Career.Add(newCareer);
            await context.SaveChangesAsync();
            return newCareer;
        }
        /*
        public void Create(Career model)
        {
            //TODO: Should we return the (Career)model?
            context.Career.Add(model);
            context.SaveChanges();
        }*/

        // Update
        public async Task<Career?> Update(Career updateCareer)
        {
            Career career = await GetById(updateCareer.Id);
            if (career != null && updateCareer != null)
            {
                career.CareerType = updateCareer.CareerType;
                context.Career.Update(career);
                await context.SaveChangesAsync();
                //return career;  
            }
            return career;
        }
        /*
        public void Update(Career model)
        {
            context.Career.Update(model);
            context.SaveChanges();
        }*/

        // Delete
        public async Task<Career> Delete(int id)
        {
            Career career = await GetById(id);
            if (career != null)
            {
                context.Career.Remove(career);
                await context.SaveChangesAsync();
            }
            return career!;
        }
        /*
        public void Delete(int id)
        {
            context.Career.Remove(GetById(id));
            context.SaveChanges();
        }*/
    }
}
