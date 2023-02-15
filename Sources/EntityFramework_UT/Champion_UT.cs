using EF_Champions;
using EF_Champions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace EntityFramework_UT
{
    public class ChampionDB_Tests 
    {
        [Fact]
        public void Add_Test()
        {
           /* var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();*/

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseSqlite("DataSource=:memory:") // j'ai du passer directement car ChampDbContext créer déja une connection
                .Options;

            using (var context = new ChampDbContext(options)) 
            {
                context.Database.EnsureCreated();

                ChampionEntity akali = new ChampionEntity { Name = "Akali", Class = ChampClassEntity.Assassin };
                ChampionEntity aatrox = new ChampionEntity { Name = "Aatrox", Class = ChampClassEntity.Fighter };
                ChampionEntity ahri = new ChampionEntity { Name = "Ahri", Class = ChampClassEntity.Mage };

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
    }
}