using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG.Repository.Server
{
    public class Dbcontext : DbContext
    {
        public Dbcontext(DbContextOptions<Dbcontext> option) : base(option)
        {
            // if I want a direct access to db, I write it here (like Program.cs)
        }

        //TODO: public DbSet<Model> model { get; set; }
    }
}
