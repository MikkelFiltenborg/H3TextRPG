using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextRPG.Repository.Models;

namespace TextRPG.Repository.Server
{
    public class Dbcontext : DbContext
    {
        public Dbcontext(DbContextOptions<Dbcontext> option) : base(option)
        {
            // if I want a direct access to db, I write it here (like Program.cs)
        }

        public DbSet<Armour> Armour { get; set; }
        public DbSet<Career> Career { get; set; }
        public DbSet<EntityBaseSystem> EntityBaseSystem { get; set; }
        public DbSet<Hero> Hero { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Monster> Monster { get; set; }
        public DbSet<Potion> Potion { get; set; }
        public DbSet<PotionType> PotionType { get; set; }
        public DbSet<Race> Race { get; set; }
        public DbSet<SkillRollType> SkillRollType { get; set; }
        public DbSet<Weapon> Weapon { get; set; }
        public DbSet<WeaponType> WeaponType { get; set; }

    }
}
