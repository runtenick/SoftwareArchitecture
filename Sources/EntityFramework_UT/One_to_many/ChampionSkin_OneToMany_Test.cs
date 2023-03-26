using EF_Champions.Entities;
using EF_Champions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework_UT.One_to_many
{
    public class ChampionSkin_OneToMany_Test
    {
        public class ChampionEntityTests
        {
            [Theory]
            [InlineData("Akali", 2)]
            [InlineData("Aatrox", 2)]
            public void SkinsToChampion(string championName, int skinCount)
            {
                // Arrange
                var options = new DbContextOptionsBuilder<ChampDbContext>()
                    .UseInMemoryDatabase(databaseName: "Skins_to_champion")
                    .Options;

                using var context = new ChampDbContext(options);
                context.Database.EnsureDeleted();

                var champion = new ChampionEntity { Name = championName };
                var skins = new List<SkinEntity>
                {
                    new SkinEntity { Name = "Skin 1", Price = 100.0f },
                    new SkinEntity { Name = "Skin 2", Price = 200.0f }
                };

                champion.Skins = skins;

                // Act
                context.Champions.Add(champion);
                context.SaveChanges();

                // Assert
                var actualChampion = context.Champions
                    .Include(c => c.Skins)
                    .FirstOrDefault(c => c.Name == championName);

                Assert.NotNull(actualChampion);
                Assert.Equal(skinCount, actualChampion.Skins.Count);
            }

            [Fact]
            public void CanGetChampionWithSkins()
            {
                // Arrange
                var options = new DbContextOptionsBuilder<ChampDbContext>()
                    .UseInMemoryDatabase(databaseName: "Champion_from_skins")
                    .Options;

                using var context = new ChampDbContext(options);
                context.Database.EnsureDeleted();

                var champion = new ChampionEntity { Name = "Akali" };
                var skins = new List<SkinEntity>
                {
                    new SkinEntity { Name = "Aiguillon", Price = 650.0f },
                    new SkinEntity { Name = "All-Star", Price = 1050.0f }
                };

                champion.Skins = skins;

                context.Champions.Add(champion);
                context.SaveChanges();

                // Act
                var actualChampion = context.Champions
                    .Include(c => c.Skins)
                    .FirstOrDefault(c => c.Name == "Akali");

                // Assert
                Assert.NotNull(actualChampion);
                Assert.Equal("Akali", actualChampion.Name);
                Assert.Equal(2, actualChampion.Skins.Count);
            }
        }

    }
}
