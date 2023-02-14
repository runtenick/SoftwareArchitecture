using EF_Champions.Entities;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Champions
{
    public class StubbedContext : ChampDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ChampionEntity c1 = new ChampionEntity() { Id = 1, Name = "Akali", Class = ChampClassEntity.Assassin};
            ChampionEntity c2 = new ChampionEntity() { Id = 2, Name = "Aatrox", Class = ChampClassEntity.Fighter};


            modelBuilder.Entity<ChampionEntity>().HasData(c1, c2);

           //modelBuilder.Entity<SkinEntity>().Property<long>("ChampionId");

            modelBuilder.Entity<SkinEntity>().HasData(
                new { Id = 1, ChampionForeignKey = 1, Name = "Skin1" },
                new { Id = 2, ChampionForeignKey = 1, Name = "Skin2" },
                new { Id = 3, ChampionForeignKey = 2, Name = "Skin3" },
                new { Id = 4, ChampionForeignKey = 2, Name = "Skin4" }
            );
        }
    }
}
