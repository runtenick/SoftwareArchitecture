using EF_Champions.Entities;
using Microsoft.EntityFrameworkCore;
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

            ChampionEntity c1 = new(){ Id = 1, Name = "Champ1"};
            ChampionEntity c2 = new(){ Id = 2, Name = "Champ2" };

            modelBuilder.Entity<ChampionEntity>().HasData(c1, c2);

            modelBuilder.Entity<SkinEntity>().Property<long>("Id");

            modelBuilder.Entity<SkinEntity>().HasData(
                new SkinEntity { Name = "Skin1", ChampionId = 1},
                new SkinEntity { Name = "Skin2", ChampionId = 1},
                new SkinEntity { Name = "Skin3", ChampionId = 2 },
                new SkinEntity { Name = "Skin4", ChampionId = 2 }
                );
        }
    }
}
