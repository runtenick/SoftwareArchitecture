using EF_Champions;
using EF_Champions.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EntityFramework_UT
{
    public class Skin_UT
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

                SkinEntity stinger = new SkinEntity() { Name = "Stinger", Price = 5.0F };
                SkinEntity infernal = new SkinEntity() { Name = "Infernal", Price = 100.0F };
                SkinEntity allstar = new SkinEntity() { Name = "All-Star", Price = 25.55F };
                
                context.Skins.Add(stinger);
                context.Skins.Add(infernal);
                context.Skins.Add(allstar);
                
                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                Assert.Equal(3, context.Skins.Count());
                Assert.Equal("Stinger", context.Skins.First().Name);
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
                
                SkinEntity stinger = new SkinEntity() { Name = "Stinger", Price = 5.0F };
                SkinEntity infernal = new SkinEntity() { Name = "Infernal", Price = 100.0F };
                SkinEntity allstar = new SkinEntity() { Name = "All-Star", Price = 25.55F };
                
                context.Skins.Add(stinger);
                context.Skins.Add(infernal);
                context.Skins.Add(allstar);
                
                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                string nameTargeted = "ng";
                Assert.Equal(1, context.Skins.Where(c => c.Name.ToLower().Contains(nameTargeted)).Count());

                nameTargeted = "i";
                Assert.Equal(2, context.Skins.Where(c => c.Name.ToLower().Contains(nameTargeted)).Count());

                var stinger = context.Skins.Where(c => c.Name.ToLower().Contains(nameTargeted)).First();
                stinger.Name = "Mecha";
                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();
                Assert.Equal(1, context.Skins.Where(c => c.Name.ToLower().Contains("mec")).Count());

            }
        }
    }
}
