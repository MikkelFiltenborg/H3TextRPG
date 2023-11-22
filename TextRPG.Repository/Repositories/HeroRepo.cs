using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Server;
using Microsoft.EntityFrameworkCore;

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
            //Hero temp = new Hero()
            //{
            //    Id = model.Id,
            //    HeroName = model.HeroName,
            //    HeroXp = model.HeroXp,
            //    Level = model.Level,
            //    Note = model.Note
            //};
            //var ebsSelected = context.EntityBaseSystem.
            //    Where(e => model.EntityBaseSystem.Select(p=>p.Id).ToArray().Contain(e.Id)).ToList();

            context.Hero
                //.Include(x => x.EntityBaseSystem)
                //.Include(x => x.Inventory)
                //.Include(x => x.Race)
                //.Include(x => x.Career)
                .Add(model);
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
                    context.Inventory.Remove(hero.Inventory);
                context.Hero.Remove(hero);
            }
            context.SaveChanges();
        }

        public List<Hero> GetAll()
        {
            return context.Hero
                .Include(x => x.Inventory)
                .Include(x => x.EntityBaseSystem)
                .Include(x => x.Race)
                .Include(x => x.Career)
                .ToList();
        }

        public Hero GetById(int id)
        {
            return context.Hero
                .Include(x => x.Inventory)
                .Include(x => x.EntityBaseSystem)
                .Include(x => x.Race)
                .Include(x => x.Career)
                .First(x => x.Id == id);
        }

        public void Update(Hero model)
        {
            throw new NotImplementedException("Update not here yet, fuck off");
            //context.Hero.Update(model);
            //context.SaveChanges();
        }
    }
}
