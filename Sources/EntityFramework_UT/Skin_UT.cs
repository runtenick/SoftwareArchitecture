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
        }
    }
}
