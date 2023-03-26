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

namespace EntityFramework_UT.Theory
{
    public class SkillUT_theory
    {
        public static IEnumerable<object[]> Data_Skill()
        {
            yield return new object[]
            {
        new SkillEntity
        {
            Name = "Hiro Arikawa",
            Description = "The Travelling Cat Chronicles",
            SkillType = SkillType.Basic,
            Champions = new List<ChampionEntity>()
        }
            };
            yield return new object[]
            {
        new SkillEntity
        {
            Name = "J.R.R Tolkien",
            Description = "Lord of the Rings",
            SkillType = SkillType.Passive,
            Champions = new List<ChampionEntity>()
        }
            };
            yield return new object[]
            {
        new SkillEntity
        {
            Name = "Andy Weir",
            Description = "Project Hail Mary",
            SkillType = SkillType.Ultimate,
            Champions = new List<ChampionEntity>()
        }
            };
        }

        [Theory]
        [MemberData(nameof(Data_Skill))]
        public void Add_Skill_Test(SkillEntity skill)
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Add_Skill_Test")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                context.Skill.Add(skill);
                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                Assert.Single(context.Skill.Where(s => s.Name == skill.Name && s.Description == skill.Description && s.SkillType == skill.SkillType));
            }
        }

        [Theory]
        [MemberData(nameof(Data_Skill))]
        public void Modify_Skill_Test(SkillEntity modifiedSkill)
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Modify_Skill_Test")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                var skill = new SkillEntity
                {
                    Name = "Initial Skill",
                    Description = "Initial Description",
                    SkillType = SkillType.Basic,
                    Champions = new List<ChampionEntity>()
                };

                context.Skill.Add(skill);
                context.SaveChanges();

                var skillToUpdate = context.Skill.FirstOrDefault(s => s.Id == skill.Id);

                skillToUpdate.Name = modifiedSkill.Name;
                skillToUpdate.Description = modifiedSkill.Description;
                skillToUpdate.SkillType = modifiedSkill.SkillType;

                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();
                Assert.Single(context.Skill.Where(s => s.Name == modifiedSkill.Name && s.Description == modifiedSkill.Description && s.SkillType == modifiedSkill.SkillType));
            }
        }

        [Theory]
        [MemberData(nameof(Data_Skill))]
        public void Delete_Skill_Test(SkillEntity skillToDelete)
        {
            var connection = new SqliteConnection("DataSource=:memory:");

            var options = new DbContextOptionsBuilder<ChampDbContext>()
                .UseInMemoryDatabase(databaseName: "Delete_Skill_Test")
                .Options;

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                context.Skill.Add(skillToDelete);
                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                var skill = context.Skill.FirstOrDefault(s => s.Id == skillToDelete.Id);

                context.Skill.Remove(skill);
                context.SaveChanges();
            }

            using (var context = new ChampDbContext(options))
            {
                context.Database.EnsureCreated();

                Assert.Null(context.Skill.FirstOrDefault(s => s.Id == skillToDelete.Id));
                //Assert.Null(context.Skill.FirstOrDefault(s => s.Name == skillToDelete.Name));
            }
        }
    }
}
