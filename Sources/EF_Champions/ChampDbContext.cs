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

        /*protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChampionEntity>().HasKey(e => e.Id);
            modelBuilder.Entity<ChampionEntity>().Property(e => e.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<SkinEntity>().Property<long>("ChampionForeignKey");

            modelBuilder.Entity<SkinEntity>()
                .HasOne(s => s.Champion)
                .WithMany(e => e.Skins)
                .HasForeignKey("ChampionForeignKey");
        }*/
    }
}
