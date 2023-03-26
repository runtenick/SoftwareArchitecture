using EF_Champions;
using EF_Champions.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework_UT.Theory
{
    public class ChampionUT_theory
    {
        // CRUD TESTS
        [Theory]
        [InlineData("Akali", ChampionClass.Assassin)]
        [InlineData("Aatrox", ChampionClass.Fighter)]
        [InlineData("Ahri", ChampionClass.Mage)]
        public void Add_Test(string name, ChampionClass championClass)
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Test_Database_Theory")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                ChampionEntity champion = new() 
                { 
                    Name = name, 
                    Class = championClass 
                };

                context.Champions.Add(champion);

                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                Assert.Single(context.Champions.Where(champion => champion.Name == name && champion.Class == championClass));
            }
        }

        [Theory]
        [InlineData(null)]
        public void Add_Bad_Name(string name)
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Bad_Name")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                ChampionEntity champion = new() 
                { 
                    Name = name, 
                    Class = ChampionClass.Assassin 
                };
                context.Champions.Add(champion);

                Assert.Throws<DbUpdateException>(() => context.SaveChanges());
            }
        }

    }
}
