using EF_Champions;
using EF_Champions.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Model;
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
                context.Skins.RemoveRange(context.Skins);
                
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
        
        [Fact]
        public void Remove_Test()
        {
            var connection = new SqliteConnection("DataSource=:memory");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
               .UseInMemoryDatabase(databaseName: "Remove_Test")
               .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();
                context.Skins.RemoveRange(context.Skins);

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
                int totalSkins = context.Skins.Count();
                SkinEntity first = context.Skins.First(); // get first to check if removed later

                context.Skins.Remove(context.Skins.First());
                context.SaveChanges();
               
                Assert.Equal(totalSkins - 1, context.Skins.Count());
                Assert.DoesNotContain(first, context.Skins);
            }
        }

        // ERROR TESTS

        // assert that the Id is the unique key
        [Fact]
        public void Add_Duplicate_Test()
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Duplicate_Test")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                SkinEntity stinger = new SkinEntity() { Name = "Stinger", Price = 5.0F };
                context.Skins.Add(stinger);

                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                SkinEntity duplicate = new SkinEntity() { Id = 1, Name = "Stinger", Price = 5.0F };
                context.Skins.Add(duplicate);

                Assert.Throws<ArgumentException>(() => context.SaveChanges());
            }
        }
    }
}
