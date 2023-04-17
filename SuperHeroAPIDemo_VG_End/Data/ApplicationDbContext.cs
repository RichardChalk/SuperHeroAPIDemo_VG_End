using Microsoft.EntityFrameworkCore;
using SuperHeroAPIDemo_G.Models;
using System.Collections.Generic;

namespace SuperHeroAPIDemo_G.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<SuperHero> SuperHeroes { get; set; }
    }

}
