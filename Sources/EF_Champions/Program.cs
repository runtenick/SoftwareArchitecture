// See https://aka.ms/new-console-template for more information
using EF_Champions;
using EF_Champions.Entities;
using EF_Champions.Mapper;
using Microsoft.EntityFrameworkCore;
using Model;
using StubLib;

class Program
{
    static void Main()
    {
        Console.WriteLine("Starting...");

        // insertion de one to many avec skin
        using (ChampDbContext db = new ChampDbContext())
        {
            //...

            ChampionEntity akali = new ChampionEntity { Name = "Akali", Class = ChampClassEntity.Assassin };
            SkinEntity[] skins_akali =
            {
                new SkinEntity() { Name = "Skin1", Champion = akali },
                new SkinEntity() { Name = "Skin2", Champion = akali },
                new SkinEntity() { Name = "Skin3", Champion = akali },
             };

            foreach (var m in skins_akali)
            {
                akali.Skins.Add(m);
            }

            db.Add(akali);
            db.SaveChanges();
        }

        // display on console to check
        using (ChampDbContext db = new ChampDbContext())
        {
            Console.WriteLine("Champions : ");
            foreach (var c in db.Champions.Include(c => c.Skins))
            {
                Console.WriteLine($"\t{c.Id}: {c.Name}");
                foreach (var s in c.Skins)
                {
                    Console.WriteLine($"\t\t{s.Name}");
                }
            }

            Console.WriteLine();

            Console.WriteLine("Skins :");
            foreach (var s in db.Skins)
            {
                Console.WriteLine($"\t{s.Id}: {s.Name} (Champion : {s.Champion.Name})");
            }
        }

        // add fake Skill data
        using (ChampDbContext db = new ChampDbContext())
        {
            ChampionEntity aatrox = new ChampionEntity { Name = "Aatrox", Class = ChampClassEntity.Mage };
            SkillEntity[] skills =
            {
                new SkillEntity() { Name = "Skill1", Description = "desc..1", SkillType = SkillType.Basic },
                new SkillEntity() { Name = "Skill2", Description = "desc..2", SkillType = SkillType.Passive },
                new SkillEntity() { Name = "Skill2", Description = "desc..3", SkillType = SkillType.Passive }
            };

            SkinEntity[] skins_aatrox =
            {
                new SkinEntity() { Name = "Skin4", Champion = aatrox },
                new SkinEntity() { Name = "Skin5", Champion = aatrox },
                new SkinEntity() { Name = "Skin6", Champion = aatrox },
             };

            foreach (var m in skins_aatrox)
            {
                aatrox.Skins.Add(m);
            }

            foreach (var sk in skills)
            {
                sk.Champions.Add(aatrox);
                aatrox.Skills.Add(sk);
            }

            db.Add(aatrox);
            db.SaveChanges();
        }

    }
}


// --------------------------------------------------------------------------------------------------------------
/* TEST SKINS
 * 
 * StubData stub = new();
var skins = (await stub.SkinsMgr.GetItems(0,
               (await stub.SkinsMgr.GetNbItems()))).Select(s => s?.SkinToEntity());

using (var context = new SkinDbContext())
{
    foreach (SkinEntity skin in skins)
    {
        context.Skins.Add(skin);
    }
    context.SaveChanges();
}*/


/* TEST CHAMPIONS
 * 
 * using ici permet de explicité le nettoyage de mémoire qui n'est pas implicite car le 
 * garbage collector de dotnet ne peut pas s'occuper de connections a la base de donnée 
 * par lui même*//*
using (var context = new ChampDbContext())
{
    context.Champions.AddRange(
        new ChampionEntity
        {
            Name = "Akali",
            Class = ChampClassEntity.Assassin
        },
        new ChampionEntity
        {
            Name = "Aatrox",
            Class = ChampClassEntity.Tank
        },
        new ChampionEntity
        {
            Name = "Ahri",
            Class = ChampClassEntity.Mage
        }
    );
    context.SaveChanges(); // permet de synchroniser les ajouts a la base de do
}


// Afficher le contenu de la base en console
using (var context2 = new ChampDbContext())
{
    var champions = context2.Champions;

    foreach (var champion in champions)
    {
        Console.WriteLine($"{champion.Id} : {champion.Name}");
    }
}*/
