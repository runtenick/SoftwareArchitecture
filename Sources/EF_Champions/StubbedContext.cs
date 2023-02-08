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

            Champion c1 = new Champion("Akali", ChampionClass.Assassin);
            Champion c2 = new Champion("Aatrox", ChampionClass.Fighter);

            modelBuilder.Entity<ChampionEntity>().HasData(c1, c2);

            modelBuilder.Entity<SkinEntity>().Property<long>("ChampionId");

            modelBuilder.Entity<SkinEntity>().HasData(
                new { Id = 1, ChampionId = 1, Name = "Skin1" },
                new { Id = 2, ChampionId = 1, Name = "Skin2" },
                new { Id = 3, ChampionId = 2, Name = "Skin3" },
                new { Id = 4, ChampionId = 2, Name = "Skin4" }
            );
        }
    }
}
