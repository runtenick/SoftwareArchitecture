using EF_Champions;
using EF_Champions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Xunit;
using Microsoft.EntityFrameworkCore.Storage;

namespace EntityFramework_UT
{
    public class ChampionDB_Tests
    {
        [Fact]
        public void Add_Test()
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Test_Database")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                ChampionEntity akali = new() { Name = "Akali", Class = ChampClassEntity.Assassin };
                ChampionEntity aatrox = new() { Name = "Aatrox", Class = ChampClassEntity.Fighter };
                ChampionEntity ahri = new() { Name = "Ahri", Class = ChampClassEntity.Mage };

                context.Champions.Add(akali);
                context.Champions.Add(aatrox);
                context.Champions.Add(ahri);

                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                Assert.Equal(3, context.Champions.Count());
                Assert.Equal("Akali", context.Champions.First().Name);
            }
        }
        
        
        [Fact]
        public void Modify_Test()
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Modify_Test")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();
                context.Champions.RemoveRange(context.Champions);

                ChampionEntity akali = new() { Name = "Akali", Class = ChampClassEntity.Assassin };
                ChampionEntity aatrox = new() { Name = "Aatrox", Class = ChampClassEntity.Fighter };
                ChampionEntity ahri = new() { Name = "Ahri", Class = ChampClassEntity.Mage };

                context.Champions.Add(akali);
                context.Champions.Add(aatrox);
                context.Champions.Add(ahri);
                context.SaveChanges();
                
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                string nameTargeted = "ak";
                Assert.Equal(1,context.Champions.Where(c => c.Name.ToLower().Contains(nameTargeted)).Count());

                nameTargeted = "i";
                Assert.Equal(2, context.Champions.Where(c => c.Name.ToLower().Contains(nameTargeted)).Count());

                var akali = context.Champions.Where(c => c.Name.ToLower().Contains(nameTargeted)).First();
                akali.Name = "Bard";
                context.SaveChanges();
            }
   
            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();
                Assert.Equal(1, context.Champions.Where(c => c.Name.ToLower().Contains("bar")).Count());

            }
        }

        [Fact]
        public void Remove_Test()
        {
            var connection = new SqliteConnection("DataSource=:memory");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
               .UseInMemoryDatabase(databaseName: "Remove_Test")
               .Options;

        }
    }
}

