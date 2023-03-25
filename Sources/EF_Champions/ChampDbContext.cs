using EF_Champions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions
{
    public class ChampDbContext : DbContext
    {
        // Constructeurs
        public ChampDbContext()
        { }

        public ChampDbContext(DbContextOptions<ChampDbContext> options)
            : base(options)
        { }

        // Entity sets
        public DbSet<ChampionEntity> Champions { get; set; }
        public DbSet<SkinEntity> Skins { get; set; }
        public DbSet<SkillEntity> Skill { get; set; }
        public DbSet<RuneEntity> Runes { get; set; }
        public DbSet<RunePageEntity> RunePages { get; set; } 

        /* Old version used before InMemory tests
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
           => optionsBuilder.UseSqlite($"Data Source = EF.myDatabse.db");
        */
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source=EF.myDatabse.db");
            }
        }        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // one-to-many
            modelBuilder.Entity<SkinEntity>().Property<int>("ChampionForeignKey");

            modelBuilder.Entity<SkinEntity>()
                .HasOne(s => s.Champion)// a skin has a champion
                .WithMany(e => e.Skins) // a champion has multiple skins
                .HasForeignKey("ChampionForeignKey"); // through the foreign key 

            // many-to-many
            modelBuilder.Entity<SkillEntity>()
                .HasMany(s => s.Champions) // a skill has many champions
                .WithMany(c => c.Skills);  // each champion has many skills

            modelBuilder.Entity<RuneEntity>()
                .HasMany(r => r.Pages)
                .WithMany(p => p.Runes);

            modelBuilder.Entity<ChampionEntity>()
                .HasMany(c => c.PagesRune)
                .WithMany(p => p.Champions);
               
        }

        public void Seed() 
        {
            if (!Champions.Any()) 
            {
                Champions.AddRange(new[] 
                {
                    new ChampionEntity() { Name = "Satoru", Class = ChampionClass.Assassin},
                    new ChampionEntity() { Name = "Nana", Class = ChampionClass.Fighter},
                });

                SaveChanges();
            }
        }
    }
}
