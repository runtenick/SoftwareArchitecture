using EF_Champions.Entities;
using EF_Champions;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFramework_UT.Theory
{
    public class RuneUT_theory
    {
        [Theory]
        [InlineData("Conqueror", "Lorem ipsum dolor sit amet", RuneFamily.Precision, "icon", "image")]
        [InlineData("Dark Harvest", "consectetur adipiscing elit", RuneFamily.Domination, "icon", "image")]
        [InlineData("Comet", "Aenean blandit purus in felis mattis pulvinar.", RuneFamily.Unknown, "icon", "image")]
        public void Add_Rune(string name, string description, RuneFamily family, string icon, string image)
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Rune_Test")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                RuneEntity rune = new RuneEntity() 
                { 
                    Name = name, 
                    Description = description, 
                    RuneFamily = family, 
                    Icon = icon, 
                    Image = image 
                };

                context.Runes.Add(rune);
                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                Assert.Single(context.Runes.Where(rune 
                    => rune.Name == name && rune.Description == description && rune.RuneFamily == family && rune.Icon == icon && rune.Image == image));
            }
        }

        [Theory]
        [InlineData("Conqueror", "Lorem ipsum dolor sit amet", RuneFamily.Precision, "icon", "image")]
        [InlineData("Dark Harvest", "consectetur adipiscing elit", RuneFamily.Domination, "icon", "image")]
        [InlineData("Comet", "Aenean blandit purus in felis mattis pulvinar.", RuneFamily.Unknown, "icon", "image")]
        public void Modify_Rune(string name, string updatedDescription, RuneFamily family, string icon, string image)
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Modify_Rune_Test")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                RuneEntity originalRune = new RuneEntity()
                {
                    Name = name,
                    Description = "Original Description",
                    RuneFamily = family,
                    Icon = icon,
                    Image = image
                };

                // This should work because I have the add test earlier so no need for extra asserts
                context.Runes.Add(originalRune);
                context.SaveChanges();

                // Update Rune
                originalRune.Description = updatedDescription;
                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                // Check if Rune with mopdified desc is different
                RuneEntity modifiedRune = context.Runes.FirstOrDefault(rune
                    => rune.Name == name && rune.Description == updatedDescription && rune.RuneFamily == family && rune.Icon == icon && rune.Image == image);

                Assert.NotNull(modifiedRune);
            }
        }

        [Theory]
        [InlineData("Conqueror", RuneFamily.Precision, "icon", "image")]
        [InlineData("Dark Harvest", RuneFamily.Domination, "icon", "image")]
        [InlineData("Comet", RuneFamily.Unknown, "icon", "image")]
        public void Delete_Rune(string name, RuneFamily family, string icon, string image)
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Rune_Test")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                RuneEntity rune = new RuneEntity()
                {
                    Name = name,
                    Description = "Original Description",
                    RuneFamily = family,
                    Icon = icon,
                    Image = image
                };

                // This should work because I have the add test earlier so no need for extra asserts
                context.Runes.Add(rune);
                context.SaveChanges();
                
                // Delete Rune
                context.Runes.Remove(rune);
                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                // Check if Rune doesn't exist
                RuneEntity deletedRune = context.Runes.FirstOrDefault(rune
                    => rune.Name == name && rune.RuneFamily == family && rune.Icon == icon && rune.Image == image);

                Assert.Null(deletedRune);
            }
        }
    }
}
