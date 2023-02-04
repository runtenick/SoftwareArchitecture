using EF_Champions.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions
{
    public class ChampDbContext : DbContext
    {
        public DbSet<ChampionEntity> Champions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseSqlite($"Data Source = EF.myDatabse.db");
    }
}
