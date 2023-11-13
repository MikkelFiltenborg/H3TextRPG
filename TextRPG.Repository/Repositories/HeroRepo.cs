﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Interfaces;
using TextRPG.Repository.Models;
using TextRPG.Repository.Server;

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
            context.Hero.Add(model);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Hero.Remove(GetById(id));
            context.SaveChanges();
        }

        public List<Hero> GetAll()
        {
            return context.Hero.ToList();
        }

        public Hero GetById(int id)
        {
            return context.Hero.First(x => x.Id == id);
        }

        public void Update(Hero model)
        {
            context.Hero.Update(model);
            context.SaveChanges();
        }
    }
}
