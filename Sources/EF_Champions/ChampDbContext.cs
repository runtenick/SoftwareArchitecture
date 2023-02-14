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
        public DbSet<SkinEntity> Skins { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseSqlite($"Data Source = EF.myDatabse.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<ChampionEntity>().HasKey(c => c.Id);
            modelBuilder.Entity<ChampionEntity>().Property(c => c.Id)
                .ValueGeneratedOnAdd(); // génération a l'insertion */

            modelBuilder.Entity<SkinEntity>().Property<int>("ChampionForeignKey");

            modelBuilder.Entity<SkinEntity>()
                .HasOne(s => s.Champion) // a skin has a champion
                .WithMany(e => e.Skins) // a champion has multiple skins
                .HasForeignKey("ChampionForeignKey"); // thtrough the foreign key 
        }
    }
}
