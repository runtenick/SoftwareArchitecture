using EF_Champions.Entities;
using EF_Champions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework_UT.Theory
{
    public class RunePageUT_theory
    {
        public static IEnumerable<object[]> Data_RunePage()
        {
            yield return new object[]
            {
                "Precision Page",
                new List<RuneEntity> { new RuneEntity { Name = "Game of Thrones" }, new RuneEntity { Name = "Clash of Kings" } },
                new List<ChampionEntity>()
            };
            yield return new object[]
            {
                "Domination Page",
                new List<RuneEntity> { new RuneEntity { Name = "Storm of Swords" }, new RuneEntity { Name = "A Feast for Crows" } },
                new List<ChampionEntity>()
            };
            yield return new object[]
            {
                "Sorcery Page",
                new List<RuneEntity> { new RuneEntity { Name = "A Dance With Dragons" }, new RuneEntity { Name = "The Winds of Winter" } },
                new List<ChampionEntity>()
            };
        }

        [Theory]
        [MemberData(nameof(Data_RunePage))]
        public void Add_RunePage_Test(string name, ICollection<RuneEntity> runes, ICollection<ChampionEntity> champions)
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_RunePage_Test")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                RunePageEntity runePage = new RunePageEntity()
                {
                    Name = name,
                    Runes = runes,
                    Champions = champions
                };

                context.RunePages.Add(runePage);
                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                Assert.Single(context.RunePages.Where(runePage =>
                    runePage.Name == name && runePage.Runes.Count == runes.Count && runePage.Champions.Count == champions.Count));
            }
        }

        [Theory]
        [MemberData(nameof(Data_RunePage))]
        public void Modify_RunePage_Test(string name, ICollection<RuneEntity> runes, ICollection<ChampionEntity> champions)
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Modify_RunePage_Test")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                var runePage = new RunePageEntity
                {
                    Name = "Initial Page",
                    Runes = new List<RuneEntity> { new RuneEntity { Name = "Rune 1" }, new RuneEntity { Name = "Rune 2" } },
                    Champions = new List<ChampionEntity>()
                };

                context.RunePages.Add(runePage);
                context.SaveChanges();

                runePage.Name = name;
                runePage.Runes = runes;
                runePage.Champions = champions;
                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                var updatedRunePage = context.RunePages.First();

                Assert.Single(context.RunePages.Where(runePage =>
                   runePage.Name == name && runePage.Runes.Count == runes.Count && runePage.Champions.Count == champions.Count));
            }
        }

        [Theory]
        [MemberData(nameof(Data_RunePage))]
        public void Delete_RunePage_Test(string name, ICollection<RuneEntity> runes, ICollection<ChampionEntity> champions)
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_RunePage_Test")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                RunePageEntity runePage = new RunePageEntity()
                {
                    Name = name,
                    Runes = runes,
                    Champions = champions
                };

                context.RunePages.Add(runePage);
                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                var runePageToDelete = context.RunePages.FirstOrDefault(runePage =>
                    runePage.Name == name && runePage.Runes.Count == runes.Count && runePage.Champions.Count == champions.Count);

                if (runePageToDelete != null)
                {
                    context.RunePages.Remove(runePageToDelete);
                    context.SaveChanges();
                }

                Assert.Null(context.RunePages.FirstOrDefault(runePage =>
                    runePage.Name == name && runePage.Runes.Count == runes.Count && runePage.Champions.Count == champions.Count));
            }
        }
    }
}
