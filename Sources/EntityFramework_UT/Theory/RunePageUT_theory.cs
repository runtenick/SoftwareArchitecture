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
        public static IEnumerable<object[]> Data_AddRuneToPage()
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
        [MemberData(nameof(Data_AddRuneToPage))]
        public void Add_RunePage(string name, ICollection<RuneEntity> runes, ICollection<ChampionEntity> champions)
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
    }
}
